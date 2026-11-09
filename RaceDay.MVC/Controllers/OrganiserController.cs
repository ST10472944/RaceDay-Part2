using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Services;
using System.Text;
using System.Text.Json;

namespace RaceDay.MVC.Controllers
{
    public class OrganiserController : Controller
    {
        private readonly ApiService _apiService;

        public OrganiserController(ApiService apiService)
        {
            _apiService = apiService;
        }

        private bool IsOrganiser() => HttpContext.Session.GetString("UserRole") == "Organiser";

        public async Task<IActionResult> Dashboard()
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");

            var client = _apiService.GetClient();
            var response = await client.GetAsync("api/Events");

            if (response.IsSuccessStatusCode)
            {
                var events = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
                return View(events);
            }

            // If API call fails, pass an empty array to avoid crashing the view
            return View(JsonDocument.Parse("[]").RootElement);
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(string title, string description, DateTime eventDate, TimeSpan eventTime, string location, string status, IFormFile? banner)
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");

            var client = _apiService.GetClient();
            string bannerUrl = "https://via.placeholder.com/800x400?text=RaceDay+Event";

            // Upload image to API (which talks to Azure)
            if (banner != null && banner.Length > 0)
            {
                var fileContent = new MultipartFormDataContent();
                fileContent.Add(new StreamContent(banner.OpenReadStream()), "file", banner.FileName);
                var imgResponse = await client.PostAsync("api/Images/upload", fileContent);

                if (imgResponse.IsSuccessStatusCode)
                {
                    var imgJson = JsonDocument.Parse(await imgResponse.Content.ReadAsStringAsync()).RootElement;
                    bannerUrl = imgJson.GetProperty("Url").GetString() ?? bannerUrl;
                }
            }

            // Create the event
            var eventData = new
            {
                Title = title,
                Description = description,
                EventDate = eventDate,
                EventTime = eventTime,
                Location = location,
                Status = status,
                BannerUrl = bannerUrl
            };

            var content = new StringContent(JsonSerializer.Serialize(eventData), Encoding.UTF8, "application/json");
            await client.PostAsync("api/Events", content);

            return RedirectToAction("Dashboard");
        }
    }
}