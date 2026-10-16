using RaceDay.API.Models;

namespace RaceDay.API.Data
{
    public static class DbSeeder
    {
        public static void SeedUsers(RaceDayDbContext context)
        {
            if (context.Users.Any()) return; // Already seeded

            context.Users.AddRange(
                new User { Email = "organiser1@raceday.co.za", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), FullName = "Thabo Mokoena", Role = "Organiser" },
                new User { Email = "organiser2@raceday.co.za", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), FullName = "Lindiwe Nkosi", Role = "Organiser" },
                new User { Email = "participant1@raceday.co.za", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), FullName = "Sipho Zulu", Role = "Participant" },
                new User { Email = "participant2@raceday.co.za", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), FullName = "Mary Smith", Role = "Participant" }
            );

            context.SaveChanges();
        }
    }
}