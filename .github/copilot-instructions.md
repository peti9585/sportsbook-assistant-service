# GitHub Copilot Instructions for sportsbook-assistant-service

## Project Overview

The **sportsbook-assistant-service** is an ASP.NET Core REST API service that provides contextual help information to a sportsbook frontend application. The service evolves through two phases:

1. **Phase 1**: Static context-based help article delivery
2. **Phase 2**: Intelligent question answering using LLM and RAG

## Technology Stack

- **Framework**: ASP.NET Core (.NET 10)
- **Language**: C# 13 with nullable reference types enabled
- **API Style**: Minimal APIs
- **Dependencies**: Microsoft.AspNetCore.OpenApi

## Project Structure

```
sportsbook-assistant-service/
├── .github/
│   ├── agents/                    # Copilot agent definitions
│   ├── copilot/                   # Legacy folder (being phased out)
│   └── copilot-instructions.md    # This file
├── docs/
│   ├── architecture/              # Architecture decision records and design docs
│   └── api/                       # API documentation
├── SportsbookAssistantService/    # Main web service project
│   ├── Program.cs                 # Application entry point and API endpoints
│   ├── appsettings.json          # Configuration files
│   └── SportsbookAssistantService.csproj
└── SportsbookAssistantService.sln # Solution file
```

## C# and .NET Coding Standards

### General Guidelines

- Use modern C# features: records, pattern matching, init-only properties, top-level statements
- Enable nullable reference types and handle nullability properly
- Follow async/await patterns for I/O operations
- Use dependency injection for all services
- Implement proper logging using ILogger<T>
- Handle exceptions gracefully with proper error messages

### Naming Conventions

- **PascalCase**: Classes, methods, properties, namespaces
- **camelCase**: Local variables, parameters
- **_camelCase**: Private fields (with underscore prefix)
- **UPPER_CASE**: Constants

### Code Organization

- Keep endpoints in `Program.cs` for simplicity (Minimal API style)
- Create service classes for business logic
- Use separate files for DTOs and domain models
- Implement interfaces for testability and abstraction
- Follow single responsibility principle

### ASP.NET Core Best Practices

- Use the Options pattern for configuration (`IOptions<T>`)
- Implement proper model validation with data annotations
- Use middleware for cross-cutting concerns
- Return appropriate HTTP status codes
- Document APIs with OpenAPI/Swagger
- Use `Results` class for consistent API responses

## Architecture Guidelines

### Layered Architecture

1. **API Layer** (`Program.cs` and endpoints)
   - HTTP request handling
   - Input validation
   - Response formatting
   
2. **Service Layer** (to be created)
   - Business logic
   - Orchestration
   - Context-to-article mapping
   
3. **Repository/Data Layer** (to be created)
   - Content access
   - File system operations
   - Future: Database and LLM integrations

### Design Principles

- **Separation of Concerns**: Keep layers independent
- **Dependency Inversion**: Depend on abstractions (interfaces)
- **Incremental Evolution**: Design Phase 1 to support Phase 2 gracefully
- **Testability**: Write testable code with clear dependencies
- **API Stability**: Maintain backward compatibility in public contracts

## Testing Strategy

- Write unit tests for business logic (services)
- Create integration tests for API endpoints
- Use xUnit as the test framework (if tests are added)
- Mock external dependencies with Moq or NSubstitute
- Follow AAA pattern: Arrange, Act, Assert

## Documentation

- Keep architecture decisions in `docs/architecture/`
- Use Markdown for all documentation
- Update API documentation when endpoints change
- Document complex business logic with inline comments
- Maintain README.md with setup and running instructions

## Phase-Specific Guidance

### Phase 1: Static Content Delivery

- Focus on simple, reliable context-to-article mapping
- Use file system for content storage
- Return articles as JSON with `title` and `content` fields
- Design interfaces that will support Phase 2

### Phase 2: LLM + RAG Integration

- Integrate LLM SDKs (Azure OpenAI, Semantic Kernel)
- Implement vector search for semantic retrieval
- Add caching for performance and cost optimization
- Handle LLM failures gracefully with fallbacks
- Monitor token usage and costs

## Additional Resources

- Architecture docs: `docs/architecture/`
- Specialized agents: `.github/agents/`
  - `dev-assistant.agent.md` - Development guidance
  - `architecture-assistant.agent.md` - Architecture decisions
  - `llm-rag-assistant.agent.md` - LLM/RAG design

## Common Tasks

### Adding a New Endpoint

```csharp
app.MapGet("/endpoint/{param}", (string param) =>
{
    // Implementation
    return Results.Ok(new { data = "value" });
})
.WithName("EndpointName")
.WithOpenApi();
```

### Creating a Service

```csharp
public interface IMyService
{
    Task<Result> DoSomethingAsync(string input);
}

public class MyService : IMyService
{
    private readonly ILogger<MyService> _logger;
    
    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }
    
    public async Task<Result> DoSomethingAsync(string input)
    {
        // Implementation
    }
}
```

### Registering Services

```csharp
// In Program.cs, before building the app
builder.Services.AddScoped<IMyService, MyService>();
```

## Error Handling

- Use `Results.Problem()` for error responses
- Log errors with appropriate log levels
- Return user-friendly error messages
- Include correlation IDs for debugging
- Handle validation errors with 400 Bad Request
- Return 404 Not Found for missing resources
- Use 500 Internal Server Error for unexpected exceptions
