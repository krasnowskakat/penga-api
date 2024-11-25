# Penga Application

## Overview

Penga Application is a .NET 8.0 based project that provides services for managing categories and costs. It uses Entity Framework Core for data access, FluentValidation for request validation, and MediatR for handling queries. The project also includes unit tests using xUnit and Moq.

## Technologies Used

- .NET 8.0
- Entity Framework Core
- FluentValidation
- MediatR
- Serilog
- xUnit
- Moq

## Project Structure

- **API**: Contains the entry point of the application and middleware configuration.
- **Application**: Contains the business logic, services, and validators.
- **Domain**: Contains the domain models.
- **Infrastructure**: Contains the data access layer.
- **Tests**: Contains unit tests for the application.

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server (or any other database supported by Entity Framework Core)

### Setup

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/penga-application.git
    cd penga-application
    ```

2. Set up the database connection string in `appsettings.json`:
    ```json
    {
      "ConnectionStrings": {
        "PengaDb": "YourConnectionStringHere"
      }
    }
    ```

3. Run the application:
    ```sh
    dotnet run --project API
    ```

4. Open your browser and navigate to `https://localhost:5001/swagger` to view the Swagger UI.

### Running Tests

To run the unit tests, use the following command:
```sh
dotnet test
