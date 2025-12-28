# Lab 1: ASP.NET Core Identity (Bài 2)

Bài 2: Phân tích chi tiết các bảng trong cơ sở dữ liệu Identity.

---

## 1. AspNetUsers
*   **Chức năng**: Lưu trữ thông tin chính của người dùng (tài khoản).
*   **Các cột chính**:
    *   `Id` (PK, string/GUID): Khóa chính.
    *   `UserName`: Tên đăng nhập.
    *   `Email`: Địa chỉ email.
    *   `PasswordHash`: Mật khẩu đã được băm (hashing).
    *   `SecurityStamp`: Chuỗi thay đổi mỗi khi user đổi mật khẩu/thông tin quan trọng (dùng để đăng xuất các phiên cũ).
*   **Ví dụ**: User 'admin' đăng ký với email 'admin@example.com'. Dữ liệu sẽ nằm ở bảng này.

## 2. AspNetRoles
*   **Chức năng**: Lưu trữ danh sách các vai trò (quyền hạn).
*   **Các cột chính**:
    *   `Id` (PK): Mã vai trò.
    *   `Name`: Tên vai trò (Ví dụ: "Admin", "User", "Manager").
    *   `NormalizedName`: Tên viết hoa dùng để tìm kiếm nhanh.
*   **Ví dụ**: Hệ thống có 2 quyền: "Quản trị viên" và "Khách hàng".

## 3. AspNetUserRoles
*   **Chức năng**: Bảng trung gian liên kết User và Role (Quan hệ Nhiều-Nhiều).
*   **Các cột chính**:
    *   `UserId` (FK): Khóa ngoại trỏ về AspNetUsers.
    *   `RoleId` (FK): Khóa ngoại trỏ về AspNetRoles.
*   **Ví dụ**: User A có quyền 'Admin'. User A cũng có quyền 'Editor'.

## 4. AspNetUserClaims
*   **Chức năng**: Lưu trữ các thông tin bổ sung (claims) của User.
*   **Các cột chính**:
    *   `Id` (PK).
    *   `UserId` (FK).
    *   `ClaimType`: Loại thông tin (ví dụ: "DateOfBirth", "Department").
    *   `ClaimValue`: Giá trị (ví dụ: "01/01/2000", "IT").
*   **Ví dụ**: Lưu ngày sinh hoặc chức vụ cụ thể của nhân viên mà không cần sửa cấu trúc bảng User.

## 5. AspNetRoleClaims
*   **Chức năng**: Tương tự UserClaims nhưng dành cho Role.
*   **Ví dụ**: Role "Admin" có claim "Permission:DeleteUser".

## 6. AspNetUserLogins
*   **Chức năng**: Lưu thông tin đăng nhập từ bên thứ 3 (Google, Facebook...).
*   **Các cột chính**:
    *   `LoginProvider`: Nhà cung cấp (Google).
    *   `ProviderKey`: ID của user bên hệ thống Google.
    *   `UserId` (FK).

## 7. AspNetUserTokens
*   **Chức năng**: Lưu các mã token phục vụ xác thực (Email confirm, Reset password, 2FA).
*   **Ví dụ**: Khi user bấm "Quên mật khẩu", một token được sinh ra và lưu ở đây.

---
*Lab 1 - C# 5 - Analysis*
