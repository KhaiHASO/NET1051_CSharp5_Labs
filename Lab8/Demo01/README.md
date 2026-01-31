# Hướng Dẫn Chạy Project Demo01 - Lab 8 (Web Client gọi RESTful APIs)

Project mô phỏng kiến trúc Client-Server trong cùng một ứng dụng ASP.NET Core MVC (Single Project).
- **Server:** `ReservationApiController` (cung cấp API).
- **Client:** `ReservationClientController` (gọi API bằng HttpClient).
- **Database:** SQL Server LocalDB.

## Yêu cầu
- .NET 10 (hoặc .NET 8)
- SQL Server LocalDB

## Cài đặt và Chạy

1.  **Cài đặt Gói NuGet (Nếu chưa có):**
    ```bash
    dotnet restore
    ```

2.  **Khởi tạo Database:**
    Để tạo database và seed dữ liệu mẫu:
    ```bash
    dotnet ef database update
    ```
    (Database tên là `CSharp5Slide8Demo01` trong `(localdb)\mssqllocaldb`)

3.  **Chạy Ứng dụng:**
    ```bash
    dotnet run
    ```
    Truy cập: `https://localhost:7053` hoặc port hiển thị trên console.

## Kịch bản Demo

Thực hiện các bước sau để kiểm tra đầy đủ các tính năng theo Slide bài học:

### 1. Xem Danh Sách (GET) - Slide 13
- Trên thanh menu, click vào **Reservation Client**.
- Trang Index hiện ra danh sách các Reservation được lấy từ API.
- **Kỹ thuật:** `HttpClient.GetAsync()` -> `ReadAsStringAsync()` -> Deserialize.
- **Quan sát:** Có một Alert Info màu xanh ở đầu trang xác nhận kỹ thuật đang dùng.

### 2. Tạo Mới (POST) - Slide 21
- Click nút **Create New**.
- Nhập thông tin (Name, Start Location, End Location).
- Click **Create**.
- **Kỹ thuật:** Serialize Object -> `StringContent` (application/json) -> `PostAsync()`.
- **Kết quả:** Quay về trang danh sách và thấy item mới.

### 3. Cập Nhật (PUT) - Slide 27 (Quan trọng)
- Click nút **Edit (PUT)** trên một dòng bất kỳ.
- Sửa thông tin.
- Click **Save (PUT)**.
- **Kỹ thuật:** **KHÔNG** gửi JSON body. Sử dụng `MultipartFormDataContent` để gửi dữ liệu dạng Form.
- **Quan sát:** Alert Info trên trang Edit giải thích về `MultipartFormDataContent`.

### 4. Cập Nhật Một Phần (PATCH) - Slide 33 (Quan trọng)
- Tại trang **Index**, kéo xuống dưới cùng phần "Test Partial Update (PATCH)".
- Nhập `ID` của một item đang có (ví dụ: 1).
- Nhập `Name` mới.
- Click **Test Patch Name**.
- **Kỹ thuật:** Tạo thủ công `HttpRequestMessage` với Method = PATCH. Body là chuỗi JSON Patch Operation (`op: replace`).
- **Kết quả:** Trang reload và tên của item đó thay đổi.

### 5. Xóa (DELETE)
- Click nút **Delete**.
- Confirm "Are you sure...?".
- Item bị xóa khỏi danh sách.

## Ghi chú về Code
- Tất cả các file Controller đều có comment tiếng Việt giải thích rõ ràng, trích dẫn số trang Slide tương ứng.
- **Controller API:** `Controllers/ReservationApiController.cs`
- **Controller Client:** `Controllers/ReservationClientController.cs`
