# 🛒 POS System Backend (.NET)

A Point of Sale (POS) system backend built with **ASP.NET Core Web API** following **Clean Architecture principles**.
This project is designed as a mid-level backend application showcasing best practices in scalability, maintainability, and separation of concerns.

---

## 🚀 Features

- 🔐 Authentication & Authorization (JWT & Refresh Tokens)
- 📦 Product Management (CRUD)
- 🗂️ Category Management
- 🧾 Sales Management
- 🧠 Clean Architecture implementation

---

## 🏗️ Architecture

The project follows a layered architecture:

```
POSSystem
├── POSSystem.API → Presentation layer (Controllers)
├── POSSystem.Application → Business logic (Services, DTOs, Mappers)
├── POSSystem.Domain → Core entities
├── POSSystem.Infrastructure → External services
└── POSSystem.Persistence → Database access (EF Core)
```

---

## 🧰 Technologies

- Framework: ASP.NET Core
- ORM: Entity Framework Core
- Database: SQL Server (LocalDB)
- Documentation: Scalar
- Patterns: Clean Architecture, Repository Pattern, Dependency Injection

---

## ⚙️ Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/rodrigofonteladev/POSSystem.git
cd POSSystem
```

---

### 2. Configure database

The project is configured to use SQL Server LocalDB for an easier development setup. Update your connection string in appsettings.json within the POSSystem.API project:

`appsettings.json`

```json
"ConnectionStrings": {
  "connectionSQL": "Server=(localdb)\\mssqllocaldb;Database=POSSystemDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
}
```

> **Note:** If you are using a full SQL Server instance (like SQLExpress), change the server value to `localhost\\SQLExpress`.

---

### 3. Run migrations

```bash
dotnet ef database update -p POSSystem.Persistence -s POSSystem.API
```

---

### 4. Run the project

```bash
dotnet run --project POSSystem.API
```

---

### 5. API Documentation

```
https://localhost:<port>/scalar/v1
```

---

## 📚 Learning Goals

This project demonstrates:

- Clean Architecture in .NET
- Layer separation and dependency management
- Efficient data handling with EF Core and LINQ
- EF Core usage with SQL Server
- Modern API documentation using Scalar

---

## 👨‍💻 Author

Developed by **Rodrigo Fontela**

- Junior Backend Developer
- Specialized in **.NET Ecosystem**

---

## 📄 License

This project is open-source and available under the MIT License.
