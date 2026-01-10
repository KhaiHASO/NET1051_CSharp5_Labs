# Bài 4: Admin Dashboard

Dự án này là bài nâng cao, xây dựng **Admin Dashboard** chuyên nghiệp sử dụng **Areas** trong ASP.NET Core.

## Yêu cầu
- .NET 10.0 SDK
- SQL Server

## Cài đặt
1.  **Cấu hình Database**: `Appsettings.json` kết nối tới `Lab2_Bai4`.
2.  **Khởi tạo Database**:
    ```bash
    dotnet ef database update
    ```
3.  **Chạy lệnh**:
    ```bash
    dotnet run
    ```

## Chức năng
- **Dashboard Area**: Truy cập `/Admin` hoặc `/Admin/Dashboard`.
- **Giao diện Admin**:
    - Sidebar menu bên trái (Dashboard, User Mgt, Role Mgt).
    - Topbar hiển thị thông tin Admin.
    - Thống kê (Summary Cards): Total Users, Total Roles, Unconfirmed Users.
- **Cấu trúc Project**:
    - Sử dụng `Area` tên là `Admin`.
    - Layout riêng `_AdminLayout.cshtml` sử dụng Bootstrap 5 và FontAwesome.
    - Controller `DashboardController` lấy dữ liệu thống kê thực từ `UserManager`.

## Mở rộng
- Để tích hợp User/Role Management vào đây, bạn có thể copy Controller từ Bài 3 vào `Areas/Admin/Controllers` và chỉnh namespace, routing.
