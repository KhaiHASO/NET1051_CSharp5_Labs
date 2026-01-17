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

---

## 3. Kịch bản Demo (Dành cho Giảng viên)

Kịch bản live coding từng bước trên VS Code để sinh viên dễ theo dõi.

| Bước | Hành động & Code | Lời thoại giảng viên (Ý chính) |
| :--- | :--- | :--- |
| **B1. Setup** | Chạy lệnh tạo project & add package (như mục 1).<br>Xóa file rác `WeatherForecast`. | "Đầu tiên thầy tạo project trắng. Bài này cần dùng phương thức PATCH nên bắt buộc phải cài thêm gói `NewtonsoftJson` nhé." |
| **B2. Model** | Tạo `Reservation.cs`. | "Tạo class Model đơn giản. Nhớ là ID chúng ta sẽ để tự tăng trong Repository." |
| **B3. Repo** | Tạo `IRepository` trước, sau đó tạo `Repository` implement nó.<br>Dùng `Dictionary` làm DB. | "Chúng ta dùng Interface để tuân thủ nguyên lý Dependency Inversion. Sau này các em đi làm, đổi DB chỉ cần viết class Repository mới là xong." |
| **B4. Controller** | Tạo `ReservationController`.<br>Viết Constructor nhận `IRepository`. | "Tuyệt đối không `new Repository()` ở đây nhé. Hãy để DI Container của .NET lo việc đó. Code sẽ sạch và dễ test hơn." |
| **B5. Config** | Vào `Program.cs`.<br>Thêm `AddSingleton` và `AddNewtonsoftJson`. | "Chỗ này quan trọng nhất bài: Vì sao thầy dùng Singleton? Vì thầy muốn dữ liệu còn nguyên khi thầy F5 trình duyệt. Nếu thầy dùng Scoped, biến Dictionary sẽ bị reset về rỗng ngay." |
| **B6. Run** | `dotnet run`.<br>Mở Postman test GET/POST. | "Rồi, project đã chạy. Thầy thử POST một bản ghi mới, sau đó GET lại xem nó có còn đó không nhé (chứng minh Singleton hoạt động)." |

---

## 4. Các API Endpoints

| Method | URL | Mô tả |
| :--- | :--- | :--- |
| `GET` | `/api/reservation` | Lấy danh sách tất cả |
| `GET` | `/api/reservation/{id}` | Lấy chi tiết theo ID |
| `POST` | `/api/reservation` | Tạo mới (kèm body JSON) |
| `PUT` | `/api/reservation` | Cập nhật toàn bộ (kèm body JSON) |
| `PATCH` | `/api/reservation/{id}` | Cập nhật 1 phần (kèm body Patch JSON) |
| `DELETE` | `/api/reservation/{id}` | Xóa theo ID |
