# DemoBai1 - LAB 3 BÀI 1: Authentication Identity

## 📋 Mô Tả

Project demo cho **LAB 3 - BÀI 1** môn **NET1051 - Lập trình C# 5**.

**Mục tiêu:** Hiểu cách `[Authorize]` hoạt động và cơ chế redirect Login + ReturnUrl trong ASP.NET Core Identity.

## 🚀 Các Lệnh CLI Đầy Đủ

### 1. Tạo Project

```bash
# Tạo project với Individual User Accounts (có sẵn Identity UI)
dotnet new mvc -au Individual -n DemoBai1

# Di chuyển vào thư mục
cd DemoBai1

# Restore packages
dotnet restore
```

### 2. Tạo Database

**Lưu ý:** Project này sử dụng **SQL Server LocalDB**. Bạn cần tạo migration và update database trước khi chạy.

```bash
# Tạo migration
dotnet ef migrations add InitialCreate

# Tạo database từ migration
dotnet ef database update
```

### 3. Chạy Project

```bash
# Build và chạy
dotnet build
dotnet run

# Hoặc chạy trực tiếp
dotnet run
```

Project sẽ chạy tại: `https://localhost:5001` hoặc `http://localhost:5000`

## 📁 Cấu Trúc Project

```
DemoBai1/
├── Areas/
│   └── Identity/              # Identity UI (Razor Pages)
│       └── Pages/
├── Controllers/
│   ├── HomeController.cs      # Controller chính (có action Secured)
│   └── HomeController_Buoc4.cs # Version tham khảo (chưa có [Authorize])
├── Data/
│   └── ApplicationDbContext.cs # DbContext cho Identity
├── Views/
│   └── Home/
│       └── Secured.cshtml     # View hiển thị "Hello"
├── Program.cs                  # Cấu hình Identity
└── LAB3_BAI1_HUONG_DAN.md     # Hướng dẫn chi tiết
```

## 🧪 Hướng Dẫn Test

### Test 1: Truy Cập Secured (Chưa đăng nhập)

1. Mở trình duyệt **Incognito/Private**
2. Click vào link **"Secured"** trên header (hoặc truy cập: `https://localhost:5001/Home/Secured`)
3. **Kết quả:** Bị redirect về `/Identity/Account/Login?returnUrl=%2FHome%2FSecured`

**Quan sát:**
- URL có chứa `returnUrl=%2FHome%2FSecured`
- `%2F` = `/` (URL encoding)
- ReturnUrl gốc: `/Home/Secured`

### Test 2: Đăng Nhập và Quay Về

1. Trên trang Login, đăng ký tài khoản mới hoặc đăng nhập
2. Sau khi login thành công
3. **Kết quả:** Tự động redirect về `/Home/Secured` và thấy "Hello"

## 📝 Code Quan Trọng

### HomeController.cs (SAU KHI thêm [Authorize])

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[HttpGet]
[Authorize]  // ← Yêu cầu đăng nhập
public IActionResult Secured()
{
    return View("Secured", "Hello");
}
```

### HomeController.cs (TRƯỚC KHI thêm [Authorize])

Xem file: `HomeController_Buoc4.cs` (file tham khảo)

## 🔍 Giải Thích

### 1. Trang Login Mặc Định

- **Route:** `/Identity/Account/Login`
- **Vị trí:** `Areas/Identity/Pages/Account/Login.cshtml`
- Được tạo tự động khi dùng template `-au Individual`

### 2. ReturnUrl

- **Mục đích:** Lưu URL gốc để redirect về sau khi login
- **Encoding:** `/Home/Secured` → `%2FHome%2FSecured`
- **Sử dụng:** Identity UI tự động đọc và redirect về ReturnUrl sau khi login thành công

### 3. [Authorize] Attribute

- Yêu cầu user phải **authenticated** (đã đăng nhập)
- Nếu chưa đăng nhập → Tự động redirect về Login
- Nếu đã đăng nhập → Cho phép truy cập bình thường

## 📚 Tài Liệu

Xem file **LAB3_BAI1_HUONG_DAN.md** để có hướng dẫn chi tiết từng bước.

## ✅ Checklist

- [x] Project có Identity UI (`/Identity/Account/Login`)
- [x] Action Secured có `[Authorize]`
- [x] Redirect về Login khi chưa đăng nhập
- [x] ReturnUrl được encode đúng
- [x] Sau khi login → Quay về Secured

---

**Lưu ý:** Project này chỉ làm **BÀI 1**, không custom login/logout (sẽ làm ở Bài 2).

