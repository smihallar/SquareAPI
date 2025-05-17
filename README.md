# SquareAPI

A simple ASP.NET Core Web API for managing colored squares on a grid. Squares are persisted in a local JSON file. The API supports adding, listing, resetting, and deleting squares, and provides interactive documentation via Swagger.

## Features

- Add a new square with a unique color and calculated grid position
- List all squares (with coordinates and color)
- Reset (clear) all squares
- Delete the last square added
- SwashBuckle for OpenAPI/Swagger documentation

## Technologies

- .NET 8
- ASP.NET Core Web API
- System.Text.Json for file-based persistence
- Swagger/OpenAPI

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Running the API

1. Clone the repository
   ```bash
   git clone https://github.com/your-username/SquareAPI.git
   cd SquareAPI
2. Build and run the project:
   ```bash
   dotnet run
3. Access Swagger UI for interactive API docs
   ```bash
   https://localhost:{port}/swagger

## API Endpoints

- `GET /api/squares` – List all squares
- `POST /api/squares` – Add a new square
- `PUT /api/squares` – Reset all squares
- `DELETE /api/squares` – Delete the last square

## Project Structure

- `Controllers/` – API controllers
- `Services/` – Business logic
- `Repositories/` – Data access and persistence
- `Models/` – Data models
- `DTOs/` – Data transfer objects

## Data Persistence

Squares are stored in a local `squares.json` file in the project directory. The repository layer handles all file operations with error handling.

## Created by
Smilla Hallgren Larsson
   
