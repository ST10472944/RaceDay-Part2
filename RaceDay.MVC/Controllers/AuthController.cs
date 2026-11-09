using Microsoft.AspNetCore.Mvc;
using RaceDay.MVC.Services;
using System.Text;
using System.Text.Json;

namespace RaceDay.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;

        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("JWToken") != null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var client = _apiService.GetClient();
            var loginData = new { Email = email, Password = password };
            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenData = JsonDocument.Parse(jsonResponse).RootElement;

                HttpContext.Session.SetString("JWToken", tokenData.GetProperty("token").GetString()!);
                HttpContext.Session.SetString("UserRole", tokenData.GetProperty("userRole").GetString()!);
                HttpContext.Session.SetString("UserName", email);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid login credentials.";
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string fullName, string role)
        {
            var client = _apiService.GetClient();
            var regData = new { Email = email, Password = password, FullName = fullName, Role = role };
            var content = new StringContent(JsonSerializer.Serialize(regData), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Auth/register", content);

            if (response.IsSuccessStatusCode) return RedirectToAction("Login");

            ViewBag.Error = await response.Content.ReadAsStringAsync();
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}