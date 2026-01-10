# Dự án demo03 - Quản lý User và Role trong ASP.NET Core Identity

Dự án này là phiên bản nâng cao, kết hợp **Quản lý User (Người dùng)** và **Quản lý Role (Vai trò)**. Cho phép quản trị viên xem danh sách người dùng và gán/gỡ bỏ quyền (Roles) cho họ.

## Yêu cầu hệ thống

- .NET 10.0 SDK
- SQL Server

## Cài đặt và Chạy dự án

1.  **Cấu hình Database:**
    File `appsettings.json` kết nối tới `Lab2_Demo03`.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Lab2_Demo03;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```

2.  **Khởi tạo Database:**
    ```bash
    dotnet ef database update
    ```

3.  **Chạy ứng dụng:**
    ```bash
    dotnet run
    ```

## Tính năng và Demo

Truy cập các đường dẫn sau (hoặc dùng Menu trên thanh điều hướng):

1.  **Quản lý Roles:** (`/Administration/ListRoles`)
    - Tạo các Role mẫu (ví dụ: `Admin`, `Editor`, `User`).
    - *Lưu ý: Bạn cần tạo Role trước thì mới có thể gán cho User.*

2.  **Quản lý Users:** (`/User/Index`)
    - Hiển thị danh sách User cùng các Role họ đang sở hữu.
    - **Tạo User**: Sử dụng chức năng Register của Identity (nhấn Login/Register nếu có hoặc truy cập `/Identity/Account/Register` - Note: cần scaffold các trang này hoặc dùng User có sẵn nếu seeding). *Trong demo này, để đơn giản, code Register không được scaffold lại, bạn có thể thêm user bằng cách gõ URL nếu đã có trang register mvc hoặc insert DB, tuy nhiên cách tốt nhất là dùng UserController này để thêm Roles cho user đã đăng ký.*
    - *Thực tế trong bài này ta chưa scaffold trang Register, nhưng ta tập trung vào **Edit User**.*

3.  **Chỉnh sửa User (Gán Role):**
    - Tại danh sách User, nhấn **Edit**.
    - Cập nhật thông tin: Email, Username.
    - **Quan trọng**: Danh sách các Role sẽ hiện ra dưới dạng Checkbox.
    - Tích chọn Role muốn gán, bỏ tích để gỡ Role.
    - Nhấn **Update** để lưu thay đổi.

4.  **Xóa User:**
    - Nhấn **Delete** và xác nhận.

## Cấu trúc Code

- **Controllers**:
    - `UserController`: Xử lý thêm/sửa/xóa User và Logic gán Role (`AddToRolesAsync`, `RemoveFromRolesAsync`).
    - `AdministrationController`: Xử lý CRUD Role.
- **ViewModels**:
    - `UserViewModel`: Hiển thị bảng User.
    - `EditUserViewModel`: Chứa thông tin User và danh sách `RoleSelectionViewModel` (checkbox).
- **Logic Gán Role (UserController.Edit POST)**:
    - Lấy danh sách Role user đang có (`GetRolesAsync`).
    - So sánh với danh sách Role được chọn từ form.
    - Dùng `Except` để tìm ra các Role cần `Add` và các Role cần `Remove`.

## Kết quả Demo

- **Database**: `Lab2_Demo03`.
- **Kết quả mong đợi**:
    - [x] Tạo được Role.
    - [x] Hiển thị User.
    - [x] Vào trang Edit User thấy danh sách Checkbox Role.
    - [x] Tích chọn -> Update -> User có Role mới.
    - [x] Bỏ chọn -> Update -> User mất Role cũ.
