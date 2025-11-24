# Coding Standards

## Overview

This document defines coding standards for the Sportsbook Assistant Service. Consistent coding practices improve readability, maintainability, and collaboration across the team.

## General Principles

### 1. Clarity Over Cleverness
Write code that is easy to understand. Prefer straightforward solutions over clever tricks.

**Good**:
```csharp
var isEligibleForBonus = user.IsActive && user.BetCount > 10;
```

**Avoid**:
```csharp
var isEligibleForBonus = user.IsActive & user.BetCount > 10; // Bitwise AND is unclear here
```

### 2. DRY (Don't Repeat Yourself)
Avoid duplication. Extract common logic into reusable methods or classes.

### 3. YAGNI (You Aren't Gonna Need It)
Don't build features or abstractions until they are actually needed.

### 4. SOLID Principles
- **Single Responsibility**: Each class should have one reason to change
- **Open/Closed**: Open for extension, closed for modification
- **Liskov Substitution**: Subtypes should be substitutable for their base types
- **Interface Segregation**: Many specific interfaces are better than one general interface
- **Dependency Inversion**: Depend on abstractions, not concretions

## C# / .NET Specific Standards

### Naming Conventions

Follow Microsoft's [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions):

| Type | Naming | Example |
|------|--------|---------|
| Namespace | PascalCase | `SportsbookAssistantService.Domain` |
| Class | PascalCase | `OddsService` |
| Interface | PascalCase with `I` prefix | `IOddsProvider` |
| Method | PascalCase | `GetOddsAsync` |
| Property | PascalCase | `EventName` |
| Field (private) | camelCase with `_` prefix | `_oddsCache` |
| Field (const) | PascalCase | `MaxRetryAttempts` |
| Parameter | camelCase | `userId` |
| Local Variable | camelCase | `oddsData` |

### Code Organization

**File Structure**:
```
├── Controllers/       # API controllers
├── Services/         # Business logic services
├── Models/           # Domain models and DTOs
├── Repositories/     # Data access layer
├── Middleware/       # Custom middleware
├── Extensions/       # Extension methods
├── Configuration/    # Configuration classes
└── Infrastructure/   # External integrations
```

**Class Structure** (top to bottom):
1. Fields (constants, static, instance)
2. Constructors
3. Properties
4. Public methods
5. Protected methods
6. Private methods
7. Nested types

### Nullability

The project has nullable reference types enabled (`<Nullable>enable</Nullable>`).

**Guidelines**:
- Use `?` for nullable reference types explicitly
- Avoid `!` null-forgiving operator unless absolutely necessary
- Check for null before dereferencing

**Example**:
```csharp
public class OddsService
{
    private readonly IOddsProvider _provider;
    
    public OddsService(IOddsProvider provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }
    
    public async Task<OddsData?> GetOddsAsync(string eventId)
    {
        if (string.IsNullOrWhiteSpace(eventId))
        {
            throw new ArgumentException("Event ID cannot be null or empty.", nameof(eventId));
        }
        
        return await _provider.FetchOddsAsync(eventId);
    }
}
```

### Async/Await

Use async/await for all I/O-bound operations.

**Best Practices**:
- Methods returning `Task` should be named with `Async` suffix
- Always use `await` instead of `.Result` or `.Wait()`
- Use `ConfigureAwait(false)` in library code (not required in ASP.NET Core)
- Return `Task<T>` for async methods with a return value
- Return `Task` for async methods without a return value

**Example**:
```csharp
public async Task<List<Odds>> GetAllOddsAsync(CancellationToken cancellationToken = default)
{
    var odds = await _repository.GetOddsAsync(cancellationToken);
    return odds.ToList();
}
```

### Dependency Injection

Use constructor injection for dependencies.

**Example**:
```csharp
public class OddsController : ControllerBase
{
    private readonly IOddsService _oddsService;
    private readonly ILogger<OddsController> _logger;
    
    public OddsController(IOddsService oddsService, ILogger<OddsController> logger)
    {
        _oddsService = oddsService ?? throw new ArgumentNullException(nameof(oddsService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
}
```

**Registration** (in `Program.cs`):
```csharp
builder.Services.AddScoped<IOddsService, OddsService>();
builder.Services.AddSingleton<IOddsCache, RedisOddsCache>();
builder.Services.AddTransient<IOddsValidator, OddsValidator>();
```

### Logging

Use structured logging with `ILogger<T>`.

**Guidelines**:
- Log significant events and errors
- Use appropriate log levels (Trace, Debug, Information, Warning, Error, Critical)
- Include correlation IDs for request tracing
- Use log message templates, not string interpolation

**Example**:
```csharp
_logger.LogInformation("Fetching odds for event {EventId}", eventId);

try
{
    var odds = await _provider.FetchOddsAsync(eventId);
    _logger.LogDebug("Retrieved {OddsCount} odds for event {EventId}", odds.Count, eventId);
    return odds;
}
catch (Exception ex)
{
    _logger.LogError(ex, "Failed to fetch odds for event {EventId}", eventId);
    throw;
}
```

### Exception Handling

**Best Practices**:
- Catch specific exceptions, not generic `Exception` (unless re-throwing)
- Use exception filters to avoid catching exceptions you can't handle
- Don't swallow exceptions without logging
- Use custom exceptions for domain-specific errors

**Example**:
```csharp
public class OddsNotFoundException : Exception
{
    public string EventId { get; }
    
    public OddsNotFoundException(string eventId)
        : base($"Odds not found for event: {eventId}")
    {
        EventId = eventId;
    }
}

// Usage
if (odds == null)
{
    throw new OddsNotFoundException(eventId);
}
```

### Configuration

Use the Options pattern for strongly-typed configuration.

**Example**:
```csharp
// Configuration class
public class SportsbookApiOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 30;
}

// Registration in Program.cs
builder.Services.Configure<SportsbookApiOptions>(
    builder.Configuration.GetSection("SportsbookApi"));

// Usage via DI
public class OddsProvider : IOddsProvider
{
    private readonly SportsbookApiOptions _options;
    
    public OddsProvider(IOptions<SportsbookApiOptions> options)
    {
        _options = options.Value;
    }
}
```

### Code Comments

**When to comment**:
- Complex algorithms requiring explanation
- Non-obvious business rules
- Public APIs (use XML documentation comments)
- Workarounds for external library issues

**When NOT to comment**:
- Obvious code (the code should be self-explanatory)
- Explaining what the code does (use better naming instead)
- Commented-out code (delete it; it's in Git history)

**Example**:
```csharp
/// <summary>
/// Calculates the implied probability from American odds.
/// </summary>
/// <param name="americanOdds">The odds in American format (e.g., -110, +150).</param>
/// <returns>The implied probability as a decimal (0.0 to 1.0).</returns>
public decimal CalculateImpliedProbability(int americanOdds)
{
    // American odds formula:
    // Negative odds: probability = |odds| / (|odds| + 100)
    // Positive odds: probability = 100 / (odds + 100)
    
    if (americanOdds < 0)
    {
        return Math.Abs(americanOdds) / (Math.Abs(americanOdds) + 100m);
    }
    else
    {
        return 100m / (americanOdds + 100m);
    }
}
```

## Testing Guidelines

### Test Framework

Use **xUnit** for testing (can be adapted to NUnit or MSTest if already in use).

### Test Naming Convention

```
MethodName_StateUnderTest_ExpectedBehavior
```

**Examples**:
- `GetOddsAsync_WithValidEventId_ReturnsOdds`
- `GetOddsAsync_WithInvalidEventId_ThrowsArgumentException`
- `CalculateImpliedProbability_WithNegativeOdds_ReturnsCorrectValue`

### Test Structure (AAA Pattern)

```csharp
[Fact]
public async Task GetOddsAsync_WithValidEventId_ReturnsOdds()
{
    // Arrange
    var eventId = "NFL_2024_001";
    var mockProvider = new Mock<IOddsProvider>();
    mockProvider
        .Setup(p => p.FetchOddsAsync(eventId))
        .ReturnsAsync(new OddsData { EventId = eventId });
    var service = new OddsService(mockProvider.Object);
    
    // Act
    var result = await service.GetOddsAsync(eventId);
    
    // Assert
    Assert.NotNull(result);
    Assert.Equal(eventId, result.EventId);
}
```

### Test Coverage

Aim for:
- **Unit Tests**: 70-80% code coverage
- **Integration Tests**: Critical paths and external integrations
- **End-to-End Tests**: Key user scenarios (minimal, due to cost)

**What to test**:
- Business logic (always)
- Edge cases and error conditions
- Public APIs
- Complex algorithms

**What NOT to test**:
- Simple getters/setters
- Framework code
- Third-party libraries

### Mocking

Use **Moq** for creating test doubles.

**Example**:
```csharp
var mockLogger = new Mock<ILogger<OddsService>>();
var mockCache = new Mock<IOddsCache>();
mockCache
    .Setup(c => c.GetAsync(It.IsAny<string>()))
    .ReturnsAsync((OddsData?)null);

var service = new OddsService(mockCache.Object, mockLogger.Object);
```

### Integration Tests

For testing with real dependencies (database, external APIs):

```csharp
public class OddsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    
    public OddsIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task GetOdds_ReturnsSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/api/odds/NFL_2024_001");
        
        // Assert
        response.EnsureSuccessStatusCode();
    }
}
```

## Code Formatting

### EditorConfig

The repository should include an `.editorconfig` file to enforce consistent formatting across IDEs.

**Key settings**:
- Indentation: 4 spaces
- End of line: LF (Unix-style)
- Trim trailing whitespace
- Insert final newline

### Using `dotnet format`

Run before committing:
```bash
dotnet format
```

### IDE Settings

Configure your IDE (Visual Studio, Rider, VS Code) to:
- Use the .editorconfig settings
- Format on save
- Organize usings on save

## Security Best Practices

- **Never commit secrets**: Use environment variables or secret management tools
- **Validate all inputs**: Sanitize and validate user input at API boundaries
- **Use parameterized queries**: Prevent SQL injection
- **Encrypt sensitive data**: Use HTTPS, encrypt data at rest
- **Keep dependencies updated**: Regularly update NuGet packages for security patches

## Performance Considerations

- **Use async/await** for I/O-bound operations
- **Cache frequently accessed data** to reduce database/API calls
- **Use efficient data structures**: Choose appropriate collections (List, Dictionary, HashSet, etc.)
- **Avoid premature optimization**: Profile before optimizing
- **Consider pagination** for large data sets

## References

- [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [.NET Framework Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/)
- [Clean Code by Robert C. Martin](https://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882)
- [xUnit Documentation](https://xunit.net/)
