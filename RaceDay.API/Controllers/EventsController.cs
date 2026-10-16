using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Models;
using System.Security.Claims;

namespace RaceDay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All endpoints require a valid token by default
    public class EventsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EventsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/events (Public - can be accessed by anyone, but still requires token here for simplicity)
        [HttpGet]
        [AllowAnonymous] // Allows public access
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.Include(e => e.Organiser).ToListAsync();
        }

        // POST: api/events (Organiser only)
        [HttpPost]
        [Authorize(Roles = "Organiser")]
        public async Task<ActionResult<Event>> CreateEvent(Event newEvent)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            newEvent.OrganiserId = userId;

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvents), new { id = newEvent.EventId }, newEvent);
        }

        // PUT: api/events/5 (Organiser only)
        [HttpPut("{id}")]
        [Authorize(Roles = "Organiser")]
        public async Task<IActionResult> UpdateEvent(int id, Event updatedEvent)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var existingEvent = await _context.Events.FindAsync(id);

            if (existingEvent == null) return NotFound();
            if (existingEvent.OrganiserId != userId) return Forbid();

            existingEvent.Title = updatedEvent.Title;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.EventDate = updatedEvent.EventDate;
            existingEvent.EventTime = updatedEvent.EventTime;
            existingEvent.Location = updatedEvent.Location;
            existingEvent.Status = updatedEvent.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/events/5 (Organiser only)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Organiser")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var eventToDelete = await _context.Events.FindAsync(id);

            if (eventToDelete == null) return NotFound();
            if (eventToDelete.OrganiserId != userId) return Forbid();

            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
