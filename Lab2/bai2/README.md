# Bài 2: Đổi mật khẩu (Change Password)

Dự án này thực hiện chức năng **Đổi mật khẩu** cho người dùng đã đăng nhập.

## Yêu cầu
- .NET 10.0 SDK
- SQL Server

## Cài đặt
1.  **Cấu hình Database**: File `appsettings.json` kết nối tới `Lab2_Bai2`.
2.  **Khởi tạo Database**:
    ```bash
    dotnet ef database update
    ```
3.  **Chạy lệnh**:
    ```bash
    dotnet run
    ```

## Chức năng
- **Change Password**:
    - Yêu cầu người dùng phải đăng nhập trước (Attribute `[Authorize]`).
    - Nhập Mật khẩu cũ, Mật khẩu mới, Xác nhận mật khẩu mới.
    - Hệ thống kiểm tra mật khẩu cũ có đúng không.
    - Nếu thành công -> Thông báo "Đổi thành công".
    - Login lại với mật khẩu mới không bị văng session (dùng `RefreshSignInAsync`).

## Lưu ý
- Để test, bạn cần có user trong database (có thể dùng code Register từ Bài 1 hoặc seed data).
- Nếu chưa đăng nhập, truy cập `/Account/ChangePassword` sẽ bị redirect về Login.
