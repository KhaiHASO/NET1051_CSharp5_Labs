# DemoSlide2 - ASP.NET Core Identity Demo

## 📋 Mô tả

Source code demo cho môn **NET1051 – Lập trình C# 5**, slide "Bài 2: UserManager & Role".

## 🎯 Chức năng

### 1. Account Management (UserManager)
- ✅ Đăng ký user mới (Register)
- ✅ Đăng nhập (Login)
- ✅ Đăng xuất (Logout)
- ✅ Đổi mật khẩu (Change Password) - yêu cầu đăng nhập

### 2. User Management (CRUD)
- ✅ Danh sách users
- ✅ Chỉnh sửa user (email/username)
- ✅ Xóa user

### 3. Role Management (RoleManager) - Admin only
- ✅ Tạo Role
- ✅ Danh sách Roles
- ✅ Chỉnh sửa Role
- ✅ Xóa Role
- ✅ Thêm/Xóa Role cho User

## 🛠️ Công nghệ sử dụng

- **Framework**: ASP.NET Core MVC (.NET 10.0)
- **Identity**: ASP.NET Core Identity
- **Database**: SQL Server LocalDB
- **ORM**: Entity Framework Core
- **UI**: Bootstrap 5

## 📁 Cấu trúc Project

```
DemoSlide2/
├─ Controllers/
│  ├─ AccountController.cs          # Xử lý đăng ký, đăng nhập, đổi mật khẩu
│  ├─ UsersController.cs            # CRUD users
│  └─ AdministrationController.cs   # Quản lý roles (Admin only)
├─ Data/
│  ├─ ApplicationDbContext.cs       # DbContext cho Identity
│  └─ SeedData.cs                    # Seed dữ liệu ban đầu
├─ Models/
│  └─ ApplicationUser.cs             # Custom User model
├─ ViewModels/
│  ├─ RegisterVm.cs                  # ViewModel đăng ký
│  ├─ ChangePasswordVm.cs            # ViewModel đổi mật khẩu
│  ├─ CreateRoleVm.cs                # ViewModel tạo role
│  ├─ EditRoleVm.cs                  # ViewModel chỉnh sửa role
│  └─ UserRoleVm.cs                  # ViewModel quản lý roles của user
├─ Views/
│  ├─ Account/                       # Views cho Account
│  ├─ Users/                         # Views cho User management
│  └─ Administration/                # Views cho Role management
├─ Migrations/                       # EF Core migrations
├─ Program.cs                        # Cấu hình ứng dụng
└─ appsettings.json                  # Cấu hình database
```

## 🚀 Hướng dẫn cài đặt và chạy

### Bước 1: Cài đặt .NET SDK

Đảm bảo bạn đã cài đặt .NET 10.0 SDK hoặc mới hơn.

Kiểm tra phiên bản:
```bash
dotnet --version
```

### Bước 2: Cài đặt packages

Project đã được cấu hình sẵn các packages cần thiết trong `DemoSlide2.csproj`:
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (10.0.0)
- `Microsoft.EntityFrameworkCore.SqlServer` (10.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (10.0.0)

Nếu cần restore packages:
```bash
cd DemoSlide2
dotnet restore
```

### Bước 3: Tạo Migration và Database

Tạo migration đầu tiên:
```bash
dotnet ef migrations add InitialCreate
```

Cập nhật database:
```bash
dotnet ef database update
```

**Lưu ý**: Database sẽ được tự động seed với:
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

## 📝 Hướng dẫn sử dụng

### 1. Đăng ký tài khoản mới

1. Truy cập `/Account/Register`
2. Điền thông tin: Username, Email, Password, ConfirmPassword
3. Click "Đăng ký"
4. User mới sẽ tự động được gán role "User"

### 2. Đăng nhập

1. Truy cập `/Account/Login`
2. Nhập Username và Password
3. Click "Đăng nhập"

### 3. Đổi mật khẩu

1. Đăng nhập vào hệ thống
2. Truy cập `/Account/ChangePassword`
3. Nhập mật khẩu cũ và mật khẩu mới
4. Click "Đổi mật khẩu"

### 4. Quản lý Users (yêu cầu đăng nhập)

1. Đăng nhập vào hệ thống
2. Truy cập `/Users` để xem danh sách users
3. Click "Sửa" để chỉnh sửa user
4. Click "Xóa" để xóa user

### 5. Quản lý Roles (chỉ dành cho Admin)

1. Đăng nhập với tài khoản Admin
2. Truy cập `/Administration/ListRoles` để xem danh sách roles
3. Click "Tạo Role mới" để tạo role
4. Click "Sửa" để chỉnh sửa role
5. Click "Xóa" để xóa role

### 6. Phân quyền User (chỉ dành cho Admin)

1. Đăng nhập với tài khoản Admin
2. Truy cập `/Users` để xem danh sách users
3. Từ trang quản lý users, có thể truy cập chức năng phân quyền
4. Hoặc truy cập trực tiếp: `/Administration/EditUsersInRole?userId={userId}`

## 🔧 Cấu hình Database

Connection string được cấu hình trong `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DemoSlide2Db;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0"
  }
}
```

Database sẽ được tạo tự động với tên `DemoSlide2Db` trong SQL Server LocalDB.

## 📚 Tài liệu tham khảo

- [ASP.NET Core Identity Documentation](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity)
- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [ASP.NET Core MVC Documentation](https://learn.microsoft.com/en-us/aspnet/core/mvc/)

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

### Lỗi: "Access Denied" khi truy cập Administration

**Nguyên nhân**: User hiện tại không có role "Admin".

**Giải pháp**: Đăng nhập với tài khoản admin (username: `admin`, password: `123456`)

## 📄 License

Source code này được tạo cho mục đích giảng dạy và học tập.

## 👨‍💻 Tác giả

Created for NET1051 - C# 5 Course

---

**Lưu ý**: Source code này được thiết kế để demo và học tập. Không nên sử dụng trong môi trường production mà không có các biện pháp bảo mật bổ sung.

