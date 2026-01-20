# Demo 02: RESTful API với Repository Pattern và Fluent API

`Demo02` là dự án minh họa cách xây dựng cấu trúc Web API một cách thủ công (Manual Architecture), không sử dụng Scaffolding, để hiểu rõ từng thành phần cốt lõi. Dự án tập trung vào hai kỹ thuật chính: **Repository Pattern** và cấu hình Database bằng **Fluent API**.

## 1. Kiến trúc Dự Án và Repository Pattern

Trong dự án này, chúng ta tách biệt logic gọi Database ra khỏi Controller thông qua Repository Pattern.

*   **Luồng xử lý (Data Flow)**:
    `Controller` -> `IRepository` -> `Repository` -> `DbContext` -> `SQL Server`

*   **Tại sao dùng Repository Pattern?**
    *   **Decoupling (Giảm sự phụ thuộc)**: Controller không cần biết chi tiết về DbContext hay EF Core. Nếu sau này đổi DB (VD: từ SQL Server sang MongoDB), chỉ cần sửa Repository, không cần sửa Controller.
    *   **Testability (Dễ kiểm thử)**: Dễ dàng Mock `IRepository` để Unit Test cho Controller mà không cần kết nối Database thật.

## 2. Fluent API Configuration

Thay vì sử dụng các Data Annotation Attributes (như `[MaxLength]`, `[Key]`) trực tiếp trong Model class, chúng ta sử dụng **Fluent API** trong phương thức `OnModelCreating` của DbContext.

*   **Code**:
    ```csharp
    modelBuilder.Entity<Reservation>()
        .Property(r => r.Name)
        .HasMaxLength(250)
        .IsUnicode(false);
    ```
*   **Giải thích**:
    *   `HasMaxLength(250)`: Giới hạn độ dài chuỗi là 250 ký tự.
    *   `IsUnicode(false)`: Ánh xạ cột trong SQL Server thành kiểu `varchar` (không chứa dấu Unicode tiếng Việt) thay vì `nvarchar`. Điều này giúp tối ưu hóa hiệu năng và dung lượng lưu trữ (**Performance optimization**).

## 3. Kịch bản Demo (Script hướng dẫn)

Thực hiện theo các bước sau để xây dựng và chạy demo:

### Bước 1: Tạo dự án và Cấu hình (Setup)
*   Tạo mới dự án ASP.NET Core Web API tên `Demo02`.
*   Cài đặt các gói NuGet:
    *   `Microsoft.EntityFrameworkCore.SqlServer`
    *   `Microsoft.EntityFrameworkCore.Tools`
*   Cập nhật `appsettings.json` với Connection String:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CSharp5Slide6Demo02;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```

### Bước 2: Định nghĩa Model và Context (Fluent API)
*   Tạo class `Reservation` trong thư mục `Models` (chỉ chứa property thuần POCO).
*   Tạo class `ConsumeClientContext` kế thừa `DbContext` trong thư mục `Data`. override `OnModelCreating` để cấu hình Fluent API.

### Bước 3: Triển khai Repository Pattern
*   Tạo Interface `IRepository` trong thư mục `Repositories`.
*   Tạo class `Repository` thực thi `IRepository`, tiêm `ConsumeClientContext` vào Constructor.

### Bước 4: Tạo Controller & Đăng ký Dịch vụ (DI)
*   Tạo `ReservationController`. Chú ý: Inject `IRepository` chứ **KHÔNG** inject `DbContext`.
*   Trong `Program.cs`, đăng ký các dịch vụ:
    ```csharp
    // Đăng ký Context
    builder.Services.AddDbContext<ConsumeClientContext>(...);
    
    // Đăng ký Repository (DI)
    builder.Services.AddScoped<IRepository, Repository>();
    ```

### Bước 5: Migration
Mở Terminal tại thư mục dự án và chạy lệnh để tạo Database:
```powershell
dotnet ef migrations add InitialDb
dotnet ef database update
```

### Bước 6: Test Demo
*   Chạy ứng dụng (`F5` hoặc `dotnet run`).
*   Truy cập Swagger (`/swagger/index.html`).
*   Thử nghiệm các API: `GET`, `POST`, `PUT`, `DELETE` để kiểm chứng Repository hoạt động đúng.
