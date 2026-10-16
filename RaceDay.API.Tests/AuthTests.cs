using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using Xunit;


namespace RaceDay.API.Tests
{
    public class AuthTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsToken()
        {
            var loginRequest = new
            {
                Email = "organiser1@raceday.co.za",
                Password = "Password123!"
            };

            var response = await _client.PostAsJsonAsync("/api/Auth/login", loginRequest);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("token");
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            var loginRequest = new
            {
                Email = "organiser1@raceday.co.za",
                Password = "WrongPassword"
            };

            var response = await _client.PostAsJsonAsync("/api/Auth/login", loginRequest);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task CreateEvent_WithoutToken_ReturnsUnauthorized()
        {
            var newEvent = new
            {
                Title = "Test Event",
                Description = "Test",
                EventDate = "2026-12-01",
                EventTime = "06:00:00",
                Location = "Test Location",
                Status = "Open"
            };

            var response = await _client.PostAsJsonAsync("/api/Events", newEvent);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}