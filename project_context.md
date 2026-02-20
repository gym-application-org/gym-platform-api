# GymPlatformApi - Project Context

## Overview

**GymPlatformApi** is a multi-tenant gym/fitness center management REST API built with **.NET 8** and **C#**. It enables gym businesses (tenants) to manage their members, subscriptions, workout programs, diet plans, attendance tracking, and internal support tickets. The system is designed as a SaaS-style backend where each tenant (gym) operates in data isolation.

---

## Architecture

The project follows **Clean Architecture** (also known as Onion Architecture) powered by the **nArchitecture** framework. It uses **CQRS** (Command Query Responsibility Segregation) via **MediatR** and enforces separation of concerns across four distinct layers.

### Layer Structure

```
src/gymplatformapi/
├── Domain/            → Entities, enums, value objects (innermost layer, zero dependencies)
├── Application/       → Business logic, CQRS handlers, validation, services, rules
├── Persistence/       → EF Core DbContext, repositories, entity configurations
├── Infrastructure/    → External service adapters (e.g. Cloudinary image upload)
└── WebAPI/            → ASP.NET Core controllers, middleware, DI bootstrap
```

### Core Packages (Git Submodule)

```
src/corePackages/      → Shared nArchitecture framework libraries
├── Core.Application   → Base CQRS pipeline behaviors (auth, caching, logging, validation, transactions)
├── Core.Persistence   → Generic repository pattern, EF Core abstractions, paging
├── Core.Security      → JWT token generation, user/role entities, hashing, encryption
├── Core.WebAPI        → Swagger extensions, exception middleware
├── Core.CrossCuttingConcerns → Exception handling, Serilog logging
├── Core.Mailing       → MailKit email service
├── Core.ElasticSearch  → ElasticSearch integration
├── Core.Localization.* → YAML-based i18n / response localization
├── Core.Translation.*  → Amazon Translate integration
└── Core.Test           → Test utilities
```

---

## Key Patterns & Conventions

### CQRS with MediatR

Every operation is modeled as either a **Command** (write) or a **Query** (read), each handled by a dedicated `IRequestHandler`. Commands and queries are self-contained classes that declare their own cross-cutting concerns via interfaces.

**Feature folder structure** (per entity):

```
Application/Features/{Entity}/
├── Commands/
│   ├── Create/   → CreateXCommand.cs, CreateXCommandValidator.cs, CreatedXResponse.cs
│   ├── Update/   → UpdateXCommand.cs, UpdateXCommandValidator.cs, UpdatedXResponse.cs
│   └── Delete/   → DeleteXCommand.cs, DeletedXCommandValidator.cs, DeletedXResponse.cs
├── Queries/
│   ├── GetById/  → GetByIdXQuery.cs, GetByIdXResponse.cs
│   └── GetList/  → GetListXQuery.cs, GetListXListItemDto.cs
├── Constants/    → XOperationClaims.cs, XBusinessMessages.cs
├── Profiles/     → MappingProfiles.cs (AutoMapper)
├── Rules/        → XBusinessRules.cs
└── Resources/Locales/ → X.en.yaml (localization strings)
```

### Pipeline Behaviors (MediatR middleware)

Registered in order in `ApplicationServiceRegistration.cs`:

| Behavior | Interface | Purpose |
|---|---|---|
| `TenantAuthorizationBehavior` | `ITenantRequest` | Validates user belongs to the requested tenant |
| `AuthorizationBehavior` | `ISecuredRequest` | Role-based access control via operation claims |
| `CachingBehavior` | `ICachableRequest` | Distributed cache reads (InMemory / Redis-ready) |
| `CacheRemovingBehavior` | `ICacheRemoverRequest` | Cache invalidation on writes |
| `LoggingBehavior` | `ILoggableRequest` | Structured logging via Serilog |
| `RequestValidationBehavior` | FluentValidation | Input validation before handler execution |
| `TransactionScopeBehavior` | `ITransactionalRequest` | Wraps handler in a DB transaction |

### Multi-Tenancy

- **`TenantEntity<T>`** is the base class for all tenant-scoped entities. It adds a `TenantId` (Guid) property.
- Tenant context is resolved from the HTTP request header (`X-Tenant-Id`) via `CurrentTenant` service.
- `TenantAuthorizationBehavior` ensures the authenticated user has membership in the requested tenant before any tenant-scoped operation proceeds.
- `Exercise` is the only global (non-tenant) entity, extending `Entity<T>` directly.

### Authentication & Authorization

- **JWT Bearer** authentication with access tokens and HTTP-only cookie refresh tokens.
- Supports **email authenticator** and **OTP authenticator** (2FA).
- Operation claims define fine-grained permissions per feature (e.g. `Members.Create`, `Members.Read`).
- Users are linked to claims via `UserOperationClaim` junction entity.

---

## Domain Model

### Entities

| Entity | Base | PK Type | Description |
|---|---|---|---|
| **Tenant** | `Entity<Guid>` | Guid | A gym business / organization |
| **Member** | `TenantEntity<Guid>` | Guid | Gym member, linked to a `User` account |
| **Staff** | `TenantEntity<Guid>` | Guid | Gym staff/trainer, linked to a `User` account |
| **SubscriptionPlan** | `TenantEntity<int>` | int | Membership plan templates (name, duration, price) |
| **Subscription** | `TenantEntity<int>` | int | Active member subscription (snapshot of plan at purchase time) |
| **Gate** | `TenantEntity<int>` | int | Physical entry gates (turnstiles) at the gym |
| **AttendanceLog** | `TenantEntity<int>` | int | Entry attempt record (allowed/denied per member per gate) |
| **Exercise** | `Entity<int>` | int | Global exercise catalog (not tenant-scoped) |
| **WorkoutTemplate** | `TenantEntity<int>` | int | Reusable workout program template |
| **WorkoutTemplateDay** | `TenantEntity<int>` | int | A day within a workout template |
| **WorkoutTemplateDayExercise** | `TenantEntity<int>` | int | An exercise entry within a workout day |
| **WorkoutAssignment** | `TenantEntity<int>` | int | Assigns a workout template to a member |
| **DietTemplate** | `TenantEntity<int>` | int | Reusable diet/nutrition plan template |
| **DietTemplateDay** | `TenantEntity<int>` | int | A day within a diet template |
| **DietTemplateMeal** | `TenantEntity<int>` | int | A meal within a diet day |
| **DietTemplateMealItem** | `TenantEntity<int>` | int | A food item within a meal |
| **DietAssignment** | `TenantEntity<int>` | int | Assigns a diet template to a member |
| **ProgressEntry** | `TenantEntity<int>` | int | Body measurements & progress tracking per member |
| **SupportTicket** | `TenantEntity<int>` | int | Internal support tickets created by staff |

### Enums

| Enum | Values |
|---|---|
| `MemberStatus` | Active, Suspended |
| `StaffRole` | Owner, Staff |
| `SubscriptionStatus` | Active, Expired, Frozen, Cancelled |
| `AssignmentStatus` | Active, Completed, Cancelled |
| `AttendanceResult` | Allowed, Denied |
| `DifficultyLevel` | Beginner, Intermediate, Advanced |
| `TicketStatus` | Open, InProgress, Resolved, Closed |
| `TicketPriority` | Low, Medium, High, Urgent |

### Entity Relationships

```
Tenant (1) ──── (*) Member
                     ├── (*) Subscription ──── (1) SubscriptionPlan
                     ├── (*) AttendanceLog ──── (1) Gate
                     ├── (*) WorkoutAssignment ──── (1) WorkoutTemplate
                     ├── (*) DietAssignment ──── (1) DietTemplate
                     └── (*) ProgressEntry

Member (1) ──── (1) User (Core.Security)
Staff  (1) ──── (1) User (Core.Security)

WorkoutTemplate (1) ──── (*) WorkoutTemplateDay (1) ──── (*) WorkoutTemplateDayExercise ──── (0..1) Exercise

DietTemplate (1) ──── (*) DietTemplateDay (1) ──── (*) DietTemplateMeal (1) ──── (*) DietTemplateMealItem

Staff (1) ──── (*) SupportTicket (created by)
```

---

## API Features (Controllers)

All controllers inherit from `BaseController` and follow the pattern `api/[controller]`.

| Controller | Endpoints | Notes |
|---|---|---|
| `AuthController` | Login, Register, RefreshToken, RevokeToken, Enable/Verify Email & OTP auth | JWT-based auth |
| `MembersController` | CRUD + paginated list | |
| `StaffsController` | CRUD + paginated list | |
| `TenantsController` | CRUD + paginated list | |
| `SubscriptionPlansController` | CRUD + paginated list | |
| `SubscriptionsController` | CRUD + paginated list | |
| `GatesController` | CRUD + paginated list | |
| `AttendanceLogsController` | CRUD + paginated list | |
| `ExercisesController` | CRUD + paginated list | Global (not tenant-scoped) |
| `WorkoutTemplatesController` | CRUD + paginated list | |
| `WorkoutTemplateDaysController` | CRUD + paginated list | |
| `WorkoutTemplateDayExercisesController` | CRUD + paginated list | |
| `WorkoutAssignmentsController` | CRUD + paginated list | |
| `DietTemplatesController` | CRUD + paginated list | |
| `DietTemplateDaysController` | CRUD + paginated list | |
| `DietTemplateMealsController` | CRUD + paginated list | |
| `DietTemplateMealItemsController` | CRUD + paginated list | |
| `DietAssignmentsController` | CRUD + paginated list | |
| `ProgressEntriesController` | CRUD + paginated list | |
| `SupportTicketsController` | CRUD + paginated list | |
| `UsersController` | User management | Core security |
| `OperationClaimsController` | Permission management | Core security |
| `UserOperationClaimsController` | User-permission assignments | Core security |

---

## Technology Stack

| Category | Technology |
|---|---|
| **Runtime** | .NET 8 (SDK 8.0.417) |
| **Web Framework** | ASP.NET Core 8 |
| **ORM** | Entity Framework Core 8.0.8 |
| **Database** | SQL Server (configured), InMemory (current default) |
| **CQRS / Mediator** | MediatR 12.2.0 |
| **Mapping** | AutoMapper 12.0.1 |
| **Validation** | FluentValidation |
| **Authentication** | JWT Bearer (System.IdentityModel.Tokens.Jwt 7.3.0) |
| **Caching** | Distributed Memory Cache (Redis-ready via StackExchange.Redis) |
| **Logging** | Serilog (file, SQL, Elasticsearch, etc.) |
| **Image Storage** | Cloudinary (via Infrastructure adapter) |
| **Email** | MailKit |
| **Search** | ElasticSearch |
| **Localization** | YAML resource files |
| **API Docs** | Swagger / Swashbuckle 6.5.0 |
| **Code Formatting** | CSharpier |

---

## Database

- **Current configuration**: EF Core **InMemory** database (`"BaseDb"`) in `PersistenceServiceRegistration.cs`.
- **Production-ready**: SQL Server provider is installed and connection string placeholder exists in `appsettings.json`.
- Entity configurations are applied via `IEntityTypeConfiguration<T>` classes in `Persistence/EntityConfigurations/`.
- Auto-migration applier middleware runs on startup (`UseDbMigrationApplier()`).

---

## CI/CD & DevOps

| File | Purpose |
|---|---|
| `.github/workflows/dotnet.yml` | GitHub Actions - build & test |
| `.github/workflows/release.yml` | GitHub Actions - release pipeline |
| `.azure/azure-pipelines.development.yml` | Azure DevOps - development environment |
| `.azure/azure-pipelines.staging.yml` | Azure DevOps - staging environment |
| `.azure/azure-pipelines.production.yml` | Azure DevOps - production environment |

---

## Testing

- Test project: `tests/GymPlatformApi.Application.Tests/`
- Located under `Features/` and `Mocks/` directories
- Uses the `Core.Test` package from corePackages

---

## Project Conventions

1. **Feature-sliced architecture**: Each domain concept is a self-contained feature folder with its own commands, queries, validators, rules, mapping profiles, and constants.
2. **Subscription snapshot pattern**: When a subscription is created, plan details (name, price, duration) are copied to the subscription record, preserving historical accuracy.
3. **Localized business exceptions**: Business rules throw exceptions using localization keys resolved from YAML resource files.
4. **Operation claims pattern**: Each feature defines granular permissions (Admin, Read, Write, Create, Update, Delete).
5. **Semantic commit messages**: The project follows semantic commit conventions (documented in `docs/Semantic Commit Messages.md`).
