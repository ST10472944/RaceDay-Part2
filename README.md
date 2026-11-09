# RaceDay - Full Stack Application (Parts 1, 2 & 3)

This repository contains the complete Portfolio of Evidence for the RaceDay event management system.

## Project Structure
- **Part 1:** ERD, API Endpoints plan, and SQL Database script (`/docs` folder).
- **Part 2:** RESTful API (`/RaceDay.API` folder) - Built with ASP.NET Core 9, EF Core, JWT Authentication, and Azure Blob Storage integration.
- **Part 3:** MVC Web Application (`/RaceDay.MVC` folder) - Consumes the Part 2 API, implements session-based authentication, role-specific interfaces, and is containerised using Docker.

## Roles and Features
- **Organiser:** Can log in, view a dashboard of their events, create new events, and upload event banner images (handled via the API and Azure Blob).
- **Participant:** Can log in, browse all upcoming events, enroll in events, and view their personal race history and results.

## 🚀 Setup and Run Instructions

### Prerequisites
- .NET 9 SDK
- SQL Server (SSMS)
- Docker Desktop (for containerisation)

### 1. Database Setup
Run the `database.sql` script (found in the Part 1 `/docs` folder) in SQL Server Management Studio to create the database and seed the initial data.

### 2. Start the API (Part 2)
1. Open the `RaceDay.API.sln` solution in Visual Studio.
2. Right-click the `RaceDay.API` project and select **Debug** > **Start Without Debugging**.
3. The API will launch and listen on `http://localhost:5045`. Swagger UI is available for testing.

### 3. Start the MVC Web App (Part 3)
1. In Visual Studio, right-click the `RaceDay.MVC` project and select **Debug** > **Start Without Debugging**.
2. The web app will open in your browser (e.g., `https://localhost:7199`).
3. Register a new account as an Organiser or Participant, or use the seeded credentials (Organiser: `organiser1@raceday.co.za` / `Password123!`).

## 🐳 Running with Docker
The MVC application is fully containerised. To build and run the application using Docker, run the following commands from the root directory (where the `.sln` file is located):

```bash
docker build -t raceday-mvc -f RaceDay.MVC/Dockerfile .
docker run -p 8080:8080 raceday-mvc
