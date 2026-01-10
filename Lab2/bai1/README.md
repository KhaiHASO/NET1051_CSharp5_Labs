# Bài 1: Đăng ký thành viên (Register)

Dự án này thực hiện chức năng **Đăng ký thành viên** sử dụng ASP.NET Core Identity.

## Yêu cầu
- .NET 10.0 SDK
- SQL Server

## Cài đặt
1.  **Cấu hình Database**: File `appsettings.json` kết nối tới `Lab2_Bai1`.
2.  **Chạy lệnh**:
    ```bash
    dotnet ef database update
    dotnet run
    ```

## Chức năng
- **Register**: Tại menu, chọn **Register**.
    - Nhập Email, UserName, Password, ConfirmPassword.
    - Password yêu cầu: Tối thiểu 4 ký tự (cấu hình đơn giản).
    - Nếu thành công -> Redirect về trang chủ (hoặc Login nếu đã làm).
    - Nếu lỗi (trùng user, pass không khớp) -> Hiện thông báo lỗi.

## Kết quả
- Database `Lab2_Bai1` được tạo.
- Bảng `AspNetUsers` chứa thông tin người dùng đăng ký.
