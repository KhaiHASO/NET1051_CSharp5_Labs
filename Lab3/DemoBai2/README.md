# DemoBai2: Login & Change Password với ASP.NET Core Identity

Demo này minh họa cách tùy chỉnh chức năng **Login** và **Change Password** sử dụng ASP.NET Core Identity nhưng với Controller và View riêng (không dùng UI mặc định của Identity Razor Pages).

## 🚀 Tính năng

1.  **Login (Đăng nhập)**
    *   Sử dụng `SignInManager<IdentityUser>`.
    *   Validate input (Email, Password).
    *   Hỗ trợ `ReturnUrl` để quay lại trang trước đó sau khi login.

2.  **Change Password (Đổi mật khẩu)**
    *   Yêu cầu đăng nhập (`[Authorize]`).
    *   Sử dụng `UserManager<IdentityUser>`.
    *   Xác thực mật khẩu cũ.

## 🛠️ Cài đặt & Chạy

1.  **Clone project và di chuyển vào thư mục:**
    ```bash
    cd DemoBai2
    ```

2.  **Cấu hình Database:**
    Project sử dụng **SQL Server LocalDB**.
    Chuỗi kết nối trong `appsettings.json`:
    ```json
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DemoBai2Db;Trusted_Connection=True;MultipleActiveResultSets=true"
    ```

3.  **Tạo Database:**
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

4.  **Chạy ứng dụng:**
    ```bash
    dotnet run
    ```
    Truy cập: `https://localhost:5001` (hoặc port tương ứng).

## 🧪 Hướng dẫn Test

### 1. Đăng ký tài khoản (Register)
*   Truy cập `/Identity/Account/Register` (UI mặc định của Identity) để tạo user mới.
*   Ví dụ: `test@example.com` / `Password123!`

### 2. Test Login
*   Truy cập `/Account/Login`.
*   Nhập Email/Password sai -> Thông báo lỗi.
*   Nhập đúng -> Redirect về trang chủ.

### 3. Test Change Password
*   Đăng nhập thành công.
*   Truy cập `/Account/ChangePassword`.
*   Nhập mật khẩu cũ sai -> Lỗi.
*   Nhập mật khẩu mới không khớp -> Lỗi.
*   Đổi thành công -> Thông báo thành công.

## 📂 Cấu trúc Code

*   **Controllers/AccountController.cs**: Xử lý logic Login và ChangePassword.
*   **Models/LoginVm.cs**: ViewModel cho Login.
*   **Models/ChangePasswordVm.cs**: ViewModel cho ChangePassword.
*   **Views/Account/Login.cshtml**: Giao diện đăng nhập.
*   **Views/Account/ChangePassword.cshtml**: Giao diện đổi mật khẩu.

## 📝 Ghi chú

*   Project đã bỏ qua `app.db` (SQLite) và sử dụng SQL Server LocalDB.
*   `.gitignore` đã được cấu hình để bỏ qua các file tạm và build artifacts.
