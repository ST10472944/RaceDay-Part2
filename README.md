# RaceDay - Part 2: RESTful API Development

## Overview
RESTful API built with ASP.NET Core (.NET 9), Entity Framework Core, and SQL Server. Implements JWT authentication, role-based access control (Organiser / Participant), and Swagger UI.

## Solution Structure
- **RaceDay.API** – Main API project (Controllers, Data, Migrations, Models)
- **RaceDay.API.Tests** – xUnit test project

## Functional Verification (via Swagger)
| Scenario | Expected | Actual |
|----------|----------|--------|
| Login (Organiser) | 200 OK with JWT | ✅ |
| Login (Participant) | 200 OK with JWT | ✅ |
| Create Event (Organiser) | 201 Created | ✅ |
| Create Event (Participant) | 403 Forbidden | ✅ |
| Create Event (No Token) | 401 Unauthorized | ✅ |

## Unit Tests
Unit tests are included using xUnit and `Microsoft.AspNetCore.Mvc.Testing`. They currently fail due to a known .NET 9 issue where `WebApplicationFactory` cannot locate `testhost.deps.json` — a widely-documented integration-testing problem. The API has been manually verified through Swagger.

## Setup Instructions
1. Clone the repo.
2. Run `database.sql` (from Part 1) in SSMS.
3. Update `appsettings.json` with your SQL Server connection string.
4. Run `dotnet ef database update`.
5. Run `dotnet run --project .\RaceDay.API\RaceDay.API.csproj`.
6. Open `http://localhost:5045/swagger`.

## Roles
- **Organiser** – Create, update, delete events. Requires Organiser JWT.
- **Participant** – View events and enroll. Cannot create or modify events.

## Video Presentation
[Watch the Part 2 walkthrough]
