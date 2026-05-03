backend is built using ASP.NET Core Web API with Entity Framework Core. Core folders are separated into Controllers, DTOs, Models, and Data for maintainability and readability.

The domain model contains WorkRequest and WorkRequestNote entities. Enums are used for Priority and Status to avoid magic strings and ensure validation consistency. DTOs are introduced to decouple API contracts from database entities and simplify validation.

The API supports pagination, filtering by status, and searching by title/client name. Validation is handled through DataAnnotations plus enum parsing checks. Common error responses follow a consistent JSON structure to simplify frontend error handling.

For persistence, SQL Server or SQLite can be used. SQL schema includes indexes on Status and Title/ClientName to improve filtering and search performance.

The frontend is built with Next.js and React hooks. A single page displays work requests with filter and search controls. API calls are triggered when filter/search changes. Loading states and basic error states are included. A lightweight form supports creation, while inline actions support status updates.

Because of the 2-hour limit, several trade-offs were made:

Minimal styling
No authentication/authorization
No advanced form validation library
No caching or optimistic UI updates
No unit/integration tests

In production, I would improve:

Add authentication and role-based permissions
Introduce service/repository layers
Add FluentValidation
Add Swagger/OpenAPI documentation
Add audit history for status changes
Add unit and integration tests
Use debounce for search
Add pagination UI

In a demo, I would first show:

Create work request
Filter/search requests
Update status
Add note

Main assumptions:

Status transitions are unrestricted
Notes are append-only
Single-user environment

Main risks:

Search uses simple contains query
No concurrency handling
No soft delete/archive logic
