# Hướng dẫn chi tiết Demo01: Routing trong ASP.NET Core Web API

Dự án này minh họa cách sử dụng **Attribute Routing** trong ASP.NET Core Web API (.NET 10), tương ứng với nội dung Slide 1-18 của môn học.

## 1. Mục tiêu Demo
- Hiểu cơ chế **Data Navigation (Routing)** để điều hướng request từ Client đến đúng Action trong Controller.
- Minh họa cách cấu hình Routing trong `Program.cs` và sử dụng Attributes `[Route]`, `[HttpGet]` trong Controller.
- Sử dụng Swagger để test API.

## 2. Hướng dẫn cài đặt và chạy
1.  **Yêu cầu:** Đã cài đặt .NET 10 SDK và SQL Server LocalDB.
2.  **Mở dự án:** Mở thư mục `demo01` bằng Visual Studio hoặc VS Code.
3.  **Cấu hình Database:**
    - Chuỗi kết nối đã được cấu hình sẵn trong `appsettings.json`:
      ```json
      "ConnectionStrings": {
        "CSharp5Slide7Demo01": "Server=(localdb)\\mssqllocaldb;Database=CSharp5Slide7Demo01;Trusted_Connection=True;"
      }
      ```
    - Database đã được khởi tạo tự động (nếu bạn đã chạy lệnh update database). Nếu chưa, chạy lệnh:
      ```powershell
      dotnet ef database update
      ```
4.  **Chạy ứng dụng:**
    - Nhấn F5 (Visual Studio) hoặc chạy lệnh `dotnet run` trong terminal.
    - Truy cập Swagger UI tại địa chỉ hiển thị trong terminal (thường là `http://localhost:xxxx/swagger`).

## 3. Kịch bản Demo (Script cho Giảng viên/Sinh viên)

**Bước 1: Show cấu hình trong `Program.cs`**
- Mở file `Program.cs`.
- Chỉ vào dòng `builder.Services.AddControllers();`: Đăng ký dịch vụ Controller.
- Chỉ vào dòng `app.MapControllers();`: Middleware quan trọng nhất để kích hoạt Attribute Routing. Giải thích rằng nếu thiếu dòng này, API sẽ không hoạt động.

**Bước 2: Show code `EmployeeController.cs`**
- Mở file `Controllers/EmployeeController.cs`.
- Giải thích Attribute `[ApiController]`: Đánh dấu class là API Controller.
- **Action `GetAllEmployees`**:
  - Chỉ vào `[Route("Emp/All")]`: Định nghĩa đường dẫn URL cố định.
  - Chỉ vào `[HttpGet]`: Chỉ định phương thức HTTP GET.
- **Action `GetEmployeeById`**:
  - Chỉ vào `[Route("Emp/ById/{Id}")]`: Định nghĩa đường dẫn có tham số động `{Id}`.
  - Giải thích biến `int Id` trong tham số hàm sẽ tự động binding với `{Id}` trên URL.

**Bước 3: Test Demo** (Khuyên dùng Swagger hoặc Postman)
- **Test GetAllEmployees:**
  - Gửi request `GET` tới `/Emp/All`.
  - Kết quả mong đợi: Chuỗi "Response from GetAllEmployees Method".
- **Test GetEmployeeById:**
  - Gửi request `GET` tới `/Emp/ById/99` (thử thay số 99 bằng số khác).
  - Kết quả mong đợi: Chuỗi "Response from GetEmployeeById Method Id: 99".

---
**Lưu ý:** Source code sử dụng .NET 10 và tuân thủ các quy chuẩn naming convention được yêu cầu.
