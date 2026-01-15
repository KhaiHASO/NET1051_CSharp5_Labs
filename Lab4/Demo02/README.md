# CSharp5 - Slide 4 - Demo 02: Claims-Based Authorization (Permission/Policy)

Project demo minh hoạ cơ chế Authorization nâng cao sử dụng **Custom Policy Provider** và **Authorization Handler** để kiểm tra **Permissions** (được lưu dưới dạng User Claims).

## 1. Yêu cầu & Công nghệ
- **Code-First**: Entity Framework Core.
- **Identity**: Microsoft.AspNetCore.Identity.
- **Database**: SQL Server LocalDB (`CSharp5Slide4Demo02`).
- **Cơ chế**:
  - Permission lưu trong bảng `AspNetUserClaims`.
  - Policy động: `Permission:TenQuyen`.
  - Middleware tự động kiểm tra quyền & ẩn hiện menu.

## 2. Cài đặt & Chạy demo

### Bước 1: Chuẩn bị
Mở terminal tại thư mục `Demo02`.

### Bước 2: Tạo Database
Chạy lệnh sau để tạo DB và Seed data:

```bash
dotnet restore
dotnet ef database update
```

### Bước 3: Chạy ứng dụng
```bash
dotnet run
```
Truy cập: `https://localhost:7xxx`

## 3. Seed Data (Tài khoản mẫu)
Khi start app, seed data khởi tạo:

1.  **Admin**:
    -   User: `admin@demo.com` / `Admin@12345`
    -   Permissions: Full quyền (`Grades.View`, `Grades.Edit`, `Reports.View`, `AdminPanel.Access`).
2.  **User**:
    -   User: `user@demo.com` / `User@12345`
    -   Permissions: Chỉ có quyền xem điểm (`Grades.View`).

## 4. Kịch bản Demo (Script giảng dạy)

### Phần 1: Setup & Kiểm tra Database
1.  Chạy `dotnet ef database update`.
2.  Mở SQL Server Object Explorer -> DB `CSharp5Slide4Demo02`.
3.  Vào bảng `AspNetUserClaims`, thấy Admin có 4 dòng permission, User có 1 dòng permission.

### Phần 2: Demo Authorize theo Permission
1.  Đăng nhập `user@demo.com`.
2.  **Menu**: Chỉ thấy "Điểm Số", không thấy "Báo Cáo", không thấy "Quản Trị".
3.  Vào **Điểm Số** (`/Grades`) -> Thành công (Do có `Grades.View`).
4.  Tại trang Điểm Số, nút "Chỉnh Sửa" bị disable (Do thiếu `Grades.Edit`).
5.  Thử truy cập trực tiếp URL: `/Grades/Edit` -> Chuyển hướng tới trang **Từ Chối Truy Cập** (Access Denied).
6.  Thử truy cập trực tiếp URL: `/Reports` -> Chuyển hướng Access Denied.

### Phần 3: Admin Cấp Quyền
1.  Đăng xuất, Đăng nhập `admin@demo.com`.
2.  Thấy full menu: Điểm Số, Báo Cáo, Quản Trị.
3.  Vào **Quản Trị** -> **Danh sách người dùng**.
4.  Chọn **Quản lý Quyền** của `user@demo.com`.
5.  Tại form "Cấp Quyền Mới":
    -   Chọn `Reports.View`.
    -   Bấm **Thêm Quyền**.
    -   Chọn `Grades.Edit`.
    -   Bấm **Thêm Quyền**.
6.  Kiểm tra bảng "Permission Hiện Tại" đã có đủ.

### Phần 4: User kiểm tra lại
1.  Đăng xuất Admin.
2.  Đăng nhập lại `user@demo.com` (Bắt buộc login lại để refresh Claims trong Cookie).
3.  **Kết quả**:
    -   Menu "Báo Cáo" đã hiện ra -> Truy cập `/Reports` thành công.
    -   Vào "Điểm Số", nút "Chỉnh Sửa" đã enable -> Bấm vào vào được trang `/Grades/Edit`.

## 5. Cấu trúc Code (Core Authorization)
- `Authorization/PermissionRequirement.cs`: Class lưu tên quyền yêu cầu.
- `Authorization/PermissionAuthorizationHandler.cs`: Logic kiểm tra `User.HasClaim("Permission", permission)`.
- `Authorization/PermissionPolicyProvider.cs`: Tự động tạo Policy khi gặp tên `Permission:X`.
- `Controllers/GradesController.cs`: Dùng `[Authorize(Policy = "Permission:Grades.View")]`.

---
**Tác giả**: [Tên Bạn] - NET1051 Labs
