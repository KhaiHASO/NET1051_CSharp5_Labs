# DemoSlide4 - Claims-Based Authorization (Cyberpunk Edition) 🦾⚡

Dự án Demo thực hiện **Claims-Based Authorization** trong ASP.NET Core 10, theo yêu cầu tài liệu Lab 4 - Lập trình C# 5. Dự án được thiết kế với giao diện **Cyberpunk** hiện đại.

## 🚀 Tính năng chính

- **Identity & Claims**: Tích hợp ASP.NET Core Identity.
- **Authorization Policies**:
  - `CreateProductPolicy`: Yêu cầu Claim `CreateProduct`.
  - `AdminViewProductPolicy`: Yêu cầu Claim `Admin`.
  - `SalesViewProductPolicy`: Yêu cầu Claim `Sales` và logic kiểm tra `CreatedBy` (Người dùng chỉ xem được sản phẩm do chính mình tạo ra).
- **Cyberpunk UI**: Sử dụng CSS tùy chỉnh (`cyberpunk.css`) với hiệu ứng neon, flicker và layout futuristic.
- **Docker Ready**: Đi kèm `docker-compose.yml` để chạy SQL Server 2022.

## 🛠️ Hướng dẫn cài đặt

### 1. Khởi chạy Database (Docker)
Nếu bạn có Docker, hãy chạy lệnh sau tại thư mục gốc:
```bash
docker-compose up -d
```

### 2. Cấu hình Connection String
Kiểm tra `appsettings.json` để đảm bảo thông tin kết nối chính xác:
```json
"DefaultConnection": "Server=localhost,1433;Database=DemoSlide4Db;User Id=sa;Password=FptPoly@2025;TrustServerCertificate=True;MultipleActiveResultSets=true"
```
### Ubuntu
```bash
dotnet tool install --global dotnet-ef
export PATH="$PATH:$HOME/.dotnet/tools"
```
## 🧪 Test Accounts (Auto-seeded)

Hệ thống tự động khởi tạo các tài khoản sau để bạn kiểm tra:

| Role | Email | Password | Claims |
| :--- | :--- | :--- | :--- |
| **Admin** | `admin@neon.system` | `Admin@123` | `Admin` |
| **Sales** | `sales@neon.system` | `Sales@123` | `Sales`, `CreateProduct` |
| **User** | `dev@neon.system` | `User@123` | (None) |

### 3. Cập nhật Database
Chạy lệnh migration để tạo cấu trúc bảng:
```bash
dotnet ef database update
```

### 4. Chạy ứng dụng
```bash
dotnet run
```

## 📝 Ghi chú về Lab 4
Dự án bao gồm:
- **Bài 1**: Cấu hình Policy đơn giản dựa trên Claim.
- **Bài 2**: Cấu hình Policy phức tạp kết hợp logic kiểm tra mã người dùng (`CreatedBy`) trong `ProductController`.

---
*Created by Antigravity AI for Lab 4 - C# 5.*
