# Hướng dẫn chi tiết Demo02: HTTP Verb Attribute Routing

Dự án này minh họa cách sử dụng các Attribute **HTTP Verbs** (`[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpDelete]`) để định nghĩa API RESTful trong ASP.NET Core Web API (.NET 10).

## 1. Mục tiêu Demo
- Hiểu và phân biệt được các phương thức HTTP: GET, POST, PUT, DELETE.
- Biết cách sử dụng Attribute Routing để map URL và Verb vào Action Method.
- Sử dụng Swagger UI để test các phương thức POST/PUT/DELETE.

## 2. Hướng dẫn cài đặt và chạy
1.  **Yêu cầu:** Đã cài đặt .NET 10 SDK.
2.  **Mở dự án:** Mở thư mục `demo02`.
3.  **Chạy ứng dụng:**
    - Nhấn F5 (Visual Studio) hoặc chạy lệnh:
      ```powershell
      dotnet run
      ```
    - Truy cập Swagger UI tại địa chỉ hiển thị (ví dụ: `http://localhost:5000/swagger`).

## 3. Kịch bản Demo 

Do trình duyệt web mặc định chỉ gửi request **GET**, chúng ta **BẮT BUỘC** phải dùng Swagger UI hoặc Postman để test đầy đủ các tính năng.

**Bước 1: Test GET (Lấy danh sách)**
- Tìm API `GET /api/products`.
- Nhấn **Try it out** -> **Execute**.
- **Kết quả:** Code 200 OK, Response body: `["Product1", "Product2"]`.

**Bước 2: Test GET by ID (Lấy chi tiết)**
- Tìm API `GET /api/products/{id}`.
- Nhấn **Try it out**.
- Nhập `id = 10`.
- Nhấn **Execute**.
- **Kết quả:** Code 200 OK, Response body: `Product10`.

**Bước 3: Test POST (Tạo mới)**
- Tìm API `POST /api/products`.
- Nhấn **Try it out**.
- Nhập `product` trong ô Request body (text/json), ví dụ: `"New Product"`.
- Nhấn **Execute**.
- **Kết quả:** Code 201 Created. Header `Location` sẽ trỏ về URL lấy chi tiết sản phẩm vừa tạo.

**Bước 4: Test PUT (Cập nhật)**
- Tìm API `PUT /api/products/{id}`.
- Nhấn **Try it out**.
- Nhập `id = 5` và `product = "Updated Name"`.
- Nhấn **Execute**.
- **Kết quả:** Code 204 No Content (Thành công nhưng không có nội dung trả về).

**Bước 5: Test DELETE (Xóa)**
- Tìm API `DELETE /api/products/{id}`.
- Nhấn **Try it out**.
- Nhập `id = 5`.
- Nhấn **Execute**.
- **Kết quả:** Code 204 No Content.

---
**Lưu ý:** Demo này tập trung vào Routing Logic nên dữ liệu là giả lập (Hardcoded), không lưu vào Database thực tế.
