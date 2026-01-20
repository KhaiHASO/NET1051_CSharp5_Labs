# Demo 1: Tạo RESTful API với Scaffolding và Entity Framework Core

Dự án này (`Demo01`) là một ví dụ minh họa cho bài học "Lesson 6: ASP.NET Core Web API - SQL". Nó thể hiện cách tạo một API quản lý đặt chỗ (`Reservation`) sử dụng kỹ thuật Code First và mô phỏng kết quả của quá trình Scaffolding.

## 1. Giải thích Code

*   **Model (`Reservation.cs`)**: Đây là lớp thực thể đại diện cho bảng `Reservations` trong cơ sở dữ liệu.
    *   Các thuộc tính `Name`, `StartLocation`, `EndLocation` được cấu hình với độ dài tối đa là 250 ký tự (`MaxLength(250)`).
    *   Trong `ApplicationDbContext`, chúng được cấu hình là `IsUnicode(false)` để tương ứng với kiểu dữ liệu `varchar` trong SQL Server (thay vì `nvarchar` mặc định).
*   **DbContext (`ApplicationDbContext.cs`)**: Lớp này đóng vai trò cầu nối giữa ứng dụng và SQL Server. Nó kế thừa từ `DbContext` và chứa `DbSet<Reservation>` để truy xuất dữ liệu.
*   **Controller (`ReservationsController.cs`)**: Chứa các phương thức API (Action Methods) để thực hiện các thao tác CRUD (Create, Read, Update, Delete). Controller này mô phỏng kết quả khi bạn chọn "Add > Controller > API Controller with actions, using Entity Framework" trong Visual Studio.

## 2. Khái niệm Scaffolding

**Scaffolding** (Giàn giáo) trong ASP.NET Core là kỹ thuật tạo code tự động. Thay vì lập trình viên phải tự viết tay từng dòng code cho Model, Context, và Controller để thực hiện các thao tác CRUD cơ bản, Visual Studio cung cấp công cụ để "sinh" (generate) ra các đoạn code này dựa trên các Model đã định nghĩa.

*   **Lợi ích**: Tiết kiệm thời gian, tạo ra cấu trúc chuẩn.
*   **Thực tế**: Trong Demo này, chúng ta viết code thủ công (hoặc copy) để hiểu rõ cấu trúc mà Scaffolding tạo ra.

## 3. Cài đặt và Cấu hình Swagger

Để tích hợp Swagger (OpenAPI) vào dự án, chúng ta thực hiện các bước sau:

1.  **Cài đặt Package**:
    Cài đặt thư viện `Swashbuckle.AspNetCore` từ NuGet:
    ```powershell
    dotnet add package Swashbuckle.AspNetCore
    ```

2.  **Đăng ký Service**:
    Trong `Program.cs`, thêm các service sau vào container:
    ```csharp
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    ```

3.  **Cấu hình Middleware**:
    Kích hoạt Swagger Middleware trong pipeline xử lý request (thường trong môi trường Development):
    ```csharp
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    ```

## 4. Kịch bản Demo 

Thực hiện các bước sau để demo

### Bước 1: Tạo Database (Migration)
Mở **Package Manager Console** (hoặc Terminal) và chạy các lệnh sau để tạo database `CSharp5Slide6Demo01`:

```powershell
# Tạo Migration đầu tiên
dotnet ef migrations add InitialCreate

# Cập nhật Database
dotnet ef database update
```

### Bước 2: Chạy ứng dụng
Nhấn **F5** hoặc chạy lệnh `dotnet run`. Trình duyệt sẽ mở giao diện **Swagger UI** tại địa chỉ `https://localhost:<port>/swagger/index.html`.

### Bước 3: Test thêm dữ liệu (POST)
1.  Trên Swagger UI, mở API `POST /api/Reservations`.
2.  Nhấn **Try it out**.
3.  Nhập dữ liệu JSON mẫu:
    ```json
    {
      "name": "Thepv",
      "startLocation": "Tay Nguyen",
      "endLocation": "Sai Gon"
    }
    ```
4.  Nhấn **Execute** và kiểm tra kết quả trả về (Code 201 Created).

### Bước 4: Test xem dữ liệu (GET)
1.  Mở API `GET /api/Reservations`.
2.  Nhấn **Try it out** > **Execute**.
3.  Kết quả sẽ hiển thị danh sách các Reservation vừa thêm.

### Bước 5: Kiểm tra trong SQL Server
1.  Mở **SQL Server Management Studio (SSMS)**.
2.  Kết nối tới server `.` (hoặc `(local)`).
3.  Tìm database `CSharp5Slide6Demo01`.
4.  Query bảng `Reservations` để chứng minh dữ liệu đã được lưu thành công với kiểu `varchar(250)`.
