using Demo01.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Demo01.Controllers
{
    public class ReservationClientController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationClientController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private string GetBaseUrl()
        {
            // Tự động lấy scheme và host hiện tại
            return $"{Request.Scheme}://{Request.Host}/api/Reservation";
        }

        // GET: ReservationClient
        // Slide 13: Get All
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(GetBaseUrl());

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var reservations = JsonConvert.DeserializeObject<List<Reservation>>(content);
                return View(reservations);
            }

            return View(new List<Reservation>());
        }

        // GET: ReservationClient/Details/5
        // Slide 17-18: Get By ID
        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{GetBaseUrl()}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var reservation = JsonConvert.DeserializeObject<Reservation>(content);
                return View(reservation);
            }

            return NotFound();
        }

        // GET: ReservationClient/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReservationClient/Create
        // Slide 21: Create (POST JSON)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var json = JsonConvert.SerializeObject(reservation);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(GetBaseUrl(), content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(reservation);
        }

        // GET: ReservationClient/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{GetBaseUrl()}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var reservation = JsonConvert.DeserializeObject<Reservation>(content);
                return View(reservation);
            }
            return NotFound();
        }

        // POST: ReservationClient/Edit/5
        // Slide 27: Update (PUT MultipartFormDataContent)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reservation reservation)
        {
            if (id != reservation.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                
                // CODE ĐÚNG THEO SLIDE 27: Sử dụng MultipartFormDataContent
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(reservation.Id.ToString()), "Id");
                    content.Add(new StringContent(reservation.Name ?? ""), "Name");
                    content.Add(new StringContent(reservation.StartLocation ?? ""), "StartLocation");
                    content.Add(new StringContent(reservation.EndLocation ?? ""), "EndLocation");

                    var response = await client.PutAsync(GetBaseUrl(), content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(reservation);
        }

        // POST: ReservationClient/Delete/5
        // Common CRUD Delete logic
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{GetBaseUrl()}/{id}");
            return RedirectToAction(nameof(Index));
        }

        // Slide 33: Update Partial - PATCH
        // Action này có thể được gọi từ một nút bấm riêng hoặc form riêng
        [HttpPost]
        public async Task<IActionResult> PatchUpdate(int id, string name)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{GetBaseUrl()}/{id}");

            // Tạo patch document JSON thủ công hoặc dùng thư viện
            // Slide 33 ví dụ: [ { "op": "replace", "path": "Name", "value": "New Name" } ]
            var patchContent = $"[ {{ \"op\": \"replace\", \"path\": \"Name\", \"value\": \"{name}\" }} ]";
            
            request.Content = new StringContent(patchContent, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }
    }
}
