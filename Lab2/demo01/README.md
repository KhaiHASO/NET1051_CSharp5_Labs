# Dự án demo01 - ASP.NET Core Identity

Dự án này là một ví dụ minh họa về cách tích hợp và cấu hình **ASP.NET Core Identity** trong một ứng dụng ASP.NET Core MVC. Nó bao gồm các chức năng cơ bản như đăng ký, đăng nhập và quản lý người dùng với Entity Framework Core.

## Yêu cầu hệ thống

- .NET 10.0 SDK trở lên
- SQL Server (LocalDB hoặc SQL Server Express)

## Cài đặt và Chạy dự án

1.  **Clone hoặc tải dự án về máy:**
    Đảm bảo bạn đang ở trong thư mục `demo01`.

2.  **Cấu hình chuỗi kết nối:**
    Mở file `appsettings.json` và kiểm tra `DefaultConnection`. Mặc định nó sử dụng SQL Server LocalDB:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Lab2_Demo01;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```

3.  **Cài đặt các gói NuGet (đã thực hiện):**
    Dự án đã được cài đặt các thư viện cần thiết:
    - `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
    - `Microsoft.EntityFrameworkCore.SqlServer`
    - `Microsoft.EntityFrameworkCore.Tools`

4.  **Tạo cơ sở dữ liệu:**
    Chạy lệnh sau để áp dụng các migration và tạo database:
    ```bash
    dotnet ef database update
    ```

5.  **Chạy ứng dụng:**
    ```bash
    dotnet run
    ```
    Truy cập vào địa chỉ `https://localhost:5001` hoặc `http://localhost:5000` (tùy thuộc vào cấu hình launchSettings).

## Cấu trúc dự án

- **Data/ApplicationDbContext.cs**: Lớp ngữ cảnh cơ sở dữ liệu, kế thừa từ `IdentityDbContext<ApplicationUser>`, dùng để quản lý kết nối và các bảng Identity.
- **Models/ApplicationUser.cs**: Lớp mở rộng của `IdentityUser`, cho phép thêm các thuộc tính tùy chỉnh cho bảng User (ví dụ: FullName, Address, v.v.).
- **Program.cs**: Nơi cấu hình các dịch vụ (Services) và pipeline xử lý request.
    - Đăng ký `ApplicationDbContext` sử dụng SQL Server.
    - Đăng ký Identity với các cấu hình bảo mật (độ mạnh mật khẩu, khóa tài khoản).
    - Cấu hình pipeline cho MVC và Authentication/Authorization.

## Các tính năng chính (Cấu hình trong Program.cs)

- **Password Policy**:
    - Bắt buộc có chữ số.
    - Độ dài tối thiểu 8 ký tự.
    - Bắt buộc có chữ hoa và chữ thường.
- **Lockout Policy**:
    - Khóa tài khoản 15 phút sau 5 lần đăng nhập thất bại.

## Ghi chú

Nếu bạn gặp lỗi liên quan đến `IdentityUser` hoặc `EntityFrameworkCore`, hãy đảm bảo rằng bạn đã restore đầy đủ các gói NuGet bằng lệnh `dotnet restore`.

## Kết quả demo
database: Demo01_IdentityDB
bảng: AspNetUsers
3 cột FirstName, LastName, DateOfBirth mới xuất hiện. -> Kết thúc phần Demo 1 thành công