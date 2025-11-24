# Architecture Overview

## Purpose

The **sportsbook-assistant-service** is a REST-based web service designed to provide contextual help information to the sportsbook frontend. The service acts as a bridge between user context and relevant help content, initially serving pre-generated articles and eventually supporting free-form question answering through LLM integration.

## High-Level API (Phase 1)

The initial API design is straightforward:

- **Endpoint**: `GET /assistant/{context_info}`
- **Request**: The `{context_info}` path parameter identifies the user's current location or context within the sportsbook frontend (e.g., `bet-slip/empty`, `live-betting/soccer`, `account/deposit`).
- **Response**: A JSON array of help articles relevant to the given context.

Example response:
```json
[
  {
    "title": "How to Place a Bet",
    "content": "<p>To place a bet...</p>"
  },
  {
    "title": "Understanding Bet Types",
    "content": "<p>There are several types of bets...</p>"
  }
]
```

## Phases

### Phase 1: Static Context Mapping

In the initial phase, the service implements a straightforward mapping approach:

1. Receive a context identifier from the frontend (e.g., `bet-slip/empty`)
2. Map the context to one or more pre-generated HTML help files
3. Load the mapped content and return it as article objects
4. Each context has a predefined set of articles with associated titles

This phase focuses on:
- Simple, reliable delivery of curated help content
- Fast response times through direct file mapping
- Establishing the article contract and API interface
- Building the foundation for future enhancements

### Phase 2: LLM + RAG Integration

In the second phase, the service evolves to support intelligent question answering:

1. Accept optional free-form user questions alongside context information
2. Use Retrieval-Augmented Generation (RAG) to find relevant help content
3. Generate contextually appropriate answers using an LLM
4. Return answers formatted as articles, maintaining API compatibility

This phase adds:
- Natural language understanding of user questions
- Semantic search over the help content corpus
- Dynamic answer generation while staying grounded in curated content
- Enhanced user experience through conversational assistance

## Logical Components

The service architecture consists of the following logical layers:

### API Layer
- **Responsibility**: HTTP request handling, routing, validation, and response formatting
- **Technology**: ASP.NET Core Minimal APIs
- **Key Concerns**: Input validation, error handling, API versioning

### Assistant Service Layer
- **Responsibility**: Business logic for determining which articles to return
- **Phase 1**: Context-to-article mapping logic
- **Phase 2**: Question interpretation, RAG orchestration, answer formatting
- **Key Concerns**: Encapsulation of logic, testability, extensibility

### Content Provider Layer
- **Responsibility**: Access to help content (HTML files, metadata)
- **Phase 1**: File system access with context-based lookups
- **Phase 2**: Integration with indexing and retrieval infrastructure
- **Key Concerns**: Abstraction over content sources, caching, performance

### RAG/LLM Engine (Phase 2 only)
- **Responsibility**: Semantic search, embedding generation, LLM interaction
- **Key Concerns**: Latency, cost management, safety, observability
- **Integration Points**: Content provider for corpus, Assistant service for orchestration

## Design Principles

- **Incremental Evolution**: Phase 1 design should facilitate Phase 2 additions without major refactoring
- **Stable Contracts**: The article format and REST API should remain backward-compatible
- **Clear Layering**: Separation of concerns between API, business logic, and data access
- **Testability**: Each component should be independently testable
- **Performance**: Fast response times for both static and dynamic content retrieval
