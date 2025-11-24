# Repository Context

## Repository Overview

**Name**: sportsbook-assistant-service  
**Purpose**: Intelligent assistant service for sportsbook operations  
**Technology Stack**: C#/.NET 10.0 ASP.NET Core Web API  
**Primary Language**: C# with nullable reference types enabled

## What This Service Does

The Sportsbook Assistant Service provides:

1. **Sportsbook Data Integration**: Fetches and processes odds, events, and betting information from upstream sportsbook APIs
2. **Intelligent Assistance**: Conversational/assistant capabilities for developers and operations teams
3. **Data Transformation**: Converts external sportsbook data into internal domain models
4. **Caching & Performance**: Reduces external API calls through intelligent caching
5. **API Endpoints**: Exposes REST APIs for downstream consumers

## Repository Structure

```
sportsbook-assistant-service/
├── .github/
│   └── copilot/
│       ├── agents/              # Custom Copilot agent instructions
│       │   ├── architecture-agent.md
│       │   ├── backend-dev-agent.md
│       │   └── reviewer-agent.md
│       └── instructions/        # Global Copilot instructions
│           ├── global-instructions.md
│           ├── repo-context.md  (this file)
│           ├── architecture-discussion-template.md
│           └── pr-review-template.md
├── docs/
│   ├── architecture/            # Architecture documentation
│   │   ├── README.md           # Architecture overview
│   │   ├── context-diagram.md  # System context view
│   │   ├── logical-architecture.md
│   │   ├── deployment-architecture.md
│   │   └── adr/                # Architecture Decision Records
│   │       ├── README.md
│   │       └── 0001-record-architecture-decisions.md
│   └── guides/                 # Development guides
│       ├── coding-standards.md
│       ├── development-workflow.md
│       └── copilot-usage-guide.md
├── SportsbookAssistantService/ # Main application project
│   ├── Program.cs              # Application entry point
│   ├── appsettings.json        # Configuration
│   ├── appsettings.Development.json
│   └── SportsbookAssistantService.csproj
└── SportsbookAssistantService.sln
```

## Key Directories

### `/docs/architecture/`
Contains all architecture documentation including:
- High-level architecture overview
- System context diagrams
- Logical and deployment architectures
- **ADRs** (Architecture Decision Records) in `/docs/architecture/adr/`

**When to reference**: 
- Planning new features or significant changes
- Understanding system design decisions
- Evaluating architectural options

### `/docs/guides/`
Developer guides covering:
- Coding standards and conventions
- Development workflow (branching, commits, PRs)
- How to use GitHub Copilot effectively

**When to reference**:
- Writing or reviewing code
- Setting up development environment
- Contributing to the repository

### `/SportsbookAssistantService/`
The main ASP.NET Core Web API project containing:
- Controllers (API endpoints)
- Services (business logic)
- Models (domain entities, DTOs)
- Repositories (data access)
- Middleware (custom request processing)
- Configuration classes

**When to reference**:
- Implementing new features
- Understanding existing code patterns
- Maintaining consistency with current structure

### `/.github/copilot/`
GitHub Copilot configuration including:
- **agents/**: Custom agent instructions (Architecture, Backend Dev, Reviewer)
- **instructions/**: Global instructions and prompt templates

**When to reference**:
- Using custom Copilot agents
- Understanding expected behavior from agents
- Contributing to agent improvements

## Technology Stack Details

### Framework & Language
- **.NET 10.0**: Latest .NET version with performance improvements
- **C# 13**: Modern C# with nullable reference types enabled
- **ASP.NET Core**: Web API framework with built-in DI, middleware, and configuration

### Key Packages
- **Microsoft.AspNetCore.OpenApi**: OpenAPI/Swagger support for API documentation

### Development Tools
- **dotnet CLI**: Build, test, and run commands
- **Git**: Version control
- **GitHub**: Source code hosting and collaboration

## Architectural Patterns

The service follows a **layered architecture**:

1. **API/Presentation Layer**: Controllers, request/response handling
2. **Orchestration Layer**: Conversation orchestrator for complex workflows
3. **Domain/Business Logic Layer**: Core sportsbook logic and rules
4. **Integration Layer**: External API clients (GitHub, CI/CD, Sportsbook APIs)
5. **Persistence/Caching Layer**: Data storage and caching

See [Logical Architecture](../../../docs/architecture/logical-architecture.md) for details.

## Coding Conventions Summary

- **Naming**: PascalCase for types/methods/properties, camelCase with `_` prefix for private fields
- **Nullability**: Nullable reference types enabled, explicit `?` for nullable types
- **Async/Await**: All I/O operations should be async with `Async` suffix
- **Dependency Injection**: Constructor injection for all dependencies
- **Logging**: Structured logging with `ILogger<T>`
- **Error Handling**: Specific exception types, global exception middleware

Full details in [Coding Standards](../../../docs/guides/coding-standards.md).

## ADR Guidelines

**Architecture Decision Records (ADRs)** document significant architectural decisions.

- **Location**: `/docs/architecture/adr/`
- **Naming**: `NNNN-short-title-with-dashes.md` (e.g., `0001-record-architecture-decisions.md`)
- **When to create**: Technology selection, architectural patterns, integration approaches, security practices

See [ADR README](../../../docs/architecture/adr/README.md) for the full process.

## Development Workflow

- **Main Branch**: `main` (protected, requires PR review)
- **Feature Branches**: `feature/<description>`
- **Bugfix Branches**: `bugfix/<description>`
- **Commit Messages**: Conventional format: `<type>(<scope>): <subject>`
- **PR Size**: Keep PRs small (50-200 lines ideal, max 400-500)

See [Development Workflow](../../../docs/guides/development-workflow.md) for details.

## External Integrations

The service integrates with:

1. **Sportsbook APIs** (upstream): Fetch odds, events, betting information
   - Integration approach to be defined in ADRs
   - Consider rate limiting, caching, error handling
   
2. **GitHub**: Source code management, issue tracking
   - PRs require approval before merge
   - CI/CD integration (to be configured)

3. **CI/CD Pipeline** (to be defined): Automated build, test, deploy
   - Specific tooling to be documented in ADRs

4. **Downstream Consumers**: Services or applications consuming our APIs
   - REST endpoints with OpenAPI documentation

## Common Development Scenarios

### Implementing a New API Endpoint
1. Review existing controller patterns
2. Create controller and action method
3. Implement service layer logic
4. Add appropriate error handling and logging
5. Update OpenAPI documentation
6. Write unit and integration tests

### Adding External API Integration
1. Check for existing ADRs on integration patterns
2. Create interface for the integration (`I*Provider`)
3. Implement the interface with proper error handling
4. Add configuration for API credentials (Options pattern)
5. Register with DI container
6. Consider creating an ADR if architectural decision

### Creating an ADR
1. Identify the architectural decision to document
2. Use Architecture Agent to draft the ADR (optional but recommended)
3. Follow ADR template structure
4. Submit via PR for review
5. Update ADR index in `/docs/architecture/adr/README.md`

## Agent Usage Context

### When to use Architecture Agent
- Planning features with architectural impact
- Creating or reviewing ADRs
- Evaluating design alternatives
- Ensuring consistency with existing architecture

### When to use Backend Dev Agent
- Implementing API endpoints, services, or repositories
- Refactoring existing C#/.NET code
- Writing or updating tests
- Troubleshooting backend issues

### When to use Reviewer Agent
- Before requesting human code review
- Checking alignment with coding standards and architecture
- Validating test coverage
- Learning what makes good code

See [Copilot Usage Guide](../../../docs/guides/copilot-usage-guide.md) for detailed examples.

## Important Constraints

- **No hardcoded secrets**: Use environment variables or secret management
- **Nullable reference types**: Must handle nullability properly
- **Async/await**: All I/O operations must be async
- **Error handling**: Don't swallow exceptions, log appropriately
- **Testing**: Business logic must have unit tests

## Getting Help

- **Documentation**: Start with `/docs/` directory
- **Architecture Questions**: Use Architecture Agent or ask architects
- **Code Questions**: Use Backend Dev Agent or check existing patterns
- **Unclear Requirements**: Ask in team discussions or issues

## Continuous Evolution

This repository and its context will evolve. When you notice:
- New patterns emerging
- Documentation gaps
- Inconsistencies

Suggest updates via PR or team discussion.

## Quick Reference Links

- [Architecture README](../../../docs/architecture/README.md)
- [ADR Index](../../../docs/architecture/adr/README.md)
- [Coding Standards](../../../docs/guides/coding-standards.md)
- [Development Workflow](../../../docs/guides/development-workflow.md)
- [Copilot Usage Guide](../../../docs/guides/copilot-usage-guide.md)
