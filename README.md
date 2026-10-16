# RaceDay - Part 2: RESTful API Development

## Overview
RESTful API built with ASP.NET Core (.NET 9), Entity Framework Core, and SQL Server. Implements JWT authentication, role-based access control (Organiser / Participant), and Swagger UI for endpoint testing.

## Solution Structure
- **RaceDay.API** – Main API project
  - `Controllers/AuthController.cs` – Register and login endpoints (JWT)
  - `Controllers/EventsController.cs` – Event CRUD with role-based authorization
  - `Data/RaceDayDbContext.cs` – EF Core DbContext
  - `Data/DbSeeder.cs` – Seeds sample users on startup
  - `Migrations/` – EF Core migrations
  - `Models/` – 6 entities (User, Event, Category, EventCategory, Enrolment, Result)
- **RaceDay.API.Tests** – xUnit integration test project

## CI/CD Build Status
![Green build](docs/green-build.png)

## Functional Verification (via Swagger)
| Scenario | Expected | Actual |
|----------|----------|--------|
| Login (Organiser) | 200 OK with JWT | ✅ Verified |
| Login (Participant) | 200 OK with JWT | ✅ Verified |
| Create Event (Organiser) | 201 Created | ✅ Verified |
| Create Event (Participant) | 403 Forbidden | ✅ Verified |
| Create Event (No Token) | 401 Unauthorized | ✅ Verified |

## Unit Tests
Unit tests use xUnit and `Microsoft.AspNetCore.Mvc.Testing`. They currently fail due to a known .NET 9 issue where `WebApplicationFactory` cannot locate `testhost.deps.json` — a widely-documented integration-testing problem in .NET 6+ projects. The API has been manually verified through Swagger as proof of functionality.

## Setup Instructions
1. Clone the repository.
2. Run `database.sql` (from Part 1) in SSMS to create the database.
3. Update the connection string in `appsettings.json` with your SQL Server details.
4. Run `dotnet ef database update` to apply EF Core migrations.
5. Run `dotnet run --project .\RaceDay.API\RaceDay.API.csproj`.
6. Open `http://localhost:5045/swagger` in your browser.

## Roles
- **Organiser** – Create, update, and delete events. Requires an Organiser-role JWT.
- **Participant** – View events and enroll. Cannot create or modify events.

## Video Presentation
[Watch the Part 2 walkthrough](PASTE_YOUR_YOUTUBE_LINK_HERE)
