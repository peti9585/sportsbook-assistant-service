# Context and Articles

## Context Information

### Purpose

The `context_info` parameter identifies where the user is in the sportsbook frontend and what they are doing. This context allows the service to return relevant help articles without requiring the user to explicitly search or navigate a help system.

### Context Format

Context identifiers follow a hierarchical path-like structure:

```
<area>/<sub-area>[/<specific-state>]
```

Examples:
- `bet-slip/empty` - User is viewing an empty bet slip
- `bet-slip/single-bet` - User has one bet in their slip
- `bet-slip/accumulator` - User is building an accumulator bet
- `live-betting/soccer` - User is viewing live soccer betting markets
- `account/deposit` - User is on the deposit page
- `account/withdrawal` - User is attempting a withdrawal
- `sports/upcoming-events` - User is browsing upcoming sports events
- `promotions/welcome-bonus` - User is viewing the welcome bonus details

### Design Goals

**Stability**: Context identifiers should remain stable across frontend versions. Changes should be backward-compatible.

**Versioning**: If the context schema needs to evolve, consider prefixing contexts (e.g., `v2:bet-slip/empty`) or maintaining mappings between old and new identifiers.

**Language**: Context identifiers are language-neutral. The service may use additional request headers or query parameters to determine the response language in future versions.

**Granularity**: Balance specificity with maintainability. Too many contexts become hard to manage; too few contexts return generic content.

**Extensibility**: The hierarchical structure allows for new contexts to be added without breaking existing ones.

## Articles

### Article Model

Articles are the primary output format of the assistant service. Each article represents a piece of help content.

Current JSON structure:
```json
{
  "title": "string",
  "content": "string"
}
```

**Fields**:
- `title`: A human-readable title for the article (plain text)
- `content`: The article body, typically containing HTML markup

### Example Articles

**Bet Slip Help**:
```json
{
  "title": "How to Place a Bet",
  "content": "<p>To place a bet, select your desired outcome by clicking on the odds. This will add the selection to your bet slip. Review your bet slip, enter your stake amount, and click 'Place Bet' to confirm.</p>"
}
```

**Account Deposit Help**:
```json
{
  "title": "Making a Deposit",
  "content": "<p>To deposit funds into your account, navigate to the Account section and select Deposit. Choose your preferred payment method, enter the amount, and follow the on-screen instructions to complete the transaction.</p>"
}
```

### Article Extensions

The article model is intentionally minimal. Future extensions might include:

- `id`: Unique identifier for the article (useful for analytics, feedback)
- `category`: Topic category (e.g., "betting", "account", "promotions")
- `tags`: Array of keywords for additional context
- `relatedArticles`: Links to related help content
- `lastUpdated`: Timestamp of last content update
- `language`: Explicit language indicator (if multi-language support is added)

When extending, ensure backward compatibility by making new fields optional.

## Phase 1: Static Mapping

### Mapping Strategy

In Phase 1, context identifiers are directly mapped to pre-generated HTML files:

1. **Configuration**: A mapping file (JSON, YAML, or code-based) defines the relationship between contexts and articles.
2. **File Storage**: Help content is stored as HTML files in a dedicated directory structure.
3. **Lookup**: When a request arrives, the service looks up the context in the mapping.
4. **Loading**: The service loads the corresponding HTML files from disk.
5. **Response**: Articles are constructed with the configured titles and loaded content.

Example mapping structure:
```json
{
  "bet-slip/empty": [
    {
      "title": "How to Place a Bet",
      "contentFile": "help/betting/how-to-place-bet.html"
    },
    {
      "title": "Understanding Bet Types",
      "contentFile": "help/betting/bet-types.html"
    }
  ],
  "account/deposit": [
    {
      "title": "Making a Deposit",
      "contentFile": "help/account/deposit-guide.html"
    }
  ]
}
```

### Benefits

- **Simplicity**: Easy to implement and reason about
- **Performance**: Fast lookups and file reads
- **Control**: Curated, reviewed content for each context
- **Testability**: Straightforward to test with mock mappings

### Limitations

- **Static**: Cannot answer questions outside predefined contexts
- **Maintenance**: Adding new contexts requires updating mappings and creating content
- **Flexibility**: Limited ability to personalize or adapt responses

## Phase 2: Integration with RAG

In Phase 2, the same article model and context information remain relevant:

### Context as Signal

The context information becomes a signal for the RAG system:
- Helps narrow the semantic search space
- Provides grounding for LLM responses
- Enables fallback to static mappings if RAG fails

### Article Format Consistency

The RAG system should generate outputs that conform to the article model:
- Title: Generated or selected based on the question
- Content: LLM-generated response, potentially formatted as HTML

### Hybrid Approach

The service may use a hybrid strategy:
- Default to static mappings for known contexts without a user question
- Use RAG when a free-form question (`q` query parameter) is provided
- Fall back to static content if RAG fails or returns low-confidence results
