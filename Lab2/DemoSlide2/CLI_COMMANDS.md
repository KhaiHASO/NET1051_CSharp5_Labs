# 🚀 CLI Commands - Hướng dẫn nhanh

## Các lệnh cần thiết để chạy project

### 1. Restore packages
```bash
dotnet restore
```

### 2. Tạo Migration
```bash
dotnet ef migrations add InitialCreate
```

**Lưu ý**: Nếu chưa cài đặt EF Core Tools, chạy lệnh:
```bash
dotnet tool install --global dotnet-ef
```

### 3. Cập nhật Database
```bash
dotnet ef database update
```

### 4. Chạy ứng dụng
```bash
dotnet run
```

### 5. Chạy với hot reload (tự động reload khi code thay đổi)
```bash
dotnet watch run
```

### 6. Xóa Migration (nếu cần)
```bash
dotnet ef migrations remove
```

### 7. Xem danh sách Migrations
```bash
dotnet ef migrations list
```

## 📝 Quy trình setup lần đầu

```bash
# 1. Di chuyển vào thư mục project
cd DemoSlide2

# 2. Restore packages
dotnet restore

# 3. Tạo migration
dotnet ef migrations add InitialCreate

# 4. Tạo database và seed data
dotnet ef database update

# 5. Chạy ứng dụng
dotnet run
```

## 🔄 Quy trình khi có thay đổi Model/DbContext

```bash
# 1. Xóa migration cũ (nếu cần)
dotnet ef migrations remove

# 2. Tạo migration mới
dotnet ef migrations add MigrationName

# 3. Cập nhật database
dotnet ef database update
```

## ⚠️ Lưu ý

- Đảm bảo SQL Server LocalDB đã được cài đặt
- Database sẽ được tạo tự động với tên `DemoSlide2Db`
- Seed data (Admin user và Roles) sẽ được tạo tự động khi chạy lần đầu

