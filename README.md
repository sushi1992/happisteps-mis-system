# HappiSteps MIS System

HappiSteps is a modular Management Information System (MIS) designed for early-years and primary education environments.

The system follows **Clean Architecture principles**, with a strong separation between domain logic, application use cases, infrastructure concerns, and API delivery.

---

## 🧱 Architecture Overview

The solution is structured into the following projects:

```
HappiSteps.Api
HappiSteps.Application
HappiSteps.Domain
HappiSteps.Infrastructure
HappiSteps.Contracts
HappiSteps.Domain.Tests
```

### Dependency Flow (important!)

```
Api → Application → Domain
Api → Infrastructure → Domain
Infrastructure → Application
```

> **Domain has zero dependencies**
> **Application depends only on Domain abstractions**
> **Infrastructure implements Application interfaces**

---

## 📦 Project Responsibilities

### 🟦 HappiSteps.Domain

**Pure business logic**

* Domain entities (e.g. `Child`, `ChildIdentifier`)
* Value objects and enums
* Business rules and invariants
* Repository interfaces (e.g. `IChildRepository`)

❌ No EF Core
❌ No ASP.NET
❌ No database concerns

---

### 🟩 HappiSteps.Application

**Use cases and orchestration**

* Application commands & handlers
* Interfaces required by the application (`IChildRepository`, `IUnitOfWork`)
* No infrastructure knowledge

This layer coordinates **what should happen**, not **how**.

---

### 🟥 HappiSteps.Infrastructure

**Technical implementation details**

* Entity Framework Core
* `HappiStepsDbContext`
* Repository implementations
* EF Core configurations & migrations

Implements interfaces defined in **Application**.

---

### 🟨 HappiSteps.Api

**HTTP delivery layer**

* ASP.NET Core controllers
* Dependency injection setup
* Maps HTTP requests → application commands

Contains **no business logic**.

---

### 🟪 HappiSteps.Contracts

**DTOs / API contracts**

* Request and response models
* Shared shapes for API boundaries

Used by API consumers and controllers.

---

### 🧪 HappiSteps.Domain.Tests

**Unit tests for domain logic**

* Tests business rules without EF or infrastructure
* Fast, isolated, deterministic

---

## 🔁 Example Flow (Create Child)

```
HTTP POST /api/children
  ↓
ChildrenController
  ↓
CreateChildHandler (Application)
  ↓
Child.Create(...) (Domain)
  ↓
IChildRepository.AddAsync(...) (Application interface)
  ↓
ChildRepository (Infrastructure / EF Core)
  ↓
Database
```

---

## 🗄️ Persistence & Migrations

* EF Core migrations live in:

  ```
  HappiSteps.Infrastructure/Persistence/Migrations
  ```

* Migrations are generated using the API project as the startup project:

  ```bash
  dotnet ef migrations add InitialCreate \
    --project HappiSteps.Infrastructure \
    --startup-project HappiSteps.Api \
    --output-dir Persistence/Migrations
  ```

---

## 🎯 Design Goals

* Clear separation of concerns
* Domain-first modelling
* Testable business logic
* Replaceable infrastructure
* Long-term maintainability

---

## 🚀 Status

🚧 Early development
Core domain and persistence foundations are in place.

---

## 📜 License

TBC
