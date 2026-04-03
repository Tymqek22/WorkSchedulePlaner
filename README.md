# WorkSchedule Planner

A web application for managing employee work schedules. It enables creating, editing, and visualizing weekly work plans.

## Tech Stack

- ASP.NET Core MVC (.NET 9)
- Entity Framework Core
- SQL Server
- Razor Views
- Bootstrap
- ASP.NET Core Identity (cookie-based authentication & authorization)

## Features

- Create and manage weekly work schedules
- Assign shifts to employees
- Interactive tile-based weekly view with expandable shift details
- Resource-based authorization for schedule management
- Ability to create own user schedules and be a member of other user schedules
- User registration and login

## Architecture

The application follows modern backend design principles:

- **Clean Architecture** – clear separation of domain, application, and infrastructure layers  
- **CQRS** – separation of read and write operations for better scalability and clarity  
- **Rich Domain Model** – business logic encapsulated within domain entities (no anemic models)  
- **Repository & Unit of Work** – abstraction over data access with transactional consistency  
- **Result Pattern** – unified way of handling operation outcomes and errors  

### Authorization Model

- **Authentication** is handled via **ASP.NET Core Identity** using **cookie-based authentication**
- **Authorization** consists of:
  - **Resource-based authorization** for validating access to specific domain objects (e.g., schedules, shifts)

This approach ensures both high-level and fine-grained security across the application.

## Running locally

1. **Clone the repository**
   ```bash
   git clone https://github.com/Tymqek22/WorkSchedulePlaner.git
   cd WorkSchedulePlaner
   ```

2. **Configure the database**  
   Make sure SQL Server is installed and update the connection string in `appsettings.json`:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\SQLEXPRESS;Database=WorkScheduleDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. **Apply migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Open in browser**
   ```
   https://localhost:port
   ```

## Future Improvements

- Integration with external calendars (e.g., Google Calendar)
- Email/SMS notifications
- Mobile-friendly / PWA version
- Drag & drop shift assignment
- Public API for external integrations
- Advanced reporting and analytics
- Better UI design

## Author

Educational project focused on building production-quality backend architecture using ASP.NET Core.

**Author:** Tymoteusz Procner
