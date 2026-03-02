# LabManager.NET

A comprehensive Laboratory Information System (LIS) built with **.NET 8**, following **Clean Architecture** principles. The system consists of an **ASP.NET Core Web API** backend and a **WPF desktop client** using the **MVVM** pattern.

---

## Solution Structure

```
LabManager.NET/
├── LabManager.slnx                    # Solution file
└── src/
    ├── LabManager.Domain/             # Domain layer (entities & enums)
    ├── LabManager.Application/        # Application layer (interfaces & DTOs)
    ├── LabManager.Infrastructure/     # Infrastructure layer (EF Core, repositories)
    ├── LabManager.WebApi/             # ASP.NET Core Web API
    └── LabManager.WPF/                # WPF desktop client (MVVM)
```

---

## Projects

### 1. LabManager.Domain — *Class Library (.NET 8)*

The innermost layer. Contains **pure business entities and enumerations** with no external dependencies.

| Folder | Contents |
|--------|----------|
| `Entities/` | `Lab`, `Equipment`, `Patient`, `TestOrder`, `Sample` |
| `Enums/` | `TestStatus`, `SampleType`, `EquipmentStatus` |

### 2. LabManager.Application — *Class Library (.NET 8)*

The application layer. Defines **repository interfaces** and **Data Transfer Objects (DTOs)**. Depends only on `Domain`.

| Folder | Contents |
|--------|----------|
| `Interfaces/` | `ILabRepository`, `IEquipmentRepository`, `IPatientRepository`, `ITestOrderRepository` |
| `DTOs/` | `LabDto`, `EquipmentDto`, `PatientDto`, `TestOrderDto` |

### 3. LabManager.Infrastructure — *Class Library (.NET 8)*

The infrastructure layer. Contains the **Entity Framework Core DbContext** and **repository implementations**. Depends on `Application` (and transitively `Domain`).

| Folder | Contents |
|--------|----------|
| `Data/` | `LabManagerDbContext` — EF Core DbContext with SQL Server provider |
| `Repositories/` | `LabRepository`, `EquipmentRepository`, `PatientRepository`, `TestOrderRepository` |

**NuGet Packages:**
- `Microsoft.EntityFrameworkCore.SqlServer` 8.0.0
- `Microsoft.EntityFrameworkCore.Design` 8.0.0

### 4. LabManager.WebApi — *ASP.NET Core Web API (.NET 8)*

The API layer. Exposes RESTful endpoints for all resources. Registers services via dependency injection and configures Swagger UI.

| Folder | Contents |
|--------|----------|
| `Controllers/` | `LabsController`, `EquipmentController`, `PatientsController`, `TestOrdersController` |

**Endpoints:**

| Controller | Routes |
|------------|--------|
| `LabsController` | `GET/POST /api/labs`, `GET/PUT/DELETE /api/labs/{id}` |
| `EquipmentController` | `GET/POST /api/equipment`, `GET/PUT/DELETE /api/equipment/{id}`, `GET /api/equipment/lab/{labId}` |
| `PatientsController` | `GET/POST /api/patients`, `GET/PUT/DELETE /api/patients/{id}` |
| `TestOrdersController` | `GET/POST /api/testorders`, `GET/PUT/DELETE /api/testorders/{id}`, `GET /api/testorders/patient/{patientId}` |

### 5. LabManager.WPF — *WPF Desktop Application (.NET 8 / Windows)*

A Windows desktop client following the **MVVM (Model-View-ViewModel)** pattern. Uses data binding, commands, and `INotifyPropertyChanged` for clean separation of concerns.

| Folder | Contents |
|--------|----------|
| `Commands/` | `RelayCommand` — reusable `ICommand` implementation |
| `Models/` | `LabModel`, `PatientModel` — client-side models |
| `ViewModels/` | `ViewModelBase`, `MainViewModel`, `LabsViewModel`, `PatientsViewModel`, `TestOrdersViewModel` |
| `Views/` | `LabsView.xaml`, `PatientsView.xaml`, `TestOrdersView.xaml` — DataGrid-based views |

---

## Architecture Overview

```
┌──────────────────────────────────────────────┐
│               LabManager.WPF                 │  WPF Desktop (MVVM)
└──────────────────────┬───────────────────────┘
                       │ HTTP
┌──────────────────────▼───────────────────────┐
│             LabManager.WebApi                │  ASP.NET Core Web API
└──────────────────────┬───────────────────────┘
                       │ depends on
┌──────────────────────▼───────────────────────┐
│          LabManager.Infrastructure           │  EF Core + Repositories
└──────────────────────┬───────────────────────┘
                       │ implements
┌──────────────────────▼───────────────────────┐
│           LabManager.Application             │  Interfaces + DTOs
└──────────────────────┬───────────────────────┘
                       │ depends on
┌──────────────────────▼───────────────────────┐
│             LabManager.Domain                │  Entities + Enums
└──────────────────────────────────────────────┘
```

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- SQL Server or SQL Server LocalDB (for the API)
- Windows (for the WPF client)

### Database Setup

1. Update the connection string in `src/LabManager.WebApi/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LabManagerDb;Trusted_Connection=True"
   }
   ```

2. Run EF Core migrations from the repository root:
   ```bash
   dotnet ef migrations add InitialCreate --project src/LabManager.Infrastructure --startup-project src/LabManager.WebApi
   dotnet ef database update --project src/LabManager.Infrastructure --startup-project src/LabManager.WebApi
   ```

### Running the API

```bash
cd src/LabManager.WebApi
dotnet run
```

The Swagger UI will be available at `https://localhost:{port}/swagger`.

### Running the WPF Client

```bash
cd src/LabManager.WPF
dotnet run
```

> **Note:** The WPF project targets `net8.0-windows` and requires Windows to run.

### Building the Full Solution

```bash
dotnet build LabManager.slnx
```

---

## Features

- 🔬 **Lab Management** — Create and manage laboratory locations and details
- 🧪 **Equipment Tracking** — Track lab equipment status and maintenance schedules
- 👤 **Patient Records** — Manage patient demographics and history
- 📋 **Test Orders** — Create and track test orders through their lifecycle
- 🧫 **Sample Management** — Associate samples with test orders
- 📊 **Swagger UI** — Interactive API documentation
- 🖥️ **Desktop Client** — Native WPF application with MVVM pattern

---

## Technology Stack

| Component | Technology |
|-----------|-----------|
| Backend API | ASP.NET Core 8 Web API |
| ORM | Entity Framework Core 8 |
| Database | SQL Server / LocalDB |
| Desktop Client | WPF (.NET 8 Windows) |
| Architecture | Clean Architecture + MVVM |
| API Documentation | Swagger / OpenAPI |

---

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.

