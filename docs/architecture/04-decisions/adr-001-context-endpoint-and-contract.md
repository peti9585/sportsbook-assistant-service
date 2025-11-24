# ADR-001: Context-Based Endpoint and Article Contract

## Status

Accepted

## Date

2025-11-24

## Context

The sportsbook assistant service needs to provide contextual help information to the frontend application. The service must:

1. Allow the frontend to request help content based on the user's current location/state in the application
2. Return structured help content that can be easily consumed and rendered by the frontend
3. Support future enhancements like free-form question answering via LLM/RAG
4. Be simple to implement and maintain in the initial phase
5. Provide a stable contract that won't require major frontend changes as the backend evolves

Key considerations:
- The frontend needs to pass context information efficiently
- The response format should be flexible enough for future enhancements
- The API should be RESTful and follow common conventions
- Performance should be good (fast response times)

## Decision

We will implement a REST API with the following design:

**Endpoint**: `GET /assistant/{context_info}`

**Path Parameter**:
- `context_info`: A hierarchical identifier representing the user's current context (e.g., `bet-slip/empty`, `account/deposit`)

**Query Parameters** (for future Phase 2):
- `q` (optional): Free-form user question for LLM/RAG-based answering

**Response**: JSON array of article objects

**Article Schema**:
```json
{
  "title": "string",
  "content": "string"
}
```

Where:
- `title`: Plain text title for the article
- `content`: HTML-formatted help content

**Example Request**:
```
GET /assistant/bet-slip/empty
```

**Example Response**:
```json
[
  {
    "title": "How to Place a Bet",
    "content": "<p>To place a bet, select your desired outcome by clicking on the odds...</p>"
  },
  {
    "title": "Understanding Bet Types",
    "content": "<p>There are several types of bets available...</p>"
  }
]
```

## Consequences

### Positive

- **Simple Integration**: Frontend can make straightforward GET requests without complex payloads
- **RESTful Design**: Uses path parameters for resource identification, following REST conventions
- **Cacheable**: GET requests can be cached by browsers and CDNs for better performance
- **Flexible Response**: Returning an array allows multiple articles per context
- **Minimal Article Model**: Simple schema is easy to consume and extend
- **Future-Ready**: Can add query parameters (like `q`) without breaking existing clients
- **Human-Readable URLs**: Context identifiers in the path make URLs understandable and debuggable

### Negative

- **Path Parameter Limitations**: Special characters in context identifiers need URL encoding
- **Limited Metadata**: Initial article model is minimal; extensions will need to be optional to maintain compatibility
- **No Built-in Versioning**: API versioning would need to be added via path prefix or headers if needed

### Neutral

- **Context Hierarchy**: Using `/` in context identifiers is intuitive but requires consistent conventions
- **Array Response**: Always returning an array (even for single articles) provides consistency but slightly increases payload size

## Alternatives Considered

### Alternative 1: POST with JSON Context

**Description**: Use `POST /assistant` with a JSON request body containing structured context information

```json
{
  "area": "bet-slip",
  "state": "empty",
  "metadata": { ... }
}
```

**Pros**:
- More structured context representation
- Can include additional metadata easily
- No URL encoding concerns

**Cons**:
- Not RESTful for a read operation
- Cannot be cached by standard HTTP caching
- More complex for frontend to implement
- Overhead of JSON parsing for simple requests

**Why not chosen**: The benefits of GET requests (caching, simplicity) outweigh the structured context advantages. The hierarchical string format (`area/state`) provides sufficient structure for Phase 1.

### Alternative 2: Query Parameter-Only Design

**Description**: Use `GET /assistant?context=bet-slip/empty`

**Pros**:
- Flexible query string approach
- Easy to add additional parameters

**Cons**:
- Less RESTful (context is the primary resource identifier)
- URLs less readable
- Doesn't follow REST conventions for resource identification

**Why not chosen**: Using path parameters for the primary resource identifier (`context_info`) is more RESTful and creates cleaner, more semantic URLs.

### Alternative 3: Rich Article Model with IDs and Metadata

**Description**: Include additional fields in the article model from the start:

```json
{
  "id": "article-123",
  "title": "...",
  "content": "...",
  "category": "betting",
  "tags": ["bet-slip", "basics"],
  "lastUpdated": "2025-11-24T12:00:00Z"
}
```

**Pros**:
- More metadata available immediately
- Better for analytics and tracking
- Supports richer frontend features

**Cons**:
- More complex to implement initially
- Overhead in response payload
- May include fields not needed by all clients
- Harder to change later if requirements evolve

**Why not chosen**: We prefer to start with a minimal, focused model. The article schema can be extended later with optional fields while maintaining backward compatibility. This follows the principle of starting simple and evolving based on real needs.

## References

- Architecture Overview: `docs/architecture/01-architecture-overview.md`
- Context and Articles Design: `docs/architecture/02-context-and-articles.md`
- OpenAPI Specification: `docs/api/openapi.yaml`
