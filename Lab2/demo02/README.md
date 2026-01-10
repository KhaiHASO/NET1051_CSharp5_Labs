# Dự án demo02 - Quản lý Role trong ASP.NET Core Identity

Dự án này mở rộng từ `demo01`, bổ sung tính năng **Quản lý Role (Vai trò)**. Cho phép tạo, xem, sửa, và xóa các Role trong hệ thống Identity.

## Yêu cầu hệ thống

- .NET 10.0 SDK trở lên
- SQL Server

## Cài đặt và Chạy dự án

1.  **Cấu hình Database:**
    File `appsettings.json` đã được cấu hình trỏ đến database `Lab2_Demo02`.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Lab2_Demo02;Trusted_Connection=True;MultipleActiveResultSets=true"
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

## Hướng dẫn sử dụng Demo

Truy cập vào các đường dẫn sau để kiểm tra tính năng:

1.  **Danh sách Roles:**
    - URL: `/Administration/ListRoles`
    - Tại đây bạn có thể thấy danh sách các role hiện có.
    - Nếu chưa có role nào, nút "Create Role" sẽ xuất hiện.

2.  **Tạo Role mới:**
    - URL: `/Administration/CreateRole`
    - Nhập tên Role (ví dụ: `Admin`, `User`, `Manager`) và nhấn Create.
    - Hệ thống sẽ kiểm tra xem Role đã tồn tại chưa trước khi tạo.

3.  **Chỉnh sửa Role:**
    - Tại màn hình List Roles, nhấn nút **Edit**.
    - Bạn có thể đổi tên Role.
    - Danh sách các User thuộc Role này sẽ được hiển thị bên dưới (hiện tại chưa có chức năng thêm User vào Role trong demo này, chỉ hiển thị).

4.  **Xóa Role:**
    - Tại màn hình List Roles, nhấn nút **Delete**.
    - Xác nhận xóa để loại bỏ Role khỏi database.

## Kết quả Demo

- **Database**: `Lab2_Demo02` được tạo.
- **Bảng AspNetRoles**: Lưu trữ thông tin các role đã tạo.
- **Chức năng**:
    - [x] Tạo Role (Create)
    - [x] Xem danh sách Role (Read)
    - [x] Sửa tên Role (Update)
    - [x] Xóa Role (Delete)
    - [x] Hiển thị User thuộc Role (trong trang Edit)

## Cấu trúc Code

- **ViewModels**:
    - `CreateRoleViewModel`: Dùng cho form tạo mới.
    - `EditRoleViewModel`: Dùng cho form chỉnh sửa, chứa danh sách Users.
- **Controllers**:
    - `AdministrationController`: Chứa các Action `CreateRole`, `ListRoles`, `EditRole`, `DeleteRole`.
    - Sử dụng `RoleManager<IdentityRole>` để thao tác với Role.
    - Sử dụng `UserManager<ApplicationUser>` để lấy thông tin User thuộc Role.
- **Views**:
    - `Views/Administration/`: Chứa các file `.cshtml` giao diện.
