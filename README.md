# Flower Inventory System

## Overview
A full-stack web application for managing a flower shop's inventory, built with ASP.NET Core (backend), Angular (frontend), and SQL Server (database).

## Features
- CRUD operations for Flowers and Categories
- Category selection for each flower
- Pagination, search, and sorting in the flower list
- Error handling and validation
- Structured logging for all backend actions
- Isolated and robust unit tests

## Technologies Used
- ASP.NET Core
- Entity Framework Core
- Angular
- SQL Server
- HTML/CSS

## Setup Instructions

### 1. Database
- Run the SQL script in `/Flower-Backend/flower_inventory_init.sql` to create and seed the database.
- Update the connection string in `Flower-Backend/appsettings.json` to match your SQL Server instance (uses integrated security by default).

### 2. Backend
```powershell
cd Flower-Backend
# Restore packages and run the backend
dotnet restore
dotnet build
dotnet run
```
The backend will start (default: http://localhost:5130).

### 3. Frontend
```powershell
cd Flower-Angular/FlowerAngularApp
# Install dependencies and start Angular dev server
npm install
npm start
```
The frontend will start (default: http://localhost:4200).

### 4. Usage
- Open http://localhost:4200 in your browser.
- Add, edit, delete, and view flowers and categories.

## Running Unit Tests
To run the backend unit tests:
```powershell
cd Flower-Backend
dotnet test Tests/Tests.csproj
```
This will build the solution and execute all tests in the `Tests` project.

**Troubleshooting:**
- If you get file lock errors (e.g., when files in `bin` or `obj` are in use), make sure the backend server is not running before testing.
- For persistent build issues, delete the `bin` and `obj` folders and rebuild:

## SQL Scripts
- See `/Flower-Backend/flower_inventory_init.sql` for database creation and seed data.

## Development Process
- Regular commits with clear, descriptive messages documenting each feature, fix, or refactor.
- Incremental development: built and tested each feature step by step, ensuring stability before moving on.

## Challenges & Assumptions
- Ensured smooth integration between Angular and ASP.NET Core, especially for data binding and API communication.
- Addressed serialization issues and CORS configuration.
- Designed the database and API to support efficient CRUD operations.
- Assumed each flower is assigned to a single category.

## Logging & Error Handling
- All controller actions are logged using `ILogger<T>` for traceability.
- Improved error handling and response consistency across the API.

## Author
- Giorgos Konosis

## Acknowledgments
- Parts of this project were developed with the assistance of AI tools (e.g., GitHub Copilot, ChatGPT) to improve productivity and code quality.

