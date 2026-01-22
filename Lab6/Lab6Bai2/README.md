# Lab 6 - Bài 2: ASP.NET Core Empty + DI + EF Core CRUD

Dự án này là bài giải mẫu cho **Lab 6 Bài 2**, môn **C# 5 (NET1051)**.  
Mục tiêu là xây dựng một Web API quản lý **Reservations** từ template **Empty**, tự cấu hình **Dependency Injection**, **Entity Framework Core**, và **Swagger**.

---

## 🚀 1. Yêu cầu môi trường
- **.NET SDK**: .NET 8.0 trở lên (Project target .NET 10 nếu có, hoặc .NET 8).
- **Database**: SQL Server (LocalDB hoặc Docker/Full Instance).
- **Tools**: Visual Studio 2022 hoặc VS Code.

## 🏃 2. Cách chạy nhanh (Quick Start)
Mở terminal tại thư mục `Lab6Bai2`:
```bash
# 1. Khôi phục các gói thư viện
dotnet restore

# 2. Cấu hình Database (Xem mục 5)
# Nếu dùng LocalDB mặc định thì không cần sửa gì thêm.

# 3. Chạy ứng dụng
dotnet run
```
Sau khi chạy, truy cập Swagger:  
👉 **http://localhost:5000/swagger** (hoặc port ngẫu nhiên được cấp)

---

## ⚙️ 3. Cấu hình Connection String
File cấu hình: `appsettings.json`.

### Cách 1: LocalDB (Mặc định)
Dành cho Windows có cài sẵn LocalDB theo Visual Studio.
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=CSharp5Lab6Bai2;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### Cách 2: SQL Server Local / Docker
Nếu bạn dùng Docker hoặc SQL Server cài riêng (SQL Express), hãy sửa lại chuỗi kết nối (hoặc xem `appsettings.Docker.json`):
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=CSharp5Lab6Bai2;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;MultipleActiveResultSets=true"
}
```

---

## 🗄️ 4. Tạo Database (2 Cách)

### Cách A: Dùng SQL Script (Nhanh nhất)
1. Mở SQL Server Management Studio (SSMS) hoặc Azure Data Studio.
2. Mở file `database/init.sql`.
3. Chạy script (F5). Nó sẽ tạo DB `CSharp5Lab6Bai2`, bảng `Reservations` và thêm 3 dòng dữ liệu mẫu.

### Cách B: Dùng EF Core Migrations
Nếu máy bạn đã cài `dotnet-ef`:
```bash
# Tại thư mục Lab6Bai2
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 🏗️ 5. Kiến trúc thư mục
```
Lab6Bai2/
├── Controllers/       # Chứa API Controller
│   └── ReservationsController.cs
├── Data/             # Chứa DbContext
│   └── ReservationContext.cs
├── Models/           # Chứa Entity class
│   └── Reservation.cs
├── database/         # Script tạo DB
│   └── init.sql
├── postman/          # Postman Collection để test
│   └── CSharp5Slide6Demo02.postman_collection.json
├── Program.cs        # Entry point
├── Startup.cs        # Cấu hình DI, Middleware (Quan trọng của bài này)
└── appsettings.json  # Cấu hình DB
```

---

## 💉 6. Giải thích Kỹ thuật: Dependency Injection (DI)

### 🧐 Dependency Injection là gì?
Dependency Injection (DI) là một kỹ thuật thiết kế phần mềm giúp giảm sự phụ thuộc chặt chẽ (tight coupling) giữa các thành phần. Thay vì Class A tự tạo ra instance của Class B (ví dụ dùng từ khóa `new`), Class A sẽ **nhận** instance của Class B từ bên ngoài thông qua Constructor (hoặc Setter/Method).

### 💡 Khác biệt so với cách "Truyền thống" (Tight Coupling)
Giả sử ta không dùng DI:
```csharp
// ❌ Cách truyền thống: Tự khởi tạo trong Controller
public class ReservationsController : ControllerBase {
    private readonly ReservationContext _context;
    
    public ReservationsController() {
        // Tự new DbContext -> Controller bị phụ thuộc cứng vào ReservationContext
        // Khó test, khó quản lý vòng đời (đóng mở kết nối), khó thay thế Database khác.
        _context = new ReservationContext("...connection string..."); 
    }
}
```

Cách dùng DI trong bài này (Loose Coupling):
```csharp
// ✅ Cách dùng DI: Inject qua Constructor
public class ReservationsController : ControllerBase {
    private readonly ReservationContext _context;

    // Controller không quan tâm context được tạo ra như thế nào.
    // IoC Container (Startup.cs) sẽ lo việc tạo và đưa vào đây.
    public ReservationsController(ReservationContext context) {
        _context = context; 
    }
}
```

### 🗝️ Tại sao dùng DI trong dự án này?
1. **Quản lý Vòng đời (Lifetime Management)**: 
   - Ta đăng ký `AddDbContext` (mặc định là `Scoped`).
   - EF Core sẽ tự động tạo kết nối khi có Request mới và tự động `.Dispose()` (đóng kết nối) khi Request kết thúc. Ta không lo bị rò rỉ kết nối SQL.
2. **Dễ dàng Testing**:
   - Khi viết Unit Test, ta có thể inject một `ReservationContext` giả (in-memory) thay vì bắt buộc phải kết nối tới SQL Server thật.
3. **Clean Architecture**: Code trong Controller sạch hơn, chỉ tập trung vào xử lý logic API, không lo cấu hình Database.

---

## 📡 7. Danh sách API Endpoints
| Method | Endpoint | Mô tả | Code |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/reservations` | Lấy tất cả danh sách | 200 OK |
| `GET` | `/api/reservations/{id}` | Lấy chi tiết theo ID | 200 OK / 404 |
| `POST` | `/api/reservations` | Tạo mới | 201 Created |
| `PUT` | `/api/reservations/{id}` | Cập nhật | 204 No Content |
| `DELETE` | `/api/reservations/{id}` | Xóa | 204 No Content |

---

## 🧪 8. Hướng dẫn Test bằng Postman
1. Mở Postman -> Import -> Chọn file `postman/CSharp5Slide6Demo02.postman_collection.json`.
2. Kiểm tra biến `baseUrl` trong collection (Mặc định `http://localhost:5000`). Nếu app chạy port khác, hãy sửa lại.
3. Chạy lần lượt các request từ trên xuống dưới để kiểm nghiệm quy trình CRUD.

---

## ❓ 9. Troubleshooting (Sửa lỗi thường gặp)
- **Lỗi kết nối DB**: Kiểm tra lại Connection String trong `appsettings.json`. Đảm bảo Server Name đúng.
- **Certificate Error**: Nếu dùng Docker/SQL Express, nhớ thêm `;TrustServerCertificate=True`.
- **Port in use**: Nếu port 5000 bị chiếm, kiểm tra file `Properties/launchSettings.json` hoặc xem log console để biết port thực tế (ví dụ: `http://localhost:5123`).
- **Thiếu dotnet-ef**: Chạy `dotnet tool install --global dotnet-ef` nếu muốn dùng lệnh migration.
