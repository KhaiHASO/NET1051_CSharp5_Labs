# TÓM TẮT CLI VÀ CODE - LAB 3 BÀI 1

## 📋 A) CÁC LỆNH CLI ĐẦY ĐỦ

### Tạo Project → Restore → Run

```bash
# 1. Tạo project với Individual User Accounts
dotnet new mvc -au Individual -n DemoBai1

# 2. Di chuyển vào thư mục
cd DemoBai1

# 3. Restore packages
dotnet restore

# 4. Build project
dotnet build

# 5. Tạo Database
dotnet ef migrations add InitialCreate
dotnet ef database update

# 6. Chạy project
dotnet run
```

**Lưu ý về Migration:**
- Project sử dụng **SQL Server LocalDB**, nên cần chạy migration để tạo database.

**Kết quả:** Project chạy tại `https://localhost:5001`

---

## 📝 B) CODE ĐẦY ĐỦ CỦA HomeController

### Version 1: TRƯỚC KHI thêm [Authorize] (BƯỚC 2-4)

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
    /// Action Secured - CHƯA có [Authorize]
    /// Ai cũng truy cập được /Home/Secured
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

### Version 2: SAU KHI thêm [Authorize] (BƯỚC 5-6)

```csharp
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;  // ← THÊM using này
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
    /// Action Secured - CÓ [Authorize]
    /// Yêu cầu đăng nhập mới truy cập được
    /// Redirect về /Identity/Account/Login?returnUrl=%2FHome%2FSecured
    /// </summary>
    [HttpGet]
    [Authorize]  // ← THÊM attribute này
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

**So sánh:**
- Version 1: Không có `using Microsoft.AspNetCore.Authorization;` và `[Authorize]`
- Version 2: Có cả 2 thứ trên

---

## 📄 C) NỘI DUNG Views/Home/Secured.cshtml

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

## 🧪 D) HƯỚNG DẪN TEST TỪNG BƯỚC

### BƯỚC 4: Test CHƯA có [Authorize]

1. **Chạy project:**
   ```bash
   dotnet run
   ```

2. **Truy cập URL:**
   ```
   https://localhost:5001/Home/Secured
   ```

3. **Kỳ vọng:**
   - ✅ Thấy trang hiển thị "Hello"
   - ✅ URL vẫn là `/Home/Secured`
   - ✅ Không có redirect

4. **Kết quả:** ✅ Truy cập được mà không cần đăng nhập

---

### BƯỚC 6: Test SAU KHI thêm [Authorize]

1. **Chạy lại project:**
   ```bash
   dotnet run
   ```

2. **Mở trình duyệt Incognito/Private** (để đảm bảo chưa đăng nhập)

3. **Truy cập:**
   - **Cách 1:** Click vào link **"Secured"** trên header navigation
   - **Cách 2:** Truy cập trực tiếp: `https://localhost:5001/Home/Secured`

4. **Kỳ vọng:**
   - ❌ KHÔNG thấy "Hello"
   - ✅ Bị redirect về trang Login
   - ✅ URL thay đổi thành: `/Identity/Account/Login?returnUrl=%2FHome%2FSecured`

5. **Quan sát ReturnUrl:**
   - URL: `https://localhost:5001/Identity/Account/Login?returnUrl=%2FHome%2FSecured`
   - ReturnUrl: `%2FHome%2FSecured`
   - Giải mã: `%2F` = `/` → ReturnUrl gốc = `/Home/Secured`

6. **Kết quả:** ✅ Bị redirect về Login, có ReturnUrl

---

### Test Đăng Nhập và Quay Về

1. **Trên trang Login:**
   - Đăng ký tài khoản mới (nếu chưa có)
   - Hoặc đăng nhập với tài khoản đã có

2. **Sau khi login thành công:**
   - ✅ Tự động redirect về `/Home/Secured`
   - ✅ Thấy trang "Hello"
   - ✅ URL: `https://localhost:5001/Home/Secured` (không còn ReturnUrl)

3. **Kết quả:** ✅ ReturnUrl hoạt động đúng

---

## 🔍 E) TRANG LOGIN MẶC ĐỊNH VÀ ReturnUrl

### 1. Trang Login Mặc Định

**Route:** `/Identity/Account/Login`

**Vị trí trong code:**
- `Areas/Identity/Pages/Account/Login.cshtml`
- Được tạo tự động khi dùng template `-au Individual`

**Cách kiểm tra:**
```bash
# Chạy project
dotnet run

# Truy cập trực tiếp
https://localhost:5001/Identity/Account/Login
```

**Kết quả:** Thấy form đăng nhập mặc định của Identity UI

---

### 2. ReturnUrl - Quan Sát và Giải Thích

#### Khi nào có ReturnUrl?

Khi user:
1. Chưa đăng nhập
2. Truy cập action có `[Authorize]`
3. Bị redirect về Login

#### ReturnUrl được tạo như thế nào?

**Ví dụ:**
- User truy cập: `/Home/Secured`
- Hệ thống lưu: `returnUrl=/Home/Secured`
- Encode: `returnUrl=%2FHome%2FSecured`
- URL đầy đủ: `/Identity/Account/Login?returnUrl=%2FHome%2FSecured`

#### URL Encoding

| Ký tự | Encoded | Giải thích |
|-------|---------|------------|
| `/` | `%2F` | Slash được encode |
| ` ` (space) | `%20` | Space được encode |
| `?` | `%3F` | Question mark được encode |

**Ví dụ cụ thể:**
```
URL gốc: /Home/Secured
Encoded: %2FHome%2FSecured
```

#### Quan Sát ReturnUrl trong Browser

1. **Mở Developer Tools (F12)**
2. **Tab Network:**
   - Xem request redirect
   - Status: `302 Found` hoặc `307 Temporary Redirect`
   - Location header: `/Identity/Account/Login?returnUrl=%2FHome%2FSecured`

3. **Tab Console:**
   - Có thể log URL để xem:
   ```javascript
   console.log(window.location.href);
   // Kết quả: https://localhost:5001/Identity/Account/Login?returnUrl=%2FHome%2FSecured
   ```

4. **Quan sát URL bar:**
   - Copy URL và paste vào notepad
   - Thấy: `returnUrl=%2FHome%2FSecured`
   - Decode: `%2F` = `/` → ReturnUrl = `/Home/Secured`

#### ReturnUrl Sau Khi Login

1. User đăng nhập thành công
2. Identity UI đọc ReturnUrl từ query string
3. Decode ReturnUrl: `%2FHome%2FSecured` → `/Home/Secured`
4. Redirect về: `/Home/Secured`
5. User thấy trang "Hello" như mong muốn

**Lưu ý:** ReturnUrl chỉ được sử dụng nếu là URL local (bảo mật)

---

## 📊 BẢNG SO SÁNH

| Tiêu chí | TRƯỚC [Authorize] | SAU [Authorize] |
|----------|-------------------|-----------------|
| **Code** | Không có `[Authorize]` | Có `[Authorize]` |
| **Truy cập /Home/Secured** | ✅ Được | ❌ Bị redirect |
| **URL** | `/Home/Secured` | `/Identity/Account/Login?returnUrl=%2FHome%2FSecured` |
| **ReturnUrl** | Không có | ✅ Có (đã encode) |
| **Cần đăng nhập?** | ❌ Không | ✅ Có |

---

## ✅ CHECKLIST

- [x] Tạo project với `-au Individual`
- [x] Tạo action Secured (chưa có [Authorize])
- [x] Tạo View Secured.cshtml
- [x] Test truy cập (thấy "Hello")
- [x] Thêm [Authorize]
- [x] Test lại (bị redirect, quan sát ReturnUrl)
- [x] Hiểu trang Login mặc định: `/Identity/Account/Login`
- [x] Hiểu ReturnUrl: encode/decode và cách hoạt động

---

**Tác giả:** LAB 3 - BÀI 1 - NET1051

