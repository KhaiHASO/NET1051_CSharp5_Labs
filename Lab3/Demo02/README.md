# Demo 02 - Xác Thực Người Dùng với ASP.NET Core Identity

Project này minh họa cách triển khai **Authentication** hoàn chỉnh sử dụng thư viện **ASP.NET Core Identity** và **SQL Server**.

## Tính Năng Chính
*   Đăng ký dịch vụ Identity.
*   Trang đăng nhập tùy chỉnh (`/DangNhap`).
*   Bảo vệ trang Web bằng `[Authorize]`.
*   Hiển thị thông tin người dùng đã đăng nhập.
*   Chức năng Đăng xuất.

## Yêu Cầu Hệ Thống
*   .NET SDK 10 (hoặc mới nhất).
*   SQL Server (LocalDB hoặc bản đầy đủ).

## Hướng Dẫn Cài Đặt & Chạy

### 1. Cấu Hình Database
Connection String đã được cấu hình trong `appsettings.json`. Mặc định trỏ tới `Server=.;Database=Slide3Demo02`.

### 2. Tạo Database (Migrations)
Mở terminal tại thư mục gốc của project `Lab3/Demo02` và chạy các lệnh:

```bash
# Tạo Migration lần đầu
dotnet ef migrations add InitialCreate

# Cập nhật Database
dotnet ef database update
```
Lệnh trên sẽ tạo Database `Slide3Demo02` và các bảng Identity cần thiết.

### 3. Chạy Ứng Dụng
```bash
dotnet run
```

## Tài Khoản Mẫu (Seed Data)
Hệ thống **TỰ ĐỘNG** tạo một tài khoản mẫu khi chạy lần đầu:
*   **Email:** `admin@example.com`
*   **Password:** `Admin@123`

## Kịch Bản Demo (Test Flow)
1.  Truy cập trang chủ.
2.  Cố gắng truy cập trang **Secured**: 
    *   Bạn sẽ bị chuyển hướng sang `/DangNhap` (do chưa có cookie).
3.  Tại trang Đăng Nhập:
    *   Nhập Email: `admin@example.com`
    *   Nhập Password: `Admin@123`
    *   Nhấn **Đăng Nhập**.
4.  Nếu thành công:
    *   Bạn được chuyển hướng về trang chủ hoặc trang Secured.
    *   Truy cập lại trang Secured sẽ thấy lời chào "Xin chào admin@example.com!".
5.  Nhấn nút **Đăng Xuất** để thoát phiên làm việc.
