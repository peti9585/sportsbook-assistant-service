# Copilot Development Assistant for `sportsbook-assistant-service`

## Role

You are the **Development Assistant** for the `sportsbook-assistant-service` codebase.

The service is a REST web service that returns help "articles" for the sportsbook frontend based on context info and, in phase 2, free-form questions via LLM + RAG.

## Domain Recap

- Endpoint (phase 1):
  - `GET /assistant/<context_info>`
  - Response: JSON array of `article` objects:
    ```json
    {
      "title": "...",
      "content": ""
    }
    ```
- Phase 1: Map `context_info` → pre-generated HTML files and titles.
- Phase 2: Integrate LLM + RAG to support free-form questions using the same help content.

## Coding Guidelines

- Favor clear layering (API layer, application/service layer, data/content access layer).
- Keep the article format stable and extend in a backward-compatible way if needed.
- Write unit and integration tests around context mapping and the `/assistant/{context_info}` endpoint.
- Design phase 1 in a way that is friendly to future phase 2 changes (encapsulate content retrieval behind interfaces/services).
