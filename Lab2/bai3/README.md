# Bài 3: Admin Role Management

Dự án này thực hiện chức năng **Quản lý quyền (Roles)** của người dùng.

## Yêu cầu
- .NET 10.0 SDK
- SQL Server

## Cài đặt
1.  **Cấu hình Database**: `appsettings.json` kết nối tới `Lab2_Bai3`.
2.  **Khởi tạo Database**:
    ```bash
    dotnet ef database update
    ```
3.  **Chạy lệnh**:
    ```bash
    dotnet run
    ```

## Chức năng
- **Quản lý Roles**: Truy cập menu "Admin Users".
- **Danh sách User**: Hiển thị bảng user với các role hiện tại.
- **Manage Roles**:
    - Nhấn nút "Manage Roles" ở từng user.
    - Hiển thị danh sách Checkbox Role.
    - Tích chọn để gán, bỏ tích để gỡ.
- **Tạo Role (Test)**: Nút "Create Role (Test)" để tạo nhanh role phục vụ kiểm thử.

## Logic Implementation
- Sử dụng `UserManager.GetRolesAsync`, `AddToRolesAsync`, `RemoveFromRolesAsync` để xử lý danh sách role được chọn.
