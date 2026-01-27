# Hướng dẫn chi tiết Demo03: Conventional-Based & Mixed Routing

Dự án này minh họa cách sử dụng **Conventional-Based Routing** và **Mixed Routing** trong ASP.NET Core Web API (.NET 10).

## 1. Mục tiêu Demo
- Hiểu cách cấu hình Default Route trong `Program.cs`.
- Hiểu sự khác biệt giữa Attribute Routing và Conventional Routing.
- Demo Mixed Routing: Một Controller chứa cả 2 loại Routing.

## 2. Hướng dẫn cài đặt và chạy
1.  **Yêu cầu:** Đã cài đặt .NET 10 SDK.
2.  **Mở dự án:** Mở thư mục `demo03`.
3.  **Chạy ứng dụng:**
    ```powershell
    dotnet run
    ```
4.  **Test API:** Sử dụng trình duyệt hoặc Postman (vì dự án này không dùng Swagger để tập trung vào test URL Convention).

## 3. Kịch bản Demo

**Bước 1: Show cấu hình `Program.cs`**
- Chỉ vào đoạn code:
  ```csharp
  app.MapControllerRoute(
      name: "default",
      pattern: "api/{controller}/{action}/{id?}");
  ```
- Giải thích: Mọi request phải bắt đầu bằng `api/`, theo sau là tên Controller và tên Action.

**Bước 2: Test Conventional Routing**
- Truy cập URL: `http://localhost:xxxx/api/Employee/GetByConvention`
- **Giải thích:**
  - `api`: Tiền tố cố định.
  - `Employee`: Tên Controller (EmployeeController).
  - `GetByConvention`: Tên Action Method.
- **Kết quả:** Chuỗi "Response from GetByConvention Method (Conventional Routing)".

**Bước 3: Test Attribute Routing (Mixed)**
- Truy cập URL: `http://localhost:xxxx/Attribute/Path`
- **Giải thích:** Action `GetByAttribute` có gắn `[Route("Attribute/Path")]`, nên nó sẽ **ghi đè** quy tắc Convention ở `Program.cs`.
- **Kết quả:** Chuỗi "Response from GetByAttribute Method (Attribute Routing)".

---
**Lưu ý:** Trong thực tế, Web API hiện đại ưu tiên dùng **Attribute Routing**. Conventional Routing thường dùng cho MVC Views.
