# LAB 3 - BÀI 1: Authentication Identity - [Authorize] và Redirect

## 📋 Mục Tiêu

- Hiểu cách **Authentication** hoạt động trong ASP.NET Core Identity
- Hiểu action được bảo vệ bằng `[Authorize]` và cơ chế redirect Login + ReturnUrl
- Quan sát URL redirect và ReturnUrl được encode như thế nào

## 🛠️ Yêu Cầu

- ASP.NET Core MVC với Individual User Accounts (Identity UI mặc định)
- .NET 10.0

---

## 📝 CÁC BƯỚC THỰC HIỆN

### BƯỚC 1: Tạo Project với Template có Identity UI

#### Lệnh CLI:

```bash
# Tạo project mới với Individual User Accounts (có sẵn Identity UI)
dotnet new mvc -au Individual -n DemoBai1

# Di chuyển vào thư mục project
cd DemoBai1

# Restore packages
dotnet restore

# Build project
dotnet build
```

**Giải thích:**
- `-au Individual`: Tạo project với **Individual User Accounts** - có sẵn Identity UI
- Identity UI mặc định sẽ có các trang: `/Identity/Account/Login`, `/Identity/Account/Register`, v.v.
- Database mặc định nên được cấu hình là **SQL Server LocalDB**.
- Cần tạo migration và update database (xem bước dưới)

#### Kiểm tra Project có Identity:

Sau khi tạo project, kiểm tra các file sau phải tồn tại:
- ✅ `Areas/Identity/Pages/` - Chứa Identity UI (Razor Pages)
- ✅ `Data/ApplicationDbContext.cs` - DbContext cho Identity
- ✅ `Program.cs` có `AddDefaultIdentity` và `MapRazorPages()`

#### Tạo Database:

**QUAN TRỌNG:** Project sử dụng **SQL Server LocalDB**, bạn cần chạy các lệnh sau:

```bash
# Tạo migration
dotnet ef migrations add InitialCreate

# Tạo database từ migration
dotnet ef database update
```

**Lưu ý:** 
- Đảm bảo bạn đã cài đặt SQL Server LocalDB (có sẵn khi cài Visual Studio hoặc .NET SDK).

---

### BƯỚC 2: Tạo Action Secured trong HomeController

#### Code HomeController (CHƯA có [Authorize]):

```csharp
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoBai1.Models;

namespace DemoBai1.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Action Secured - Trả về View với chuỗi "Hello"
    /// CHƯA có [Authorize] - ai cũng truy cập được
    /// </summary>
    [HttpGet]
    public IActionResult Secured()
    {
        return View("Secured", "Hello");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
```

**Lưu ý:**
- Action `Secured()` trả về View với model là chuỗi `"Hello"`
- Chưa có `[Authorize]` nên không yêu cầu đăng nhập

---

### BƯỚC 3: Tạo View Secured.cshtml

#### Tạo file: `Views/Home/Secured.cshtml`

```html
@{
    ViewData["Title"] = "Trang Bảo Mật";
}

<div class="container mt-5">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card">
                <div class="card-header bg-primary text-white">
                    <h3 class="mb-0">Trang Secured</h3>
                </div>
                <div class="card-body">
                    <h2 class="text-center text-success mb-4">Hello</h2>
                    <p class="lead text-center">
                        Đây là trang Secured. Bạn đã truy cập thành công!
                    </p>
                </div>
            </div>
        </div>
    </div>
</div>
```

---

### BƯỚC 4: Chạy và Test (CHƯA có [Authorize])

#### Lệnh CLI:

```bash
# Chạy project
dotnet run
```

#### Test:

1. Mở trình duyệt và truy cập: `https://localhost:5001/Home/Secured`
   - **Kỳ vọng:** Thấy trang hiển thị "Hello"
   - **Kết quả:** ✅ Truy cập được mà không cần đăng nhập

2. Quan sát URL:
   - URL: `https://localhost:5001/Home/Secured`
   - Không có redirect, không có ReturnUrl

**Kết luận BƯỚC 4:**
- Action `Secured` chưa được bảo vệ
- Ai cũng có thể truy cập mà không cần đăng nhập

---

### BƯỚC 5: Thêm [Authorize] cho Action Secured

#### Code HomeController (SAU KHI thêm [Authorize]):

```csharp
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;  // ← Thêm using này
using Microsoft.AspNetCore.Mvc;
using DemoBai1.Models;

namespace DemoBai1.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Action Secured - BƯỚC 5: Thêm [Authorize] để bảo vệ action
    /// Khi chưa đăng nhập, sẽ tự động redirect về /Identity/Account/Login?returnUrl=/Home/Secured
    /// </summary>
    [HttpGet]
    [Authorize] // ← THÊM DÒNG NÀY
    public IActionResult Secured()
    {
        return View("Secured", "Hello");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
```

**Thay đổi:**
1. Thêm `using Microsoft.AspNetCore.Authorization;`
2. Thêm `[Authorize]` attribute trước action `Secured()`

---

### BƯỚC 6: Chạy lại và Test (SAU KHI thêm [Authorize])

#### Lệnh CLI:

```bash
# Chạy lại project (nếu đã dừng)
dotnet run
```

#### Test:

1. **Mở trình duyệt mới (hoặc Incognito)** để đảm bảo chưa đăng nhập

2. **Cách 1:** Click vào link **"Secured"** trên header navigation  
   **Cách 2:** Truy cập trực tiếp: `https://localhost:5001/Home/Secured`

3. **Quan sát kết quả:**
   - ❌ **KHÔNG** thấy trang "Hello"
   - ✅ **BỊ REDIRECT** về trang Login
   - ✅ URL thay đổi thành: `https://localhost:5001/Identity/Account/Login?returnUrl=%2FHome%2FSecured`

4. **Phân tích URL redirect:**
   ```
   https://localhost:5001/Identity/Account/Login?returnUrl=%2FHome%2FSecured
   ```
   - **Trang Login mặc định:** `/Identity/Account/Login`
   - **ReturnUrl:** `%2FHome%2FSecured`
   - **ReturnUrl đã được encode:** `%2F` = `/` (URL encoding)
   - **ReturnUrl gốc:** `/Home/Secured`

5. **Giải mã ReturnUrl:**
   - `%2F` = `/`
   - `%2FHome%2FSecured` = `/Home/Secured`
   - ReturnUrl chứa URL mà user muốn truy cập ban đầu

---

## 🔍 GIẢI THÍCH CHI TIẾT

### 1. Trang Login Mặc Định

**Vị trí:** `/Identity/Account/Login`

**Giải thích:**
- Khi tạo project với `-au Individual`, ASP.NET Core tự động tạo **Identity UI** dưới dạng Razor Pages
- Identity UI nằm trong `Areas/Identity/Pages/Account/`
- Route mặc định: `/Identity/Account/Login`
- Trang này được tạo tự động, không cần code thủ công

**Kiểm tra trong code:**
- File: `Areas/Identity/Pages/Account/Login.cshtml` (nếu có)
- Hoặc được scaffold tự động khi chạy

### 2. Cơ Chế Redirect và ReturnUrl

**Khi nào xảy ra redirect?**
- User chưa đăng nhập
- Truy cập action có `[Authorize]`
- ASP.NET Core tự động redirect về Login page

**ReturnUrl được tạo như thế nào?**
1. User truy cập: `/Home/Secured`
2. Hệ thống phát hiện chưa đăng nhập
3. Lưu URL gốc (`/Home/Secured`) vào ReturnUrl
4. Encode ReturnUrl: `/Home/Secured` → `%2FHome%2FSecured`
5. Redirect về: `/Identity/Account/Login?returnUrl=%2FHome%2FSecured`

**Sau khi login thành công:**
- Identity UI tự động đọc ReturnUrl
- Redirect về URL gốc: `/Home/Secured`
- User thấy trang "Hello" như mong muốn

### 3. [Authorize] Attribute

**Chức năng:**
- Yêu cầu user phải **authenticated** (đã đăng nhập) mới truy cập được
- Nếu chưa đăng nhập → tự động redirect về Login
- Nếu đã đăng nhập → cho phép truy cập bình thường

**Cách hoạt động:**
```csharp
[Authorize]  // ← Kiểm tra: User.Identity.IsAuthenticated == true?
public IActionResult Secured()
{
    // Chỉ chạy đến đây nếu đã đăng nhập
    return View("Secured", "Hello");
}
```

---

## 📊 SO SÁNH TRƯỚC VÀ SAU

| Tiêu chí | TRƯỚC (BƯỚC 4) | SAU (BƯỚC 6) |
|----------|----------------|--------------|
| **Code** | Không có `[Authorize]` | Có `[Authorize]` |
| **Truy cập /Home/Secured** | ✅ Ai cũng truy cập được | ❌ Phải đăng nhập |
| **Redirect** | Không có | ✅ Redirect về Login |
| **URL sau redirect** | `/Home/Secured` | `/Identity/Account/Login?returnUrl=%2FHome%2FSecured` |
| **ReturnUrl** | Không có | ✅ Có ReturnUrl (đã encode) |

---

## ✅ CHECKLIST HOÀN THÀNH

- [x] BƯỚC 1: Tạo project với template Individual User Accounts
- [x] BƯỚC 2: Tạo action Secured (chưa có [Authorize])
- [x] BƯỚC 3: Tạo View Secured.cshtml
- [x] BƯỚC 4: Test truy cập /Home/Secured (thấy "Hello")
- [x] BƯỚC 5: Thêm [Authorize] cho action Secured
- [x] BƯỚC 6: Test lại (bị redirect về Login, quan sát ReturnUrl)

---

## 🧪 HƯỚNG DẪN TEST ĐẦY ĐỦ

### Test Case 1: Truy cập Secured (Chưa đăng nhập)

**Bước:**
1. Mở trình duyệt Incognito/Private
2. Click vào link **"Secured"** trên header (hoặc truy cập: `https://localhost:5001/Home/Secured`)

**Kỳ vọng:**
- ❌ Không thấy "Hello"
- ✅ Bị redirect về `/Identity/Account/Login?returnUrl=%2FHome%2FSecured`
- ✅ Thấy form đăng nhập

**Quan sát:**
- Copy URL và paste vào notepad
- Quan sát ReturnUrl: `%2FHome%2FSecured`
- Giải mã: `%2F` = `/` → ReturnUrl = `/Home/Secured`

### Test Case 2: Đăng nhập và Quay về Secured

**Bước:**
1. Trên trang Login, đăng ký tài khoản mới (nếu chưa có)
   - Click "Register" → Điền form → Submit
2. Hoặc đăng nhập với tài khoản đã có
3. Sau khi login thành công

**Kỳ vọng:**
- ✅ Tự động redirect về `/Home/Secured`
- ✅ Thấy trang "Hello"
- ✅ URL: `https://localhost:5001/Home/Secured` (không còn ReturnUrl)

**Quan sát:**
- ReturnUrl đã được sử dụng để redirect về trang gốc
- User không cần nhập lại URL `/Home/Secured`

---

## 📚 KIẾN THỨC QUAN TRỌNG

### 1. Authentication vs Authorization

- **Authentication (Xác thực):** "Bạn là ai?" - Kiểm tra user có đúng là người đó không
- **Authorization (Phân quyền):** "Bạn được phép làm gì?" - Kiểm tra quyền truy cập

`[Authorize]` yêu cầu **Authentication** - user phải đăng nhập.

### 2. Identity UI Mặc Định

- Tạo bằng: `dotnet new mvc -au Individual`
- Route: `/Identity/Account/Login`, `/Identity/Account/Register`
- Tự động xử lý ReturnUrl sau khi login

### 3. URL Encoding

- `/` được encode thành `%2F`
- `/Home/Secured` → `%2FHome%2FSecured`
- Mục đích: Tránh conflict với query string parameters

---

## 🐛 Troubleshooting

### Lỗi: Không redirect về Login

**Nguyên nhân:** Thiếu `app.UseAuthentication()` trong `Program.cs`

**Giải pháp:**
```csharp
app.UseRouting();
app.UseAuthentication();  // ← Phải có dòng này
app.UseAuthorization();
```

### Lỗi: Không thấy trang Login

**Nguyên nhân:** Project không có Identity UI

**Giải pháp:**
- Tạo lại project với `-au Individual`
- Hoặc scaffold Identity UI: `dotnet aspnet-codegenerator identity`

### Lỗi: ReturnUrl không hoạt động

**Nguyên nhân:** Identity UI chưa được map

**Giải pháp:**
```csharp
// Trong Program.cs phải có:
app.MapRazorPages();  // ← Map Identity Razor Pages
```

---

## 📝 TÓM TẮT

1. ✅ Tạo project với `-au Individual` → Có sẵn Identity UI
2. ✅ Tạo action `Secured()` → Trả về "Hello"
3. ✅ Test chưa có `[Authorize]` → Truy cập được
4. ✅ Thêm `[Authorize]` → Bảo vệ action
5. ✅ Test lại → Bị redirect về `/Identity/Account/Login?returnUrl=%2FHome%2FSecured`
6. ✅ Quan sát ReturnUrl → Hiểu cách encode và decode

**Kết luận:** `[Authorize]` tự động redirect về Login và lưu ReturnUrl để quay lại trang gốc sau khi login.

---

**Tác giả:** LAB 3 - BÀI 1 - NET1051  
**Ngày:** 2025

