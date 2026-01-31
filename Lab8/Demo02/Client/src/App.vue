<script setup>
import { ref, onMounted } from 'vue'
import ReservationService from './services/ReservationService'
import * as bootstrap from 'bootstrap'

// State
const reservations = ref([])
const loading = ref(false)
const errorMessage = ref('')
const currentReservation = ref({ name: '', startLocation: '', endLocation: '' })
const isEditing = ref(false)
let modalInstance = null

// Fetch Data
const fetchReservations = async () => {
  loading.value = true
  errorMessage.value = ''
  try {
    const response = await ReservationService.getAll()
    reservations.value = response.data
  } catch (error) {
    errorMessage.value = 'Lỗi khi tải dữ liệu: ' + error.message
  } finally {
    loading.value = false
  }
}

// Open Modal for Create
const openCreateModal = () => {
  isEditing.value = false
  currentReservation.value = { name: '', startLocation: '', endLocation: '' }
  showModal()
}

// Open Modal for Edit
const openEditModal = (item) => {
  isEditing.value = true
  // Copy object để tránh sửa trực tiếp vào list khi chưa lưu
  currentReservation.value = { ...item }
  showModal()
}

// Show Modal Helper
const showModal = () => {
  const modalEl = document.getElementById('reservationModal')
  if (modalEl) {
    // eslint-disable-next-line no-undef
    modalInstance = new bootstrap.Modal(modalEl)
    modalInstance.show()
  }
}

// Hide Modal Helper
const hideModal = () => {
  if (modalInstance) {
    modalInstance.hide()
  }
}

// Save (Create or Update)
const saveReservation = async () => {
  if (!currentReservation.value.name || !currentReservation.value.startLocation || !currentReservation.value.endLocation) {
    alert('Vui lòng nhập đầy đủ thông tin!')
    return
  }

  loading.value = true
  try {
    if (isEditing.value) {
      await ReservationService.update(currentReservation.value.id, currentReservation.value)
    } else {
      await ReservationService.create(currentReservation.value)
    }
    hideModal()
    await fetchReservations()
  } catch (error) {
    alert('Lỗi khi lưu: ' + error.message)
  } finally {
    loading.value = false
  }
}

// Delete
const deleteReservation = async (id) => {
  if (!confirm('Bạn có chắc muốn xóa?')) return

  loading.value = true
  try {
    await ReservationService.delete(id)
    await fetchReservations()
  } catch (error) {
    alert('Lỗi khi xóa: ' + error.message)
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  fetchReservations()
})
</script>

<template>
  <div class="container mt-4">
    <h1 class="mb-4 text-center">Quản lý Reservation (VueJS + .NET API)</h1>

    <!-- Alert Error -->
    <div v-if="errorMessage" class="alert alert-danger" role="alert">
      {{ errorMessage }}
    </div>

    <!-- Spinner -->
    <div v-if="loading" class="text-center my-3">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Loading...</span>
      </div>
    </div>

    <!-- Toolbar -->
    <div class="mb-3 text-end">
      <button class="btn btn-primary" @click="openCreateModal">
        <i class="bi bi-plus-lg"></i> Thêm Mới
      </button>
    </div>

    <!-- Table -->
    <table class="table table-striped table-hover table-bordered shadow-sm">
      <thead class="table-dark">
        <tr>
          <th>ID</th>
          <th>Tên</th>
          <th>Điểm đi</th>
          <th>Điểm đến</th>
          <th>Thao tác</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in reservations" :key="item.id">
          <td>{{ item.id }}</td>
          <td>{{ item.name }}</td>
          <td>{{ item.startLocation }}</td>
          <td>{{ item.endLocation }}</td>
          <td>
            <button class="btn btn-warning btn-sm me-2" @click="openEditModal(item)">Sửa</button>
            <button class="btn btn-danger btn-sm" @click="deleteReservation(item.id)">Xóa</button>
          </td>
        </tr>
        <tr v-if="reservations.length === 0 && !loading">
          <td colspan="5" class="text-center text-muted">Không có dữ liệu.</td>
        </tr>
      </tbody>
    </table>

    <!-- Modal Form -->
    <div class="modal fade" id="reservationModal" tabindex="-1" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">{{ isEditing ? 'Cập nhật Reservation' : 'Thêm mới Reservation' }}</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <form @submit.prevent="saveReservation">
              <div class="mb-3">
                <label class="form-label">Tên</label>
                <input v-model="currentReservation.name" type="text" class="form-control" required>
              </div>
              <div class="mb-3">
                <label class="form-label">Điểm đi</label>
                <input v-model="currentReservation.startLocation" type="text" class="form-control" required>
              </div>
              <div class="mb-3">
                <label class="form-label">Điểm đến</label>
                <input v-model="currentReservation.endLocation" type="text" class="form-control" required>
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Đóng</button>
            <button type="button" class="btn btn-primary" @click="saveReservation">Lưu</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style>
/* CSS tùy chỉnh nếu cần */
</style>
