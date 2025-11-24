# Sportsbook Assistant Service - Architecture Documentation

## Goals

The Sportsbook Assistant Service aims to provide:

- **Intelligent Sportsbook Assistance**: Help developers and operations teams interact with sportsbook data, odds, and events through a conversational interface.
- **Extensibility**: Support integration with multiple sportsbook APIs and downstream consumers.
- **Maintainability**: Maintain clear architectural boundaries and well-documented design decisions.
- **Observability**: Provide comprehensive logging, monitoring, and health check capabilities.

## Scope

This architecture documentation covers:

- **System Context**: How the Sportsbook Assistant Service interacts with external systems and stakeholders.
- **Logical Architecture**: The internal components, layers, and their responsibilities.
- **Deployment Architecture**: How the service is deployed across different environments.
- **Architecture Decision Records (ADRs)**: Key architectural decisions and their rationale.

The documentation is tailored to a **C#/.NET backend service** deployed as an ASP.NET Core Web API.

## Structure

This architecture documentation is organized as follows:

- **[Context Diagram](./context-diagram.md)**: System context view showing external actors and systems.
- **[Logical Architecture](./logical-architecture.md)**: Internal component structure and data flows.
- **[Deployment Architecture](./deployment-architecture.md)**: Deployment topology and operational considerations.
- **[Architecture Decision Records (ADRs)](./adr/README.md)**: Catalog of architectural decisions.

## Contributing to Architecture Documentation

When proposing architectural changes:

1. Review existing ADRs to understand current decisions.
2. Use the Architecture Agent to draft new ADRs or discuss options.
3. Update relevant architecture diagrams and documentation.
4. Link ADRs in pull requests that implement architectural decisions.

For guidance on using GitHub Copilot agents for architecture discussions, see the [Copilot Usage Guide](../guides/copilot-usage-guide.md).
