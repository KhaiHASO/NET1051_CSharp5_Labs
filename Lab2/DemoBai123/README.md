# DemoBai123 - ASP.NET Core Identity Demo (Feature Folders)

## 📋 Mô tả

Source code demo cho môn **NET1051 – Lập trình C# 5**, Lab 2. Project được tổ chức theo **Feature Folders** với 3 Bài riêng biệt nhưng vẫn dùng chung 1 project để dữ liệu Identity liên thông.

## 🎯 Cấu trúc Feature Folders

Project được tổ chức theo các Bài (Bai1, Bai2, Bai3) trong thư mục `Features/`:

```
DemoBai123/
├─ Features/
│  ├─ Bai1_Register/              # Bài 1: UserManager - Register
│  │  └─ RegisterVm.cs
│  ├─ Bai2_Auth_ChangePassword/   # Bài 2: SignInManager & ChangePassword
│  │  ├─ LoginVm.cs
│  │  └─ ChangePasswordVm.cs
│  └─ Bai3_Admin_Roles/           # Bài 3: RoleManager - CRUD Roles
│     ├─ CreateRoleVm.cs
│     ├─ EditRoleVm.cs
│     └─ UserRoleVm.cs
├─ Controllers/
│  ├─ AccountController.cs        # Xử lý Bai1 & Bai2
│  └─ AdminController.cs          # Xử lý Bai3
├─ Data/
│  ├─ ApplicationDbContext.cs     # DbContext dùng chung
│  └─ SeedData.cs                 # Seed dữ liệu ban đầu
├─ Models/
│  └─ ApplicationUser.cs          # User model dùng chung
├─ Views/
│  ├─ Account/                    # Views cho Bai1 & Bai2
│  └─ Admin/                      # Views cho Bai3
├─ Program.cs
├─ appsettings.json
└─ README.md
```

## 📚 Các Bài học

### Bài 1: Register (UserManager)

**Chức năng:**
- Form đăng ký: Username, Email, Password, ConfirmPassword
- Check trùng username/email
- Sử dụng `UserManager.CreateAsync`
- Thành công redirect sang Login

**Routes:**
- `GET /Account/Register` - Hiển thị form đăng ký
- `POST /Account/Register` - Xử lý đăng ký

**File liên quan:**
- `Features/Bai1_Register/RegisterVm.cs`
- `Controllers/AccountController.cs` (phần Register)
- `Views/Account/Register.cshtml`

### Bài 2: Login + ChangePassword

**Chức năng:**
- Login bằng `SignInManager.PasswordSignInAsync`
- ChangePassword có `[Authorize]`
- Sử dụng `UserManager.ChangePasswordAsync`

**Routes:**
- `GET /Account/Login` - Hiển thị form đăng nhập
- `POST /Account/Login` - Xử lý đăng nhập
- `GET /Account/ChangePassword` - Hiển thị form đổi mật khẩu (yêu cầu đăng nhập)
- `POST /Account/ChangePassword` - Xử lý đổi mật khẩu

**File liên quan:**
- `Features/Bai2_Auth_ChangePassword/LoginVm.cs`
- `Features/Bai2_Auth_ChangePassword/ChangePasswordVm.cs`
- `Controllers/AccountController.cs` (phần Login & ChangePassword)
- `Views/Account/Login.cshtml`
- `Views/Account/ChangePassword.cshtml`

### Bài 3: Admin Role Management

**Chức năng:**
- Role CRUD (Create/List/Edit/Delete)
- Gán/gỡ role cho user (`AddToRoleAsync`/`RemoveFromRoleAsync`)
- Chỉ Admin truy cập được (`[Authorize(Roles="Admin")]`)

**Routes:**
- `GET /Admin/ListRoles` - Danh sách roles
- `GET /Admin/CreateRole` - Form tạo role mới
- `POST /Admin/CreateRole` - Xử lý tạo role
- `GET /Admin/EditRole/{id}` - Form chỉnh sửa role
- `POST /Admin/EditRole` - Xử lý cập nhật role
- `GET /Admin/DeleteRole/{id}` - Form xác nhận xóa role
- `POST /Admin/DeleteRole` - Xử lý xóa role
- `GET /Admin/ListUsers` - Danh sách users
- `GET /Admin/ManageUserRoles?userId={id}` - Quản lý roles cho user
- `POST /Admin/ManageUserRoles` - Xử lý cập nhật roles của user

**File liên quan:**
- `Features/Bai3_Admin_Roles/CreateRoleVm.cs`
- `Features/Bai3_Admin_Roles/EditRoleVm.cs`
- `Features/Bai3_Admin_Roles/UserRoleVm.cs`
- `Controllers/AdminController.cs`
- `Views/Admin/*.cshtml`

## 🛠️ Công nghệ sử dụng

- **Framework**: ASP.NET Core MVC (.NET 10.0)
- **Identity**: ASP.NET Core Identity
- **Database**: SQL Server LocalDB
- **ORM**: Entity Framework Core
- **UI**: Bootstrap 5

## 🚀 Hướng dẫn cài đặt và chạy

### Bước 1: Cài đặt .NET SDK

Đảm bảo bạn đã cài đặt .NET 10.0 SDK hoặc mới hơn.

Kiểm tra phiên bản:
```bash
dotnet --version
```

### Bước 2: Tạo project và cài đặt packages

```bash
# Di chuyển vào thư mục project
cd DemoBai123

# Restore packages
dotnet restore
```

**Packages đã được cấu hình trong `DemoBai123.csproj`:**
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (10.0.0)
- `Microsoft.EntityFrameworkCore.SqlServer` (10.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (10.0.0)

### Bước 3: Tạo Migration và Database

```bash
# Tạo migration đầu tiên
dotnet ef migrations add InitialCreate

# Cập nhật database
dotnet ef database update
```

**Lưu ý**: Nếu chưa cài đặt EF Core Tools, chạy lệnh:
```bash
dotnet tool install --global dotnet-ef
```

**Database sẽ được tự động seed với:**
- **Roles**: `Admin`, `User`
- **Admin account**: 
  - Username: `admin`
  - Password: `123456`
  - Email: `admin@example.com`

### Bước 4: Chạy ứng dụng

```bash
dotnet run
```

Hoặc nếu muốn chạy với hot reload:
```bash
dotnet watch run
```

Ứng dụng sẽ chạy tại: `https://localhost:5001` hoặc `http://localhost:5000`

## 🔐 Tài khoản mặc định

Sau khi chạy migration và seed data, bạn có thể đăng nhập với:

- **Username**: `admin`
- **Password**: `123456`
- **Role**: `Admin`

## 📝 Routes Test

### Bài 1: Register

1. **GET /Account/Register**
   - Hiển thị form đăng ký
   - Test: Điền thông tin và đăng ký user mới

2. **POST /Account/Register**
   - Xử lý đăng ký
   - Test: Thử đăng ký với username/email trùng để kiểm tra validation

### Bài 2: Login + ChangePassword

3. **GET /Account/Login**
   - Hiển thị form đăng nhập
   - Test: Đăng nhập với admin/123456

4. **POST /Account/Login**
   - Xử lý đăng nhập
   - Test: Thử đăng nhập với thông tin sai để kiểm tra error handling

5. **GET /Account/ChangePassword**
   - Hiển thị form đổi mật khẩu (yêu cầu đăng nhập)
   - Test: Đăng nhập trước, sau đó truy cập route này

6. **POST /Account/ChangePassword**
   - Xử lý đổi mật khẩu
   - Test: Đổi mật khẩu và đăng nhập lại với mật khẩu mới

### Bài 3: Admin Role Management

7. **GET /Admin/ListRoles**
   - Danh sách tất cả roles
   - Test: Đăng nhập với admin, truy cập route này

8. **GET /Admin/CreateRole**
   - Form tạo role mới
   - Test: Tạo role mới (ví dụ: "Manager")

9. **GET /Admin/EditRole/{id}**
   - Form chỉnh sửa role
   - Test: Chỉnh sửa tên role

10. **GET /Admin/DeleteRole/{id}**
    - Form xác nhận xóa role
    - Test: Xóa role (lưu ý: không xóa Admin và User)

11. **GET /Admin/ListUsers**
    - Danh sách tất cả users
    - Test: Xem danh sách users

12. **GET /Admin/ManageUserRoles?userId={id}**
    - Quản lý roles cho user
    - Test: Gán/gỡ roles cho user

13. **POST /Admin/ManageUserRoles**
    - Xử lý cập nhật roles của user
    - Test: Chọn/bỏ chọn roles và cập nhật

## 🔧 Cấu hình Database

Connection string được cấu hình trong `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DemoBai123Db;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0"
  }
}
```

Database sẽ được tạo tự động với tên `DemoBai123Db` trong SQL Server LocalDB.

## 📖 Hướng dẫn sử dụng chi tiết

### Test Bài 1: Register

1. Truy cập `/Account/Register`
2. Điền thông tin:
   - Username: `testuser`
   - Email: `test@example.com`
   - Password: `123456`
   - ConfirmPassword: `123456`
3. Click "Đăng ký"
4. Kiểm tra: Redirect sang Login page
5. Thử đăng ký lại với cùng username/email để kiểm tra validation

### Test Bài 2: Login + ChangePassword

1. Truy cập `/Account/Login`
2. Đăng nhập với:
   - Username: `admin`
   - Password: `123456`
3. Sau khi đăng nhập thành công, truy cập `/Account/ChangePassword`
4. Đổi mật khẩu:
   - OldPassword: `123456`
   - NewPassword: `newpass123`
   - ConfirmPassword: `newpass123`
5. Đăng xuất và đăng nhập lại với mật khẩu mới

### Test Bài 3: Admin Role Management

1. Đăng nhập với tài khoản Admin (`admin`/`123456`)
2. Truy cập `/Admin/ListRoles` để xem danh sách roles
3. Click "Tạo Role mới" để tạo role mới (ví dụ: "Manager")
4. Click "Sửa" để chỉnh sửa role
5. Truy cập `/Admin/ListUsers` để xem danh sách users
6. Click "Phân quyền" để gán/gỡ roles cho user
7. Chọn/bỏ chọn roles và click "Cập nhật"

## 🐛 Xử lý lỗi thường gặp

### Lỗi: "Cannot open database"

**Nguyên nhân**: Database chưa được tạo hoặc LocalDB chưa được khởi động.

**Giải pháp**:
```bash
# Tạo lại migration và database
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Lỗi: "Login failed for user"

**Nguyên nhân**: Connection string không đúng hoặc LocalDB chưa được cài đặt.

**Giải pháp**: Kiểm tra lại connection string trong `appsettings.json` và đảm bảo SQL Server LocalDB đã được cài đặt.

### Lỗi: "Access Denied" khi truy cập Admin routes

**Nguyên nhân**: User hiện tại không có role "Admin".

**Giải pháp**: Đăng nhập với tài khoản admin (username: `admin`, password: `123456`)

## 📚 Tài liệu tham khảo

- [ASP.NET Core Identity Documentation](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity)
- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [ASP.NET Core MVC Documentation](https://learn.microsoft.com/en-us/aspnet/core/mvc/)

## 🎓 Điểm khác biệt với DemoSlide2

1. **Tổ chức code**: Theo Feature Folders (Bai1, Bai2, Bai3) thay vì Controllers/ViewModels riêng biệt
2. **Routes**: Sử dụng `/Admin/*` thay vì `/Administration/*`
3. **Cấu trúc**: Mỗi Bài có thư mục riêng trong `Features/`
4. **Dữ liệu**: Vẫn dùng chung ApplicationDbContext và ApplicationUser để đảm bảo Identity liên thông

## 📄 License

Source code này được tạo cho mục đích giảng dạy và học tập.

## 👨‍💻 Tác giả

Created for NET1051 - C# 5 Course - Lab 2

---

**Lưu ý**: Source code này được thiết kế để demo và học tập. Không nên sử dụng trong môi trường production mà không có các biện pháp bảo mật bổ sung.

