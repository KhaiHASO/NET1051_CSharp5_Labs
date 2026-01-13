# Demo 01 - Minh Họa Authentication trong ASP.NET Core MVC

Project này là một ví dụ đơn giản minh họa cơ chế **Authentication** và cách sử dụng attribute `[Authorize]` để bảo vệ tài nguyên trong ASP.NET Core.

## Mục Đích
Demo kịch bản người dùng chưa đăng nhập cố gắng truy cập vào một trang được bảo vệ (`Secured`) và bị hệ thống chặn lại (chuyển hướng hoặc báo lỗi).

## Yêu Cầu Hệ Thống
*   .NET SDK 10 (hoặc mới nhất)

## Hướng Dẫn Chạy
1.  Mở terminal tại thư mục gốc của project `Lab3/Demo01`.
2.  Chạy lệnh:
    ```bash
    dotnet run
    ```
3.  Truy cập trình duyệt theo địa chỉ hiển thị (thường là `http://localhost:5xxx`).

## Kịch Bản Demo (Test Flow)
Thực hiện các bước sau để kiểm chứng:

1.  **Trang Chủ (Index):**
    *   Bạn truy cập vào trang chủ bình thường (vì Action `Index` cho phép Anonymous).
    *   Trên giao diện có nút màu xanh **"Truy cập trang bảo mật (Secured)"**.

2.  **Thử Truy Cập Vùng Bảo Mật:**
    *   Nhấn vào nút **"Truy cập trang bảo mật"**.
    *   **Kết quả mong đợi:** Bạn **KHÔNG** thể vào được trang `Secured`.
    *   **Hiện tượng:** Hệ thống sẽ chuyển hướng bạn đến URL `/Account/Login` (do cấu hình trong `Program.cs`) và hiển thị lỗi 404 (vì ta chưa làm trang Login), hoặc hiển thị trang trắng/lỗi tùy trình duyệt.
    *   **Ý nghĩa:** Điều này chứng tỏ Middleware Authentication và attribute `[Authorize]` đã hoạt động: nó chặn request chưa xác thực và yêu cầu người dùng phải đăng nhập.

## Cấu Trúc Code Chính
*   **Program.cs:**
    *   `AddAuthentication().AddCookie(...)`: Đăng ký dịch vụ xác thực Cookie.
    *   `app.UseAuthentication()`: Kích hoạt Middleware xác thực.
*   **HomeController.cs:**
    *   `[Authorize]` trên action `Secured`: Đánh dấu action này cần bảo vệ.
    *   `[AllowAnonymous]` trên action `Index`: Cho phép truy cập tự do.
