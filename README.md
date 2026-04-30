# NHS Patient Registry API

NHS Patient Registry API retrieves patient summary details by patient ID. It uses in-memory sample data and focuses on clean code, clear structure, validation, error handling, and test coverage.

## Overview

The API allows a user to search for a patient by their ID and returns basic patient details when a matching record is found.

If the patient ID is invalid or the patient does not exist, the API returns a clear error response.

The project keeps the implementation simple because the requirement is to use a mocked or in-memory data source rather than a database.

## Features

- Get patient details by patient ID
- In-memory patient data
- API versioning
- Swagger documentation
- FluentValidation for request validation
- AutoMapper for mapping models to DTOs
- Global exception handling
- Consistent error response format
- Health check endpoint
- Unit tests
- Integration tests

## Technology Stack

- .NET 10
- C#
- ASP.NET Core Web API
- xUnit
- Moq
- FluentAssertions
- FluentValidation
- AutoMapper
- Swagger / OpenAPI

## API Endpoint

### Get patient by ID

```http
GET /api/v1/patients/{id}
```

Example:

```http
GET /api/v1/patients/1
```

## Example Success Response

```json
{
  "id": 1,
  "nhsNumber": "4857773456",
  "name": "John Smith",
  "dateOfBirth": "1985-04-12T00:00:00",
  "gpPractice": "Delapre Medical Centre Northampton"
}
```

## Example Not Found Response

```json
{
  "statusCode": 404,
  "message": "Patient with ID 999 was not found.",
  "detail": "No patient record was found for the supplied ID.",
  "timestamp": "2026-04-30T10:15:30Z"
}
```

## Example Bad Request Response

```json
{
  "statusCode": 400,
  "message": "Patient ID must be a positive integer.",
  "detail": "Please provide a patient ID greater than zero.",
  "timestamp": "2026-04-30T10:15:30Z"
}
```

## Health Check

```http
GET /health
```

Example response:

```json
{
  "status": "Healthy",
  "service": "Nhs.PatientRegistry.Api",
  "timestamp": "2026-04-30T10:15:30Z",
  "version": "1.0.0"
}
```

## Project Structure

```text
Nhs.PatientRegistry.Api
│
├── Abstractions
│   ├── IPatientRepository.cs
│   └── IPatientService.cs
│
├── Controllers
│   └── PatientsController.cs
│
├── DTOs
│   ├── ApiErrorResponse.cs
│   └── PatientDetailsDto.cs
│
├── Extensions
│   ├── ServiceCollectionExtensions.cs
│   └── SwaggerConfigurationExtensions.cs
│
├── Mapping
│   └── PatientMappingProfile.cs
│
├── Middleware
│   └── GlobalExceptionMiddleware.cs
│
├── Models
│   └── Patient.cs
│
├── Repositories
│   └── InMemoryPatientRepository.cs
│
├── Services
│   └── PatientService.cs
│
├── Validation
│   └── PatientIdValidator.cs
│
└── Program.cs
```

## Design Decisions

### In-memory repository

The API uses an in-memory collection because the requirement does not ask for a real database. The in-memory repository could later be replaced with a real database like PostgreSQL, MS SQL.

### Service layer

The business flow is kept in the service layer. The controller handles HTTP requests and responses, while the service handles the patient lookup flow.

### Repository pattern

A repository is used to keep the data access logic separate from the rest of the application.

### DTOs

The API returns a DTO instead of exposing the domain entity model directly. This keeps the API response separate from the internal data model.

### Validation

FluentValidation is used to validate the patient ID. This keeps validation logic separate and easy to unit test unlike Data annotations.

### Error handling

A global exception middleware is also included to handle unexpected errors.

## Testing Approach

The project includes both unit tests and integration tests.

Unit tests are used to check individual parts of the application, such as the service and controller logic.

Integration tests are used to test the real API endpoint through the ASP.NET Core pipeline. This helps confirm that routing, dependency injection, middleware, controller logic, and response formatting work together.

## Assumptions

- A database is not required for this version.
- Authentication, Authorization is not included because it was not part of the requirement.
- The API currently supports looking up patients by patient ID only.

## Future Improvements

- Convert the project into clean architecture structure by creating separate projects for Domain, Application,Infrastructure and UseCase/API and setting the dependencies.
- Add authentication, authorisation, role based access, claims based access using Microsoft Entra ID / Azure AD OAuth 2.0 or ASP.NET Core Identity with JWT tokens so patient records are not accessed anonymously.
- Add Distributed caching using Redis/MemCache to cache frequently fetched patient details.
- Replace the in-memory data source with a secure database.
- Add audit logging to track who accessed patient records and when.
- Add NHS number validation, including format and check digit validation.
- Prepare the API for cloud deployment using Azure App Service or Azure Kubernetes Service.
- Store secrets securely using Azure Key Vault or environment-based configuration.
- Add Application Insights or similar monitoring for logging, errors, and performance tracking.
- Add CI/CD pipeline support using GitHub Actions or Azure DevOps.
