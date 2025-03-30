# RestaurantPOS - Restaurant Management System

**RestaurantPOS** is a practical and maintainable Restaurant Management System backend developed as a showcase of modern .NET Web API development practices, focusing on clear code structure, SOLID principles, and effective REST API design.

---

## Overview

This project provides a RESTful API designed to efficiently manage common restaurant operations including:

- Menu items management with dynamic categories
- Customer records and reservations
- Order processing
- Payment handling
- Table management and reservation validation

The project serves as a solid example of how to build scalable, readable, and maintainable APIs using the latest .NET 9 features, Entity Framework Core, AutoMapper, and structured exception handling.

---

## Features

- **Comprehensive REST API**

  - Supports CRUD operations for Categories, Menu Items, Orders, Customers, Reservations, Tables, and Payments.

- **Robust Validation**

  - Ensures data integrity through validation checks, such as verifying category existence before creating menu items.

- **Structured Exception Handling**

  - Uses custom middleware for consistent HTTP responses across various scenarios (Resource Not Found, Validation errors, etc.).

- **In-Memory Database**

  - Utilizes Entity Framework Core’s In-memory provider to simplify development and testing.

- **Clear and Modular Codebase**

  - Follows SOLID, DRY, and KISS principles to ensure clarity and ease of future extensions.

- **API Documentation**
  - Integrated Swagger UI for simple and effective documentation and testing of API endpoints.

---

## Technology Stack

- .NET 9 (ASP.NET Core Web API)
- Entity Framework Core (In-Memory DB)
- AutoMapper
- Swagger / OpenAPI
- Dependency Injection (DI)
- C# 12

---

## Project Structure

```
RestaurantPOS
├── Controllers      # API endpoints for resources
├── Data             # Database context and models
├── DTOs             # Data Transfer Objects for requests/responses
├── Entities         # Domain models/entities
├── Enums            # Enumerations used in application
├── Exceptions       # Custom exceptions for consistent error handling
├── Mappings         # AutoMapper profiles for object mapping
├── Middlewares      # Custom middleware for global error handling
├── Repositories     # Data access layer interfaces and implementations
├── Services         # Business logic and orchestration layer
├── Program.cs       # Entry point and DI setup
├── RestaurantPOS.csproj # Project dependencies and configuration
```

---

## How to Run

1. Clone the repository and navigate to the project folder.
2. Restore the dependencies:
   ```bash
   dotnet restore
   ```
3. Run the application:
   ```bash
   dotnet run
   ```
4. Access the API via Swagger at:
   ```
   http://localhost:5095/swagger/
   ```

---

## About Me

**Ahmed Waleed**  
📧 **ahmed.waleed919@outlook.com**

---

## License

This project is licensed under the MIT License - see the LICENSE file for details.
