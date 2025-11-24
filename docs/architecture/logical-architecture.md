# Logical Architecture

## Overview

The Sportsbook Assistant Service follows a layered architecture pattern typical of .NET backend services. This document describes the logical components, their responsibilities, and key interaction flows.

## Component Layers

### 1. API / Presentation Layer
- **Technology**: ASP.NET Core Web API
- **Responsibilities**:
  - Expose REST endpoints for external consumers
  - Handle HTTP request/response processing
  - Input validation and output formatting
  - Authentication and authorization (when implemented)
  - OpenAPI/Swagger documentation

### 2. Conversation Orchestrator
- **Purpose**: Coordinate complex workflows and multi-step operations
- **Responsibilities**:
  - Manage conversational state for assistant interactions
  - Orchestrate calls to domain logic and integration layers
  - Handle error recovery and retry logic
  - Aggregate results from multiple data sources
- **Note**: This component is a placeholder for future conversational/assistant capabilities

### 3. Domain / Sportsbook Logic Layer
- **Purpose**: Implement core business logic for sportsbook operations
- **Responsibilities**:
  - Process and transform sportsbook data
  - Apply business rules and validation
  - Calculate derived metrics (e.g., arbitrage opportunities, value bets)
  - Maintain domain models and entities
- **Note**: Specific domain logic to be developed based on business requirements

### 4. Integration Layer
- **Purpose**: Abstract external system interactions
- **Responsibilities**:
  - **GitHub Integration**: Interact with GitHub APIs for repository operations
  - **CI/CD Integration**: Trigger builds, monitor deployment status
  - **Sportsbook API Integration**: Fetch odds, events, and betting information from upstream providers
  - Handle API rate limiting and error handling
  - Transform external data models to internal domain models

### 5. Persistence / Caching Layer
- **Purpose**: Manage data storage and retrieval
- **Responsibilities**:
  - Cache frequently accessed sportsbook data to reduce API calls
  - Store application state and configuration
  - Provide fast data access for performance-critical operations
- **Technologies**: 
  - Placeholder for database (e.g., SQL Server, PostgreSQL)
  - Placeholder for caching (e.g., Redis, in-memory cache)
- **Note**: Specific persistence strategy to be determined based on scaling requirements

## Main Data Flow

### Flow 1: Fetch Sportsbook Data

1. **External Consumer** sends a request to the API Layer (e.g., `GET /api/odds/nfl`)
2. **API Layer** validates the request and routes to the appropriate controller
3. **Conversation Orchestrator** (if needed) coordinates the operation
4. **Domain Logic Layer** determines what data is needed
5. **Persistence/Cache Layer** checks if data is cached and fresh
6. If cache miss or stale data:
   - **Integration Layer** fetches data from Sportsbook APIs
   - **Domain Logic Layer** processes and validates the data
   - **Persistence/Cache Layer** stores the result for future requests
7. **API Layer** formats the response and returns to the consumer

### Flow 2: Architecture Discussion Session (Future)

1. **Developer/Architect** initiates an architecture session via a Copilot agent
2. **Conversation Orchestrator** manages the session state and context
3. **Integration Layer** fetches relevant ADRs and documentation from GitHub
4. **Domain Logic Layer** applies architectural patterns and constraints
5. The agent provides recommendations, generates ADR drafts, or updates documentation
6. **Integration Layer** can commit changes back to GitHub when approved

## Cross-Cutting Concerns

### Logging
- Structured logging using `ILogger<T>` throughout all layers
- Log correlation IDs for request tracing
- Different log levels for different environments

### Error Handling
- Global exception handling middleware at the API layer
- Domain-specific exceptions with meaningful error codes
- Graceful degradation when external systems are unavailable

### Configuration
- Environment-specific configuration via `appsettings.{Environment}.json`
- Sensitive configuration via environment variables or secret management
- Feature flags for gradual rollout of new capabilities

### Health Checks
- ASP.NET Core health check endpoints
- Dependency health checks (databases, external APIs)
- Readiness and liveness probes for orchestration platforms

## Technology Stack

- **Framework**: .NET 10.0 (ASP.NET Core)
- **Language**: C# with nullable reference types enabled
- **API Documentation**: OpenAPI/Swagger
- **Dependency Injection**: Built-in ASP.NET Core DI container
- **Async/Await**: Asynchronous programming throughout

## Future Considerations

- **Event-Driven Architecture**: Consider adding message queues for asynchronous processing
- **Microservices**: Potential to split into smaller services as complexity grows
- **CQRS Pattern**: Separate read and write models for complex query scenarios
- **GraphQL**: Alternative API style for flexible client queries

## Notes

This logical architecture is a starting point and will evolve as the service is developed. Key architectural decisions will be captured in ADRs under the [adr](./adr/) directory.
