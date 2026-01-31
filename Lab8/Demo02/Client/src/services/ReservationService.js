import axios from 'axios';

// URL của Backend API (Port 7xxx tùy theo cấu hình launchSettings của Backend)
// Trong demo này ta giả sử backend chạy ở cổng 5225 (http) hoặc 7225 (https)
// Cần check launchSettings.json của Backend để chính xác. 
// Tôi sẽ dùng localhost:5225 (HTTP) hoặc config sau.
// Tạm thời để một hằng số, sinh viên cần check port thực tế.
const API_URL = 'http://localhost:5250/api/Reservations';
// Note: Check properties/launchSettings.json của Backend để lấy port chính xác.
// Thường là http://localhost:5xxx hoặc https://localhost:7xxx

class ReservationService {
    getAll() {
        return axios.get(API_URL);
    }

    get(id) {
        return axios.get(`${API_URL}/${id}`);
    }

    create(data) {
        return axios.post(API_URL, data);
    }

    update(id, data) {
        return axios.put(`${API_URL}/${id}`, data);
    }

    delete(id) {
        return axios.delete(`${API_URL}/${id}`);
    }
}

export default new ReservationService();
