# Hướng Dẫn Chạy Solution `Demo02` (Decoupled Architecture)

Solution mô phỏng kiến trúc tách biệt (Decoupled) giữa Backend và Frontend.
- **Backend:** ASP.NET Core Web API 8/10.
- **Frontend:** VueJS 3 + Vite + Bootstrap 5.

## Cấu trúc thư mục
- `Demo02/Api`: Project Backend Web API.
- `Demo02/Client`: Project Frontend VueJS.

## Yêu cầu
- .NET 10 (hoặc .NET 8)
- Node.js & npm
- SQL Server LocalDB

---

## Bước 1: Chạy Backend (API)

1.  Mở Terminal tại thư mục `Demo02/Api`.
2.  Chạy lệnh cập nhật database (nếu chưa):
    ```bash
    dotnet ef database update
    ```
3.  Chạy server tại cổng **7000** (HTTPS):
    ```bash
    dotnet run --urls="https://localhost:7000"
    ```
4.  **Test API:**
    - Truy cập Swagger: [https://localhost:7000/swagger](https://localhost:7000/swagger)
    - Thấy danh sách các API (GET, POST, PUT, DELETE).

---

## Bước 2: Chạy Frontend (VueJS)

1.  Mở một Terminal MỚI.
2.  Truy cập thư mục `Demo02/Client`.
3.  Cài đặt thư viện (chỉ lần đầu):
    ```bash
    npm install
    ```
4.  Chạy Frontend:
    ```bash
    npm run dev
    ```
5.  Truy cập địa chỉ hiển thị trên màn hình (thường là `http://localhost:5173`).

---

## Kịch bản Demo
- **Bước 1:** Mở Swagger (Backend) -> Gọi API GET -> Thấy dữ liệu JSON.
- **Bước 2:** Mở Frontend -> Thấy giao diện hiển thị danh sách Reservation.
- **Bước 3:** Thử thêm/sửa/xóa trên Frontend để kiểm tra API.

---

