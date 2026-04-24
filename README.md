# Gym Platform API

A comprehensive multi-tenant gym management REST API built with .NET 8 and Clean Architecture principles. This platform enables gym businesses to efficiently manage members, subscriptions, workout programs, diet plans, attendance tracking, and support operations through a SaaS-style backend with complete data isolation per tenant.

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Architecture](#architecture)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
  - [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Multi-Tenancy](#multi-tenancy)
- [Authentication & Authorization](#authentication--authorization)
- [Database](#database)
- [Testing](#testing)
- [Deployment](#deployment)
- [License](#license)
- [Contributing](#contributing)

## Overview

**Gym Platform API** is a production-ready, enterprise-grade REST API designed for fitness center management. Built on the **nArchitecture** framework, it implements **Clean Architecture** (Onion Architecture) with **CQRS** pattern via **MediatR**, ensuring maintainability, scalability, and testability.

The platform supports complete gym operations including:
- Member and staff management
- Subscription and payment tracking
- Workout template creation and assignment
- Diet plan management and assignment
- Attendance logging with gate integration
- Progress tracking and measurements
- Internal support ticket system

## Key Features

### Core Capabilities
- **Multi-Tenant Architecture**: Complete data isolation per gym with tenant-scoped operations
- **CQRS Pattern**: Separation of read and write operations using MediatR
- **JWT Authentication**: Secure access token and HTTP-only cookie refresh tokens
- **Two-Factor Authentication**: Email authenticator and OTP support
- **Role-Based Access Control**: Fine-grained permissions via operation claims
- **Dynamic Filtering & Pagination**: Advanced querying capabilities
- **Distributed Caching**: In-memory cache with Redis support
- **Localization Support**: YAML-based multi-language responses
- **Image Management**: Cloudinary integration for media storage
- **Audit Logging**: Comprehensive activity tracking via Serilog
- **API Documentation**: Interactive Swagger/OpenAPI documentation
- **Health Monitoring**: Built-in health check endpoints

### Business Features
- **Member Management**: Complete CRUD operations with status tracking
- **Staff Management**: Role-based staff administration (Owner, Staff)
- **Subscription Plans**: Flexible plan templates with pricing
- **Subscription Management**: Active subscription tracking with status (Active, Expired, Frozen, Cancelled)
- **Attendance System**: Gate-based entry logging with allow/deny results
- **Workout Templates**: Multi-day workout program creation with exercise assignments
- **Diet Templates**: Comprehensive meal planning with nutritional tracking
- **Progress Tracking**: Body measurements and progress monitoring
- **Support Tickets**: Internal ticket management with priority and status tracking

## Architecture

This project follows **Clean Architecture** principles with clear separation of concerns across four distinct layers:

```
src/gymplatformapi/
├── Domain/            # Entities, enums, value objects (zero dependencies)
├── Application/       # Business logic, CQRS handlers, validation, services
├── Persistence/       # EF Core DbContext, repositories, configurations
├── Infrastructure/    # External service integrations (Cloudinary)
└── WebAPI/            # Controllers, middleware, dependency injection
```

### Layer Responsibilities

| Layer | Description | Dependencies |
|-------|-------------|--------------|
| **Domain** | Core business entities and value objects | None |
| **Application** | Business rules, CQRS commands/queries, validation | Domain |
| **Persistence** | Data access, EF Core implementation | Application, Domain |
| **Infrastructure** | External services (image storage, email) | Application |
| **WebAPI** | HTTP endpoints, middleware, DI configuration | All layers |

### Core Packages (Git Submodule)

The project leverages the **nArchitecture** framework through shared core packages:

```
src/corePackages/
├── Core.Application               # Base CQRS pipeline behaviors
├── Core.Persistence               # Generic repository, paging, EF Core abstractions
├── Core.Security                  # JWT, user/role entities, encryption
├── Core.WebAPI                    # Swagger extensions, exception middleware
├── Core.CrossCuttingConcerns      # Exception handling, Serilog logging
├── Core.Mailing                   # MailKit email service
├── Core.ElasticSearch             # ElasticSearch integration
├── Core.Localization.*            # YAML-based i18n support
├── Core.Translation.*             # Amazon Translate integration
└── Core.Test                      # Test utilities
```

### CQRS Pattern with MediatR

Every operation is modeled as either a **Command** (write) or **Query** (read), each handled by a dedicated `IRequestHandler`. The feature folder structure per entity:

```
Application/Features/{Entity}/
├── Commands/
│   ├── Create/           # CreateXCommand, Validator, Response
│   ├── Update/           # UpdateXCommand, Validator, Response
│   └── Delete/           # DeleteXCommand, Validator, Response
├── Queries/
│   ├── GetById/          # GetByIdXQuery, Response
│   └── GetList/          # GetListXQuery, ListItemDto
├── Constants/            # XOperationClaims, XBusinessMessages
├── Profiles/             # AutoMapper profiles
├── Rules/                # XBusinessRules
└── Resources/Locales/    # X.en.yaml, X.tr.yaml
```

### Pipeline Behaviors

MediatR pipeline behaviors are executed in order for cross-cutting concerns:

| Behavior | Interface | Purpose |
|----------|-----------|---------|
| `TenantAuthorizationBehavior` | `ITenantRequest` | Validates user belongs to the requested tenant |
| `AuthorizationBehavior` | `ISecuredRequest` | Role-based access control via operation claims |
| `CachingBehavior` | `ICachableRequest` | Distributed cache reads |
| `CacheRemovingBehavior` | `ICacheRemoverRequest` | Cache invalidation on writes |
| `LoggingBehavior` | `ILoggableRequest` | Structured logging via Serilog |
| `RequestValidationBehavior` | FluentValidation | Input validation before handler execution |
| `TransactionScopeBehavior` | `ITransactionalRequest` | Wraps handler in a DB transaction |

## Technology Stack

| Category | Technology |
|----------|------------|
| **Runtime** | .NET 8 (SDK 8.0.417) |
| **Web Framework** | ASP.NET Core 8 |
| **ORM** | Entity Framework Core 8.0.8 |
| **Database** | SQL Server, PostgreSQL (InMemory default) |
| **CQRS/Mediator** | MediatR 12.2.0 |
| **Object Mapping** | AutoMapper 12.0.1 |
| **Validation** | FluentValidation |
| **Authentication** | JWT Bearer (System.IdentityModel.Tokens.Jwt 7.3.0) |
| **Caching** | Distributed Memory Cache, Redis (StackExchange.Redis) |
| **Logging** | Serilog (File, SQL, Elasticsearch) |
| **Image Storage** | Cloudinary |
| **Email** | MailKit |
| **Search** | ElasticSearch |
| **Localization** | YAML Resource Files |
| **API Documentation** | Swagger/Swashbuckle 6.5.0 |
| **Code Formatting** | CSharpier |

## Project Structure

```
gym-platform-api/
├── .azure/                           # Azure DevOps CI/CD pipelines
│   ├── azure-pipelines.development.yml
│   ├── azure-pipelines.staging.yml
│   └── azure-pipelines.production.yml
├── .ebextensions/                    # AWS Elastic Beanstalk configuration
├── .github/workflows/                # GitHub Actions workflows
│   ├── dotnet.yml                   # Build & test
│   └── release.yml                  # Release pipeline
├── docs/                             # Documentation
│   └── Semantic Commit Messages.md
├── src/
│   ├── corePackages/                # nArchitecture framework (git submodule)
│   │   ├── Core.Application/
│   │   ├── Core.Persistence/
│   │   ├── Core.Security/
│   │   ├── Core.WebAPI/
│   │   └── ...
│   └── gymplatformapi/              # Main application
│       ├── Domain/                  # Business entities & enums
│       │   └── Entities/            # Member, Staff, Tenant, Subscription, etc.
│       ├── Application/             # Business logic layer
│       │   ├── Features/            # CQRS commands & queries
│       │   │   ├── Members/
│       │   │   ├── Staffs/
│       │   │   ├── Tenants/
│       │   │   ├── Subscriptions/
│       │   │   ├── WorkoutTemplates/
│       │   │   ├── DietTemplates/
│       │   │   ├── AttendanceLogs/
│       │   │   ├── ProgressEntries/
│       │   │   ├── SupportTickets/
│       │   │   └── ...
│       │   └── Services/            # Business services & repositories
│       ├── Persistence/             # Data access layer
│       │   ├── Contexts/            # EF Core DbContext
│       │   ├── EntityConfigurations/
│       │   ├── Repositories/
│       │   └── Migrations/
│       ├── Infrastructure/          # External integrations
│       │   └── Adapters/            # Cloudinary, etc.
│       └── WebAPI/                  # Presentation layer
│           ├── Controllers/         # API endpoints
│           └── Program.cs           # Application entry point
└── tests/
    └── GymPlatformApi.Application.Tests/
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (version 8.0.417 or higher)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [PostgreSQL](https://www.postgresql.org/download/) (optional, InMemory used by default)
- [Redis](https://redis.io/download) (optional, for distributed caching)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (optional, for containerized services)
- IDE: [Visual Studio 2022](https://visualstudio.microsoft.com/), [JetBrains Rider](https://www.jetbrains.com/rider/), or [VS Code](https://code.visualstudio.com/)

### Installation

1. **Clone the repository**

```bash
git clone https://github.com/yourusername/gym-platform-api.git
cd gym-platform-api
```

2. **Initialize git submodules** (for corePackages)

```bash
git submodule update --init --recursive
```

3. **Restore dependencies**

```bash
dotnet restore GymPlatformApi.sln
```

4. **Build the solution**

```bash
dotnet build GymPlatformApi.sln --configuration Release
```

### Configuration

#### Database Configuration

By default, the application uses **EF Core InMemory** database for quick testing. To use a persistent database:

1. Open `src/gymplatformapi/Persistence/PersistenceServiceRegistration.cs`
2. Switch from InMemory to SQL Server or PostgreSQL:

```csharp
// Comment out InMemory provider:
// services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("BaseDb"));

// Uncomment SQL Server:
services.AddDbContext<BaseDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

// Or PostgreSQL:
services.AddDbContext<BaseDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("PostgreSql")));
```

3. Update connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "SqlServer": "Server=localhost;Database=GymPlatformDb;User Id=sa;Password=YourStrong@Password;TrustServerCertificate=True;",
  "PostgreSql": "Host=localhost;Port=5432;Database=GymPlatformDb;Username=postgres;Password=YourPassword;"
}
```

#### JWT Configuration

Update JWT settings in `appsettings.json`:

```json
"TokenOptions": {
  "AccessTokenExpiration": 30,
  "Audience": "gymplatformapi@yourdomain.com",
  "Issuer": "nArchitecture@yourdomain.com",
  "RefreshTokenTTL": 2,
  "SecurityKey": "YourVeryStrongSecurityKeyHereMustBeLongEnough"
}
```

Generate a secure key using the provided script:

```bash
# Windows PowerShell
.\generate-jwt-key.ps1

# Linux/Mac
openssl rand -base64 64
```

#### CORS Configuration

Configure allowed origins in `appsettings.json`:

```json
"WebAPIConfiguration": {
  "APIDomain": "https://api.yourdomain.com/api",
  "AllowedOrigins": [
    "https://yourdomain.com",
    "http://localhost:4200"
  ]
}
```

#### Email Configuration

Configure SMTP settings for email features:

```json
"MailSettings": {
  "Server": "smtp.gmail.com",
  "Port": 587,
  "SenderEmail": "noreply@yourdomain.com",
  "SenderFullName": "Gym Platform",
  "UserName": "your-email@gmail.com",
  "Password": "your-app-specific-password",
  "AuthenticationRequired": true
}
```

#### Cloudinary Configuration (Image Storage)

Sign up at [Cloudinary](https://cloudinary.com/) and add your credentials:

```json
"CloudinaryAccount": {
  "Cloud": "your-cloud-name",
  "ApiKey": "your-api-key",
  "ApiSecret": "your-api-secret"
}
```

### Running the Application

#### Development Mode

```bash
cd src/gymplatformapi/WebAPI
dotnet run
```

Or with hot reload:

```bash
dotnet watch run
```

The API will be available at:
- **HTTP**: `http://localhost:5278`
- **HTTPS**: `https://localhost:7278`
- **Swagger UI**: `https://localhost:7278/swagger`

#### Production Build

```bash
dotnet publish src/gymplatformapi/WebAPI/WebAPI.csproj -c Release -o ./publish
cd publish
dotnet WebAPI.dll
```

#### Using Docker

```bash
# Build Docker image
docker build -t gym-platform-api:latest .

# Run container
docker run -d -p 8080:80 -p 8081:443 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  --name gym-api gym-platform-api:latest
```

## API Documentation

### Swagger UI

Interactive API documentation is available at `/swagger` when running in development mode.

### Base URL

```
https://localhost:7278/api
```

### Authentication Header

Most endpoints require JWT authentication:

```
Authorization: Bearer <your-jwt-token>
```

### Tenant Header

Tenant-scoped endpoints require:

```
X-Tenant-Id: <tenant-guid>
```

### API Endpoints

#### Authentication

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/auth/login` | User login (returns access & refresh tokens) |
| POST | `/auth/register` | User registration |
| POST | `/auth/refresh-token` | Refresh access token |
| POST | `/auth/revoke-token` | Revoke refresh token |
| POST | `/auth/enable-email-authenticator` | Enable email 2FA |
| POST | `/auth/verify-email-authenticator` | Verify email 2FA code |
| POST | `/auth/enable-otp-authenticator` | Enable OTP 2FA |
| POST | `/auth/verify-otp-authenticator` | Verify OTP 2FA code |

#### Tenants

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/tenants` | List all tenants (paginated) |
| GET | `/tenants/{id}` | Get tenant by ID |
| POST | `/tenants` | Create new tenant |
| PUT | `/tenants` | Update tenant |
| DELETE | `/tenants/{id}` | Delete tenant |

#### Members

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/members` | List all members (paginated) |
| GET | `/members/{id}` | Get member by ID |
| POST | `/members` | Create new member |
| PUT | `/members` | Update member |
| DELETE | `/members/{id}` | Delete member |

#### Staffs

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/staffs` | List all staff (paginated) |
| GET | `/staffs/{id}` | Get staff by ID |
| POST | `/staffs` | Create new staff |
| PUT | `/staffs` | Update staff |
| DELETE | `/staffs/{id}` | Delete staff |

#### Subscription Plans

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/subscriptionplans` | List all plans (paginated) |
| GET | `/subscriptionplans/{id}` | Get plan by ID |
| POST | `/subscriptionplans` | Create new plan |
| PUT | `/subscriptionplans` | Update plan |
| DELETE | `/subscriptionplans/{id}` | Delete plan |

#### Subscriptions

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/subscriptions` | List all subscriptions (paginated) |
| GET | `/subscriptions/{id}` | Get subscription by ID |
| POST | `/subscriptions` | Create new subscription |
| PUT | `/subscriptions` | Update subscription |
| DELETE | `/subscriptions/{id}` | Delete subscription |

#### Workout Templates

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/workouttemplates` | List all workout templates (paginated) |
| GET | `/workouttemplates/{id}` | Get template by ID |
| POST | `/workouttemplates` | Create new template |
| PUT | `/workouttemplates` | Update template |
| DELETE | `/workouttemplates/{id}` | Delete template |

#### Diet Templates

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/diettemplates` | List all diet templates (paginated) |
| GET | `/diettemplates/{id}` | Get template by ID |
| POST | `/diettemplates` | Create new template |
| PUT | `/diettemplates` | Update template |
| DELETE | `/diettemplates/{id}` | Delete template |

#### Attendance Logs

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/attendancelogs` | List all logs (paginated) |
| GET | `/attendancelogs/{id}` | Get log by ID |
| POST | `/attendancelogs` | Log attendance entry |

#### Progress Entries

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/progressentries` | List all entries (paginated) |
| GET | `/progressentries/{id}` | Get entry by ID |
| POST | `/progressentries` | Create progress entry |
| PUT | `/progressentries` | Update entry |
| DELETE | `/progressentries/{id}` | Delete entry |

#### Support Tickets

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/supporttickets` | List all tickets (paginated) |
| GET | `/supporttickets/{id}` | Get ticket by ID |
| POST | `/supporttickets` | Create new ticket |
| PUT | `/supporttickets` | Update ticket |
| DELETE | `/supporttickets/{id}` | Delete ticket |

#### Health Check

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/health` | API health status |

### Example Request

```bash
# Login
curl -X POST "https://localhost:7278/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "Password123!"
  }'

# Get Members (with tenant and auth)
curl -X GET "https://localhost:7278/api/members?PageIndex=0&PageSize=10" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "X-Tenant-Id: 3fa85f64-5717-4562-b3fc-2c963f66afa6"
```

## Multi-Tenancy

### How It Works

- Each tenant (gym) has complete data isolation
- `TenantEntity<T>` base class adds `TenantId` property to all tenant-scoped entities
- Tenant context is resolved from HTTP header `X-Tenant-Id`
- `TenantAuthorizationBehavior` validates user membership before operations
- Only `Exercise` entity is global (shared across all tenants)

### Creating a Tenant

```http
POST /api/tenants
Content-Type: application/json

{
  "name": "Fitness Center Pro",
  "contactEmail": "admin@fitnesscenterpro.com",
  "contactPhone": "+1234567890",
  "address": "123 Main Street, City, State 12345"
}
```

### Tenant-Scoped Requests

All tenant-scoped endpoints require the `X-Tenant-Id` header:

```http
GET /api/members
X-Tenant-Id: 3fa85f64-5717-4562-b3fc-2c963f66afa6
Authorization: Bearer <token>
```

## Authentication & Authorization

### Registration

```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "newuser@example.com",
  "password": "SecurePass123!",
  "firstName": "John",
  "lastName": "Doe"
}
```

### Login

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "SecurePass123!"
}
```

**Response:**
```json
{
  "accessToken": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiration": "2026-04-24T14:30:00Z"
  },
  "refreshToken": {
    "token": "refresh_token_here",
    "expiration": "2026-04-26T12:00:00Z"
  }
}
```

### Operation Claims (Permissions)

The system uses fine-grained permissions per feature:

- `Admin` - Full system access
- `{Feature}.Read` - Read-only access
- `{Feature}.Write` - Create and update access
- `{Feature}.Create` - Create access only
- `{Feature}.Update` - Update access only
- `{Feature}.Delete` - Delete access

Example: `Members.Create`, `Subscriptions.Update`, `WorkoutTemplates.Read`

### Assigning Permissions

```http
POST /api/useroperationclaims
Content-Type: application/json

{
  "userId": 1,
  "operationClaimId": 5
}
```

## Database

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

WorkoutTemplate (1) ──── (*) WorkoutTemplateDay
WorkoutTemplateDay (1) ──── (*) WorkoutTemplateDayExercise
WorkoutTemplateDayExercise (*) ──── (0..1) Exercise

DietTemplate (1) ──── (*) DietTemplateDay
DietTemplateDay (1) ──── (*) DietTemplateMeal
DietTemplateMeal (1) ──── (*) DietTemplateMealItem

Staff (1) ──── (*) SupportTicket (created by)
```

### Applying Migrations

When using SQL Server or PostgreSQL:

```bash
# Install EF Core tools
dotnet tool install --global dotnet-ef

# Create migration
dotnet ef migrations add InitialCreate --project src/gymplatformapi/Persistence --startup-project src/gymplatformapi/WebAPI

# Apply migration
dotnet ef database update --project src/gymplatformapi/Persistence --startup-project src/gymplatformapi/WebAPI
```

The application also supports automatic migration on startup via the `UseDbMigrationApplier()` middleware.

### Supported Databases

- **SQL Server** (Production recommended)
- **PostgreSQL** (Production recommended)
- **InMemory** (Development/Testing only)

## Testing

### Running Tests

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test tests/GymPlatformApi.Application.Tests/GymPlatformApi.Application.Tests.csproj
```

### Test Structure

```
tests/GymPlatformApi.Application.Tests/
├── Features/                    # Feature-specific tests
│   ├── Members/
│   ├── Subscriptions/
│   └── ...
└── Mocks/
    ├── Repositories/            # Mock repositories
    └── ...
```

## Deployment

### AWS Elastic Beanstalk

Configuration files are provided in `.ebextensions/`:

```bash
# Deploy using AWS CLI
eb init
eb create gym-platform-api-env
eb deploy
```

Or use the PowerShell script:

```powershell
.\deploy-to-aws.ps1
```

### Azure DevOps

CI/CD pipelines are configured for three environments:

- `.azure/azure-pipelines.development.yml` - Development
- `.azure/azure-pipelines.staging.yml` - Staging
- `.azure/azure-pipelines.production.yml` - Production

### GitHub Actions

Automated workflows in `.github/workflows/`:

- `dotnet.yml` - Build and test on every push
- `release.yml` - Create releases on tag push

### Docker Deployment

```bash
# Build image
docker build -t gym-platform-api:1.0.0 .

# Run with environment variables
docker run -d -p 8080:80 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e ConnectionStrings__SqlServer="Server=db;Database=GymPlatformDb;..." \
  -e TokenOptions__SecurityKey="YourSecureKeyHere" \
  gym-platform-api:1.0.0
```

### Environment Variables

Key environment variables for production:

```bash
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__SqlServer=<connection-string>
TokenOptions__SecurityKey=<secure-key>
TokenOptions__Issuer=<issuer>
TokenOptions__Audience=<audience>
WebAPIConfiguration__AllowedOrigins__0=<origin-url>
CloudinaryAccount__Cloud=<cloudinary-cloud>
CloudinaryAccount__ApiKey=<cloudinary-key>
CloudinaryAccount__ApiSecret=<cloudinary-secret>
```

## License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

Copyright (c) 2022 Ahmet Çetinkaya

## Contributing

Contributions are welcome! Please follow these guidelines:

### Commit Messages

This project follows [Semantic Commit Messages](docs/Semantic%20Commit%20Messages.md) convention:

```
feat: add member profile image upload
fix: resolve subscription expiration calculation bug
docs: update API documentation for workout templates
refactor: optimize database query in attendance logs
test: add unit tests for diet template service
```

### Pull Request Process

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes following semantic commit conventions
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Code Style

- Follow C# coding conventions
- Use CSharpier for code formatting
- Ensure all tests pass before submitting PR
- Add XML documentation comments for public APIs
- Keep business logic in Application layer, not Controllers

### Development Workflow

1. Create a feature branch from `develop`
2. Implement feature following Clean Architecture principles
3. Add unit tests for business logic
4. Add integration tests for API endpoints
5. Update documentation if needed
6. Submit PR to `develop` branch

## Support

For questions, issues, or feature requests:

- **GitHub Issues**: [Create an issue](https://github.com/yourusername/gym-platform-api/issues)
- **Documentation**: Check the [docs](docs/) folder
- **Email**: support@yourdomain.com

---

**Built with nArchitecture framework** - A production-grade Clean Architecture foundation for .NET applications.
