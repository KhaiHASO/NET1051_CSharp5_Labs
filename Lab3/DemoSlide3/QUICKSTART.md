# Hướng Dẫn Chạy Nhanh

## Bước 1: Restore và Build

```bash
dotnet restore
dotnet build
```

## Bước 2: Tạo Database

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

**Lưu ý:** Nếu chưa cài `dotnet-ef` tool:

```bash
dotnet tool install --global dotnet-ef
```

## Bước 3: Chạy Project

```bash
dotnet run
```

## Bước 4: Test

1. Mở trình duyệt: `https://localhost:5001`
2. Click link **Secured** → Sẽ redirect về Login
3. Đăng nhập với:
   - **Email:** `test@example.com`
   - **Password:** `123`
4. Sau khi login thành công → Quay về trang Secured
5. Click **Đăng Xuất** → Quay về Home

## User Test Mặc Định

Project tự động tạo user test khi chạy lần đầu:
- **Email:** `test@example.com`
- **Password:** `123`

## Troubleshooting

### Lỗi Migration
```bash
# Xóa migration cũ và tạo lại
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Lỗi Connection String
Kiểm tra file `appsettings.json` có connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DemoSlide3Db;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

