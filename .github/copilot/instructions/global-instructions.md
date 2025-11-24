# Global GitHub Copilot Instructions

These instructions apply to all GitHub Copilot interactions within the Sportsbook Assistant Service repository.

## Core Principles

### 1. Be Concise but Complete
- Provide enough detail to be actionable
- Avoid unnecessary verbosity
- Include essential context and rationale
- Use clear, direct language

### 2. Prefer Small, Incremental Changes
- Suggest minimal changes that achieve the goal
- Break large changes into smaller, focused steps
- Make one logical change at a time
- Ensure each change can be reviewed and tested independently

### 3. Respect Repository Standards
All suggestions and implementations must follow:
- **Coding Standards**: See `docs/guides/coding-standards.md`
  - Follow C#/.NET naming conventions
  - Use nullable reference types properly
  - Apply async/await patterns
  - Include appropriate logging
- **Development Workflow**: See `docs/guides/development-workflow.md`
  - Follow branching strategies
  - Write clear commit messages
  - Create focused pull requests
- **Architecture Documentation**: See `docs/architecture/`
  - Align with documented architecture
  - Consider existing ADRs before suggesting changes
  - Maintain architectural consistency

### 4. Reference Architecture Decision Records (ADRs)
- Check existing ADRs before making architectural suggestions
- If a decision contradicts or extends an ADR, call it out
- Suggest creating a new ADR when introducing significant architectural changes
- Link to relevant ADRs in explanations and code comments

### 5. Call Out Documentation Needs
When suggesting changes, proactively identify if updates are needed to:
- Code comments or XML documentation
- README files
- Architecture documentation
- ADRs (new or updates to existing)
- User guides or operational documentation

## Code Quality Standards

### Security
- Never include hardcoded secrets, API keys, or passwords
- Validate all user inputs
- Use parameterized queries to prevent SQL injection
- Follow principle of least privilege
- Highlight potential security concerns

### Testing
- Suggest appropriate test cases for new code
- Include unit tests for business logic
- Consider edge cases and error scenarios
- Follow the AAA pattern (Arrange, Act, Assert)
- Use meaningful test names: `MethodName_StateUnderTest_ExpectedBehavior`

### Error Handling
- Use specific exception types
- Include meaningful error messages
- Log errors with appropriate context
- Don't swallow exceptions without logging
- Consider graceful degradation for external dependencies

### Performance
- Use async/await for I/O-bound operations
- Suggest caching for frequently accessed data
- Avoid N+1 query problems
- Consider pagination for large data sets
- Profile before optimizing (avoid premature optimization)

## C#/.NET Specific Guidelines

### Language Features
- Use nullable reference types (`<Nullable>enable</Nullable>`)
- Prefer expression-bodied members for simple methods
- Use pattern matching where appropriate
- Leverage LINQ for data transformations
- Use `var` for obvious types, explicit types when clarity is needed

### Framework Conventions
- Use dependency injection for all dependencies
- Follow ASP.NET Core middleware patterns
- Use the Options pattern for configuration
- Implement proper disposal patterns (IDisposable, IAsyncDisposable)
- Use `ILogger<T>` for structured logging

### API Design
- Follow RESTful conventions
- Use appropriate HTTP status codes
- Version APIs appropriately
- Include OpenAPI/Swagger documentation
- Validate inputs at API boundaries

## Interaction Style

### When Providing Explanations
- Start with a brief summary
- Provide details as needed
- Use code examples to illustrate points
- Reference relevant documentation
- Suggest next steps when appropriate

### When Generating Code
- Follow the repository's existing patterns and style
- Include necessary using statements
- Add XML documentation comments for public APIs
- Include TODO comments for incomplete implementations
- Highlight areas that need team review or decision

### When Reviewing Code
- Focus on correctness, maintainability, and architecture alignment
- Be constructive: suggest improvements, don't just criticize
- Prioritize issues: critical bugs, then architecture, then style
- Acknowledge good practices
- Reference specific lines when pointing out issues

## Context Awareness

### Repository Structure
This is a C#/.NET 10.0 ASP.NET Core Web API project with:
- Main project: `SportsbookAssistantService/`
- Solution file: `SportsbookAssistantService.sln`
- Documentation: `docs/`
- Copilot instructions: `.github/copilot/`

### Key Technologies
- .NET 10.0
- ASP.NET Core Web API
- OpenAPI/Swagger
- Structured logging (ILogger)
- Dependency injection

### Domain Context
This service provides intelligent assistance for sportsbook operations, including:
- Integration with sportsbook APIs for odds and events
- Conversational/assistant capabilities
- Data caching and transformation
- Real-time updates and notifications

## Special Considerations

### When Uncertain
- State assumptions clearly
- Offer multiple options when appropriate
- Ask clarifying questions if needed
- Suggest team discussion for significant decisions
- Reference documentation that could help resolve uncertainty

### When Changes Are Architectural
- Explicitly state this is an architectural change
- Suggest creating or updating an ADR
- Discuss trade-offs and alternatives
- Consider impact on existing code
- Recommend review by architects/tech leads

### When External Dependencies Are Involved
- Consider availability and failure scenarios
- Suggest retry logic and circuit breakers
- Include appropriate timeouts
- Propose caching strategies
- Document integration assumptions

## Continuous Improvement

These instructions may evolve as the project grows. When you identify:
- Patterns that should be standardized
- Common issues that need guidance
- Missing or unclear instructions

Suggest updates to these global instructions or the relevant documentation.

## References

- [Repository Context](./repo-context.md)
- [Architecture Documentation](../../../docs/architecture/README.md)
- [Coding Standards](../../../docs/guides/coding-standards.md)
- [Development Workflow](../../../docs/guides/development-workflow.md)
- [Copilot Usage Guide](../../../docs/guides/copilot-usage-guide.md)
