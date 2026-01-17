# Demo02: Xây dựng Web API quản lý Reservation với Repository Pattern

Dự án Demo dành cho môn **NET1051 (ASP.NET Core Web API)**, minh họa cách xây dựng API chuẩn RESTful sử dụng mô hình Repository và lưu trữ dữ liệu In-Memory.

---

## 1. Yêu cầu hệ thống & Cài đặt

### Yêu cầu
- **.NET SDK**: Phiên bản 10.0
- **Editor**: Visual Studio Code hoặc Visual Studio 2022.
- **Công cụ test**: Postman, Insomnia, hoặc VS Code REST Client.

### Thiết lập dự án (CLI)
Giảng viên/Sinh viên có thể setup nhanh bằng các lệnh sau tại Terminal:

```bash
# 1. Tạo Project Web API
dotnet new webapi -n DemoApi

# 2. Di chuyển vào thư mục
cd DemoApi

# 3. Cài đặt thư viện hỗ trợ JSON Patch (Bắt buộc cho bài này)
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
dotnet add package Microsoft.AspNetCore.JsonPatch

# 4. Chạy dự án
dotnet run
```

---

## 2. Giải thích Kiến trúc & Source Code

Mô hình dự án áp dụng **Repository Pattern** để tách biệt logic xử lý dữ liệu khỏi Controller.

### 📂 Models/Reservation.cs
Class POCO đơn giản đại diện cho dữ liệu đặt chỗ.
- **Properties**: `Id`, `Name`, `StartLocation`, `EndLocation`.

### 📂 Models/IRepository.cs & Repository.cs
Tầng layier truy xuất dữ liệu.
- **IRepository**: Interface định nghĩa các hành động CRUD (Create, Read, Update, Delete). Giúp code lỏng lẻo (loose coupling) -> Dễ dàng thay thế database sau này (ví dụ chuyển từ List sang SQL Server mà không sửa Controller).
- **Repository**: Class triển khai interface.
    - **Lưu trữ**: Sử dụng `Dictionary<int, Reservation>` để giả lập Database lưu trên RAM.
    - **Add**: Logic tự tăng ID (`items.Keys.Max() + 1`).
    - **Constructor**: Tạo sẵn dữ liệu mẫu (Seeding data) để thuận tiện cho việc test.

### 📂 Controllers/ReservationController.cs
API Controller xử lý request từ client.
- **Attribute**: `[ApiController]` và `[Route("api/[controller]")]`.
- **Dependency Injection (DI)**: Controller **không** tự khởi tạo Repository (`new Repository()`). Thay vào đó, nó nhận `IRepository` qua **Constructor**.
- **JSON Patch**: Hàm `Patch` sử dụng `JsonPatchDocument` để cập nhật từng phần dữ liệu (yêu cầu cấu hình `NewtonsoftJson`).

### 📂 Program.cs (Cấu hình DI)
Điểm quan trọng nhất của bài bài:
```csharp
// Đăng ký Repository là Singleton
builder.Services.AddSingleton<IRepository, Repository>();

// Đăng ký NewtonsoftJson để hỗ trợ JSON Patch
builder.Services.AddControllers().AddNewtonsoftJson();
```
> **Tại sao dùng Singleton?**
> Vì chúng ta đang lưu dữ liệu trên RAM (biến `Dictionary` trong class Repository).
> - Nếu dùng `AddScoped` hoặc `AddTransient`: Mỗi Request (F5) sẽ tạo ra một instance Repository mới -> **Mất dữ liệu cũ**.
> - Dùng `AddSingleton`: Chỉ tạo 1 instance duy nhất tồn tại suốt vòng đời ứng dụng -> **Giữ được dữ liệu**.

--

---

## 4. Hướng dẫn Test với Postman (Chi tiết)

Dưới đây là ví dụ cụ thể Body JSON để các bạn copy vào Postman test. 
**URL Mặc định**: `http://localhost:5xxx/api/reservation` (Thay `5xxx` bằng port thực tế khi chạy `dotnet run`).

### 1. Lấy danh sách (GET All)
- **Method**: `GET`
- **URL**: `/api/reservation`
- **Body**: Không có.

### 2. Lấy chi tiết (GET By ID)
- **Method**: `GET`
- **URL**: `/api/reservation/1`

### 3. Thêm mới (POST)
- **Method**: `POST`
- **URL**: `/api/reservation`
- **Body** (Chọn tab **Body** -> **Raw** -> Chọn **JSON**):
```json
{
  "name": "Nguyen Van A",
  "startLocation": "Ha Noi",
  "endLocation": "Ho Chi Minh"
}
```

### 4. Cập nhật toàn bộ (PUT)
*Lưu ý: PUT yêu cầu gửi đầy đủ thông tin object, nếu thiếu field nào field đó sẽ bị null/default.*
- **Method**: `PUT`
- **URL**: `/api/reservation`
- **Body** (Raw JSON):
```json
{
  "id": 1,
  "name": "Nguyen Van A (Da sua)",
  "startLocation": "Ha Noi",
  "endLocation": "Da Nang"
}
```

### 5. Cập nhật một phần (PATCH) - *Tính năng chính*
*Sử dụng chuẩn **JSON Patch** (RFC 6902) để chỉ sửa đúng các trường cần thiết.*
> [!IMPORTANT]
> Để tránh lỗi **415 Unsupported Media Type**, bạn **BẮT BUỘC** phải set Header:
> - **Key**: `Content-Type`
> - **Value**: `application/json-patch+json` (không phải `application/json`)

- **Method**: `PATCH`
- **URL**: `/api/reservation/1`
- **Body** (Raw JSON). Lưu ý đây là một **Mảng** `[]`:
```json
[
  {
    "op": "replace",
    "path": "/name",
    "value": "Ten Moi (Patch)"
  },
  {
    "op": "replace",
    "path": "/endLocation",
    "value": "Nha Trang"
  }
]
```
> **Giải thích JSON Patch**:
> - `op`: Operation (replace, add, remove, copy, move, test). Ở đây dùng `replace`.
> - `path`: Tên property cần sửa (có dấu `/` ở đầu).
> - `value`: Giá trị mới.

### 6. Xóa (DELETE)
- **Method**: `DELETE`
- **URL**: `/api/reservation/1`
- **Response**: Status **200 OK** (Body sẽ **rỗng/Empty**).
  > *Lưu ý: Code Controller đang trả về `void` nên sẽ không có nội dung JSON nào được trả về, chỉ cần check Status Code là 200 là thành công.*
