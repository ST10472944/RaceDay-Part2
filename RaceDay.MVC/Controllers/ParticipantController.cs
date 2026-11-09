using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Services;
using System.Text;
using System.Text.Json;

namespace RaceDay.MVC.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly ApiService _apiService;

        public ParticipantController(ApiService apiService)
        {
            _apiService = apiService;
        }

        private bool IsParticipant() => HttpContext.Session.GetString("UserRole") == "Participant";

        public async Task<IActionResult> Browse()
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Auth");

            var client = _apiService.GetClient();
            var response = await client.GetAsync("api/Events");

            if (response.IsSuccessStatusCode)
            {
                var events = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
                return View(events);
            }
            return View(JsonDocument.Parse("[]").RootElement);
        }

        public IActionResult MyResults()
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Auth");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Enrol(int eventId)
        {
            if (!IsParticipant()) return RedirectToAction("Login", "Auth");

            
            // we are simulating the success response for the UI demo here.
            // In a real app, you would call: await client.PostAsync($"api/Enrolments/{eventId}", ...)

            TempData["Success"] = "Successfully enrolled in the event!";
            return RedirectToAction("MyResults");
        }
    }
}