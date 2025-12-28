# DemoSlide1 - Tài liệu chi tiết dự án ASP.NET Core Identity

Dự án này là một ví dụ minh họa toàn diện về cách xây dựng hệ thống xác thực và phân quyền trong ASP.NET Core MVC sử dụng **Identity** và **Entity Framework Core (Code First)**.

## 📚 Mục tiêu & Phạm vi
Dự án được thiết kế để giải thích các khái niệm:
1.  Cách tích hợp thư viện Identity vào dự án MVC.
2.  Cách hoạt động của Code First (từ Class ra Database).
3.  Cách bảo mật các Controller/Action cụ thể.
4.  Cách tùy biến giao diện Identity (Scaffolding).

---

## 🛠 Chi tiết Chức năng & Kiến thức Áp dụng

Dưới đây là giải thích chi tiết các chức năng có trong source code và kiến thức kỹ thuật tương ứng:

### 1. Hệ thống Xác thực (Authentication)
*Chức năng: Đăng ký, Đăng nhập, Đăng xuất, Quên mật khẩu.*

*   **Source Code**: Thư mục `Areas/Identity/Pages/Account/`.
*   **Kiến thức áp dụng**:
    *   **ASP.NET Core Identity System**: Sử dụng thư viện `Microsoft.AspNetCore.Identity.EntityFrameworkCore` để quản lý User, Password hashing (băm mật khẩu), và Cookie authentication.
    *   **Razor Pages**: Mặc dù dự án chính là MVC, nhưng Identity UI mặc định sử dụng Mô hình Razor Pages (PageModel) để xử lý logic đơn lẻ cho từng trang (ví dụ: `Login.cshtml.cs` xử lý logic POST đăng nhập).
    *   **Scaffolding**: Kỹ thuật sinh code tự động từ thư viện Identity ra source code để lập trình viên có thể chỉnh sửa giao diện và logic (thay vì dùng thư viện DLL đóng kín).

### 2. Hệ thống Phân quyền (Authorization)
*Chức năng: Ngăn chặn người dùng chưa đăng nhập truy cập vào trang chủ hoặc trang riêng tư.*

*   **Source Code**: `Controllers/HomeController.cs` và `Program.cs`.
*   **Kiến thức áp dụng**:
    *   **Middleware Pipeline (Program.cs)**:
        *   `app.UseAuthentication()`: Kích hoạt middleware xác thực (kiếm tra Cookie để biết "bạn là ai?").
        *   `app.UseAuthorization()`: Kích hoạt middleware phân quyền (kiểm tra xem "bạn có được phép vào đây không?").
        *   *Lưu ý*: Thứ tự khai báo cực kỳ quan trọng (Authentication phải đứng trước Authorization).
    *   **Attributes**: Sử dụng `[Authorize]` đặt trên class Controller hoặc Method.
        *   Trong `HomeController`, attribute này chặn toàn bộ truy cập nếu user chưa đăng nhập -> Tự động chuyển hướng về trang Login.

### 3. Quản lý Dữ liệu (Database / Code First)
*Chức năng: Lưu trữ thông tin người dùng, tự động tạo bảng trong SQL Server.*

*   **Source Code**: `Data/ApplicationDbContext.cs`, `appsettings.json`.
*   **Kiến thức áp dụng**:
    *   **EF Core Code First**: Phương pháp thiết kế database bắt đầu từ code C#. Ta viết class, EF Core sẽ sinh ra bảng.
    *   **IdentityDbContext**: Thay vì kế thừa `DbContext` thường, ta kế thừa `IdentityDbContext<IdentityUser>`. Class này chứa sẵn các `DbSet` cho các bảng hệ thống như: `AspNetUsers` (lưu user), `AspNetRoles` (lưu quyền), `AspNetUserClaims`...
    *   **Migrations**: Cơ chế version control cho database (`dotnet ef migrations add`, `dotnet ef database update`). Giúp đồng bộ cấu trúc code C# xuống SQL Server mà không cần viết lệnh SQL thủ công.
    *   **Dependency Injection (DI)**: Đăng ký DbContext vào hệ thống dịch vụ (Service Container) trong `Program.cs` để có thể inject vào Controller/View bất cứ đâu.

### 4. Giao diện & Trải nghiệm (UI/UX)
*Chức năng: Hiển thị giao diện Tiếng Việt, responsive, hiệu ứng đẹp mắt.*

*   **Source Code**: `wwwroot/css/site.css`, `Views/Shared/_Layout.cshtml`, `Views/Shared/_LoginPartial.cshtml`.
*   **Kiến thức áp dụng**:
    *   **Partial Views (`_LoginPartial`)**: Tách phần logic hiển thị nút "Đăng nhập/Đăng ký/Xin chào" ra một file riêng để tái sử dụng và giúp code Layout gọn gàng.
    *   **CSS Variables & Animations**: Sử dụng biến CSS (`:root`) để quản lý màu sắc đổi theme dễ dàng. Áp dụng `@keyframes` để làm hiệu ứng Fade-in khi chuyển trang.
    *   **Glassmorphism**: Kỹ thuật làm mờ nền (backdrop-filter) cho Navbar.
    *   **Tag Helpers**: Các thẻ đặc biệt của ASP.NET Core trong View (ví dụ: `asp-controller`, `asp-action`, `asp-route-...`) giúp tạo link URL sạch và binding dữ liệu form chính xác.

---

## ⚙️ Cài đặt và Chạy dự án

### 1. Yêu cầu môi trường
- **.NET SDK 8.0** trở lên.
- **SQL Server** (hoặc dùng bản nhẹ **LocalDB** đi kèm Visual Studio).

### 2. Cấu hình kết nối (Connection String)
Mở file `appsettings.json`, kiểm tra chuỗi kết nối:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DemoSlide1Db;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
*Bạn có thể đổi `Server=...` thành địa chỉ SQL Server của bạn nếu cần.*

### 3. Tạo Database (Quan trọng)
Vì sử dụng Code First, bạn cần chạy lệnh sau để sinh database lần đầu:

```bash
dotnet ef database update
```
*Lệnh này sẽ chạy các file migration đã có trong thư mục `Data/Migrations` để tạo bảng.*

### 4. Chạy ứng dụng
```bash
dotnet run
```
Truy cập trình duyệt tại: `http://localhost:5xxx` (hoặc cổng hiển thị trong terminal).

## 🧪 Kịch bản Test (Demo Script)
1.  **Vào trang chủ**: Sẽ bị chặn -> Chuyển hướng sang Login.
2.  **Đăng ký**: Tạo tài khoản mới (email + password).
    *   *Lưu ý*: Mật khẩu mặc định yêu cầu: Chữ hoa, chữ thường, số, ký tự đặc biệt.
3.  **Sau khi đăng ký**: Tự động đăng nhập -> Vào được Trang chủ.
4.  **Kiểm tra giao diện**: Thấy lời chào "Xin chào [User]!" trên menu.
5.  **Đăng xuất**: Quay lại trạng thái khách (Anonymous).

---
*Tác giả: Antigravity Agent (Google DeepMind)*
