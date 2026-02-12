# WorkSchedule Planner

A Web application for managing employee work schedules. It allows creating, editing and viewing weekly work plans with a user-friendly interface.

## Tech Stack

- ASP.NET Core MVC (.NET 9)
- Entity Framework Core
- SQL Server
- Razor Views
- Bootstrap
- JavaScript (UI interactivity)
- ASP.NET Core Identity (authentication & authorization)

## Features

- Create and edit weekly work schedules
- Assign shifts to employees
- Interactive tile-based weekly view with expandable shift details
- Multiple user roles (admin, employee)
- Schedule history / archive
- User registration and login
- Shift reports generation

## Architecture

The project follows: 
- **Clean Architecture** principles with clear separation of concerns  
- **CQRS** to separate commands (writes) from queries (reads)
- **Repository** and **Unit of Work** patterns for data access and transactional consistency
- **Result Pattern** for consistent error handling.  

This design makes the application modular, testable, and easy to maintain.

## Running locally

1. **Clone the repository**
   ```bash
   git clone https://github.com/Tymqek22/WorkSchedulePlaner.git
   cd WorkSchedulePlaner
   ```
2. **Configure the database**
   Make sure SQL Server is installed
   Update the connection string in appsettings.json:
   ```csharp
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
   ```bash
   https://localhost:port
   ```

## Future Improvements

- Integration with external calendars (e.g., Google Calendar)
- Email/SMS notifications for employees
- Mobile-friendly / PWA version
- Drag & drop shift assignment
- Public API for external integrations

## Author

- Educational project built to practice ASP.NET Core and MVC architecture following with Clean Architecture and CQRS pattern.
- Author: Tymoteusz Procner
