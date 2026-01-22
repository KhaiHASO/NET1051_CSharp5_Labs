# Demo 02: RESTful API với Repository Pattern và Fluent API

`Demo02` là dự án minh họa cách xây dựng cấu trúc Web API một cách thủ công (Manual Architecture), không sử dụng Scaffolding, để hiểu rõ từng thành phần cốt lõi. Dự án tập trung vào hai kỹ thuật chính: **Repository Pattern** và cấu hình Database bằng **Fluent API**.

## 1. Kiến trúc Dự Án và Repository Pattern

Trong dự án này, chúng ta tách biệt logic gọi Database ra khỏi Controller thông qua Repository Pattern.

### 🏛️ Repository Pattern là gì?
Repository Pattern là lớp trung gian kết nối giữa **Business Logic Layer** (Controller/Service) và **Data Access Layer** (DbContext/Database). Nó đóng vai trò "kho chứa" logic truy xuất dữ liệu, giúp Controller không cần biết dữ liệu được lấy từ đâu (SQL, API, File...).

### 🧠 Tại sao cần Repository Pattern? (So sánh với cách thường)

**Cách thường (Không dùng Repository):**
- Controller gọi trực tiếp `DbContext`.
- **Hậu quả**:
  - Code trong Controller bị rối, trộn lẫn logic xử lý API và logic truy vấn Data.
  - Nếu muốn đổi logic query (ví dụ: cần lọc thêm điều kiện `IsDeleted = false` cho mọi query), ta phải sửa ở **tất cả** các Action trong Controller.
  - Khó kiểm thử Unit Test vì Controller dính chặt với DbContext (kết nối DB thật).

**Dùng Repository Pattern:**
- Controller chỉ gọi `IRepository`. Repository gọi `DbContext`.
- **Lợi ích**:
  1. **Decoupling (Giảm phụ thuộc)**: Controller chỉ biết đến Interface `IRepository`. Nếu sau này đổi từ SQL Server sang MongoDB, chỉ cần viết class Repository mới, Controller không cần sửa dòng code nào.
  2. **Code Reusability (Tái sử dụng)**: Các logic truy vấn phức tạp (VD: Lấy danh sách kèm phân trang, tìm kiếm) được viết một lần trong Repository và tái sử dụng ở nhiều nơi.
  3. **Unit Testing Dễ dàng**: Ta dễ dàng tạo một `MockRepository` giả lập dữ liệu trả về để test Controller mà không cần động chạm đến Database thật.

### 🔄 Luồng dữ liệu (Code Flow)
`Client` (Postman) 
  ⬇️ 
`Controller` (Nhận Request) 
  ⬇️ 
`IRepository` (Interface trừu tượng) 
  ⬇️ 
`Repository Class` (Thực thi logic truy vấn, dùng DbContext) 
  ⬇️ 
`DbContext` (Ánh xạ Object <-> SQL) 
  ⬇️ 
`SQL Server` (Lưu trữ)

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
