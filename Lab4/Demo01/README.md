# CSharp5 - Slide 4 - Demo 01: User Claims & Policy Authorization

Dự án Demo minh hoạ cách sử dụng **User Claims** trong ASP.NET Core Identity để phân quyền (Authorization) dựa trên Policy.

## 1. Yêu cầu & Công nghệ
- **Nền tảng**: .NET 8 (ASP.NET Core MVC).
- **Database**: SQL Server LocalDB (`CSharp5Slide4Demo01`).
- **Thư viện**: Entity Framework Core, Identity.
- **Tính năng chính**:
  - Quản lý Claims cho User (Admin).
  - Xem Claims của bản thân (User).
  - Chặn truy cập bằng Policy (`RequireCanGrade`).

## 2. Cài đặt & Chạy dự án (Code-First)

### Bước 1: Chuẩn bị môi trường
Đảm bảo máy đã cài .NET 8 SDK và Visual Studio / VS Code.

### Bước 2: Build & Tạo Database
Mở terminal tại thư mục `Demo01` và chạy lệnh:

```bash
# 1. Khôi phục packages
dotnet restore

# 2. Tạo Database & Bảng (nếu chưa có)
dotnet ef database update
```
*Lưu ý: Lệnh `dotnet ef database update` sẽ tự động chạy migration `InitialIdentity` đã tạo sẵn.*

### Bước 3: Chạy ứng dụng
```bash
dotnet run
```
Truy cập: `https://localhost:7xxx` (port tuỳ máy).

## 3. Seed Data (Dữ liệu mẫu)
Khi chạy lần đầu, `SeedData` sẽ tự động tạo:
1. **Admin**: `admin@demo.com` / `Admin@12345` (Role: Admin).
2. **User**: `user@demo.com` / `User@12345` (Role: User, có sẵn Claim `Department=IT`).

## 4. Kịch bản Demo (Hướng dẫn chi tiết)

### Phần 1: Kiểm tra Database
1. Mở **SQL Server Object Explorer** (hoặc SSMS).
2. Connect vào `(localdb)\mssqllocaldb`.
3. Tìm DB `CSharp5Slide4Demo01`.
4. Mở bảng `AspNetUsers`: thấy 2 user đã seed.
5. Mở bảng `AspNetUserClaims`: thấy user@demo.com có claim `Department`.

### Phần 2: Quản lý Claims (Admin)
1. Đăng nhập bằng `admin@demo.com` / `Admin@12345`.
2. Trên Navbar, click menu màu đỏ **Admin Users**.
3. Danh sách user hiện ra. Click **Manage Claims** cho `user@demo.com`.
4. **Thêm Claim**:
   - Chọn Type: `Position`.
   - Nhập Value: `Teacher`.
   - Click **Add Claim**.
5. Thấy Claim mới xuất hiện trong bảng "Existing Claims".
6. Mở lại Database (bảng `AspNetUserClaims`) để chứng minh dữ liệu đã lưu.

### Phần 3: Xem Claims (User thường)
1. Logout Admin.
2. Đăng nhập `user@demo.com` / `User@12345`.
3. Click menu **My Claims**.
4. Sẽ thấy danh sách: `Department: IT`, `Position: Teacher`.

### Phần 4: Demo Authorization (Chặn quyền)
1. User (`user@demo.com`) click menu **Grades**.
2. **Kết quả**: Bị chuyển hướng sang trang **Access Denied** (Từ chối truy cập).
   - Lý do: Trang này yêu cầu Policy `RequireCanGrade` (cần claim `CanGrade` = `true`), nhưng user chưa có.
3. Logout User.
4. Login Admin (`admin@demo.com`).
5. Vào **Admin Users** -> **Manage Claims** của `user@demo.com`.
6. Thêm Claim:
   - Type: `CanGrade`
   - Value: `true`
7. Logout Admin.
8. Login lại User (`user@demo.com`).
   - *Lưu ý: Cần logout/login để làm mới Claims trong Cookie.*
9. Click menu **Grades**.
10. **Kết quả**: Truy cập thành công "Access Granted!".

## 5. Cấu trúc Code chính
- **Program.cs**:
  - Cấu hình SQL Server.
  - Cấu hình Policy: `options.AddPolicy("RequireCanGrade", ...)`
  - Cấu hình `AccessDeniedPath`.
- **Controllers/AdminController.cs**: Code thêm/xoá Claims dùng `UserManager.AddClaimAsync`.
- **Controllers/GradesController.cs**: Có attribute `[Authorize(Policy = "RequireCanGrade")]`.
- **Views/Admin/Claims.cshtml**: Giao diện quản lý Claims.

---
**Tác giả**: [Tên Bạn] - NET1051 Labs
