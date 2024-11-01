[![Testing](https://github.com/Mohammad-Nayef/Hotel-Booking/actions/workflows/tests.yml/badge.svg)](https://github.com/Mohammad-Nayef/Hotel-Booking/actions/workflows/tests.yml)

# Hotel Booking System 
### The final project of my backend internship at Foothill Technology Solutions.
### ASP.NET WEB API project for booking hotels.

## Features:
- User login and register functionalities.
- Global search functionality for finding hotels by hotel name, hotel description, room type, city name and other properties with text fields.
- Managing images and thumbnails for cities, hotels and rooms.
- Show popular cities which are the most visited ones.
- Email sending service that sends an email to the user when the user creates a booking containing all of the needed information like total price, exact location for the hotel on the map and other details.
- Admin page to search, add, update and delete entities.
  
## System architecture:
Used Clean Architecture which provides great separation of concerns between business logic and external layers and minimal changes for replacing external frameworks like ASP.NET or EF Core.
The system has 4 main layers:
<p align="center">
  <img src="https://github.com/Mohammad-Nayef/Hotel-Booking/blob/main/Clean%20Architecture.png" width="400" height="400" />
</p>

### Core layers:
1. **Domain layer**:
   - Contains the shared models and abstractions for business logic.
   - Has no dependencies on other layers.
2. **Application layer**:
   - Contains the implementation of business logic requirements that are declared in the domain layer and all of the validation logic.
   - Depends on the abstractions in domain layer.
### External layers:
3. **Infrastructure**:
   - Contains the implementation of external services that are declared in the domain layer like data repositories and email service.
   - Depends on the abstractions in domain layer.
4. **Presentation (API) layer**:
   - Contains the implementation of the APIs using the help of business logic abstractions in the domain layer.
   - Depends on the domain layer and has no direct interactions with data repositories.

## Technical features:
- Secure APIs with JWT for authentication and Role-Based Access Control (RBAC) for regular users and admins.
- Passwords are securely stored in the database using [Microsoft.AspNet.Identity.IPasswordHasher](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.ipasswordhasher-1?view=aspnetcore-7.0) which stores hashed passwords using [Password Salting](https://en.wikipedia.org/wiki/Salt_(cryptography)).
- Used SQL Server as a database using the help of EF Core as an ORM.
- Unit testing for units that contains testable logic, architecture testing to make sure that Clean Architecture principles aren't violated and integration testing for the APIs.
- Well-documented clean codes and APIs with best-practices.
- Robust validations for creating and updating entities.
- Logging using [Serilog](https://serilog.net/).
- Rate limiting middleware to limit the number of requests for a client in a customizable time interval.
- Meaningful response status codes and messages.
- API versioning.

## Database Schema:
![Database Schema](https://github.com/Mohammad-Nayef/Hotel-Booking/blob/main/Database%20Diagram.svg)

## API documentation:
- Comprehensive dynamic and up-to-date documentation using SwaggerUI which is available when running the application in development environment.
- Documentation using Postman: https://documenter.getpostman.com/view/30810910/2sA2xiXsN9 (outdated)

## Getting started:
- The project is built on .NET 8.0
- **External packages**: AutoMapper, SixLabors.ImageSharp, Serilog, FluentValidation, Moq, AutoFixture, FluentAssertion, xUnit, NetArchTest.
- Download the project and open the solution file `HotelBooking.sln`.
- Run the migration in Infrastructure layer to update the database.
- The system has an admin that's seeded into the database using seeding migration, the credentials are: Username: `admin`, Password: `admin123`.
