# RestaurantAPI

A simple RESTful API for managing a small restaurant: customers, tables, dishes, bookings, and admin operations. Built with .NET 8 and Entity Framework Core.

## Overview

This repository implements a backend API that exposes CRUD operations for restaurant entities and includes seeded data for dishes and tables. The project uses Entity Framework Core for data access and is designed to run locally in Visual Studio 2022 or from the .NET CLI.

## What the API does

- Exposes CRUD endpoints for: `Customer`, `Table`, `Dish`, `Booking`, and `Admin` resources.
- Persists data using Entity Framework Core and a configured database provider.
- Seeds initial `Dish` and `Table` records in `Lab1/Data/RestaurantContext.cs` when the database is created.

## Technologies

- .NET 8
- C# 12
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / LocalDB (configurable via `appsettings.json`)

## Key Features

- Manage Customers, Tables, Dishes, Bookings, and Admin entities
- RESTful API surface for `Customer`, `Table`, `Dish`, `Booking`, and `Admin` resources
- Database seeding for initial Dishes and Tables (see `Lab1/Data/RestaurantContext.cs`)
- Easy local development using Visual Studio 2022 or the .NET CLI

## Typical Endpoints

- `GET /api/dishes` — list dishes
- `GET /api/tables` — list tables
- `GET /api/customers` — list customers
- `POST /api/bookings` — create a booking
- (Other standard `GET`, `POST`, `PUT`, `DELETE` routes per entity)

## Project Structure

- `Lab1/` — main API project
  - `Data/RestaurantContext.cs` — EF Core DbContext and seed data
  - `Models/` — entity classes (Customer, Table, Dish, Booking, Admin)
  - `Program.cs` — app startup and service registration
  - `appsettings.json` — configuration and connection strings
- `README.md` — this file
- `CONTRIBUTING.md` — repository contribution guidelines (add when ready)
- `.editorconfig` — coding style and formatting rules (add when ready)

## Getting Started

### Prerequisites

- Visual Studio 2022 (with .NET 8 workload) or .NET 8 SDK
- (Optional) SQL Server, SQLite, or another provider supported by EF Core

### Clone the repository

git clone https://github.com/seanie1995/RestaurantAPI.git
cd "RestaurantAPI"

### Configure the database

1. Open `Lab1/appsettings.json` and set your connection string under `ConnectionStrings:DefaultConnection` (or the name your `Program.cs` expects).
2. From the project root, install the EF Core CLI if you haven't:

   dotnet tool install --global dotnet-ef

3. Add and apply migrations (example):

   cd Lab1
   dotnet ef migrations add InitialCreate
   dotnet ef database update

**Note:** The project contains seed data for `Dish` and `Table` in `Lab1/Data/RestaurantContext.cs`. When the database is created or migrations are applied, those records will be inserted automatically.

### Run the API

- **Visual Studio 2022:** Open the solution, set the `Lab1` project as startup, then press F5 or __Debug > Start Debugging__.
- **.NET CLI:** From the `Lab1` directory run:

   dotnet run

### API Endpoints

The project exposes endpoints for the main entities (Customers, Tables, Dishes, Bookings, Admin). See the controllers in the `Lab1` project for exact route templates and supported operations.

## Testing

No automated tests are included yet. Add unit and integration tests according to the repository's `CONTRIBUTING.md` guidance.

## Contributing

Please follow the project's coding standards and formatting rules. A `CONTRIBUTING.md` and `.editorconfig` will be included in the repository to formalize contribution steps and style.

## License

This repository does not include a license file. Add a `LICENSE` file (for example MIT) if you intend to publish with a permissive license.

## Contact

For questions or issues, open an issue in the repository.

## Run Locally (Short)

- Open the solution in Visual Studio 2022 and run (F5), or:
- From the `Lab1` folder run: `dotnet run`.

Seeded data and project behavior are defined in `Lab1/Data/RestaurantContext.cs`.
