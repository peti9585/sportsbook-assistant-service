# LLM and RAG Design (Phase 2)

## Overview

Phase 2 introduces the ability for the sportsbook assistant service to answer free-form user questions using Large Language Models (LLMs) combined with Retrieval-Augmented Generation (RAG). This design leverages the existing help content corpus while providing natural language understanding and dynamic answer generation.

## API Options

### Option 1: Query Parameter on Existing Endpoint

Extend the current endpoint to accept an optional query parameter:

```
GET /assistant/{context_info}?q={user_question}
```

**Example**:
```
GET /assistant/bet-slip/empty?q=How+do+I+add+multiple+bets+to+my+slip
```

**Behavior**:
- If `q` is absent: Return static articles for the context (Phase 1 behavior)
- If `q` is present: Use RAG to answer the question, using context as a signal

**Pros**:
- Single endpoint for both modes
- Context information still available to guide RAG
- Backward compatible with Phase 1 clients

**Cons**:
- Mixing two different retrieval strategies on one endpoint
- Query string length limitations for complex questions

### Option 2: Dedicated Query Endpoint

Create a new endpoint specifically for free-form questions:

```
POST /assistant/query
```

**Request Body**:
```json
{
  "question": "How do I add multiple bets to my slip?",
  "context": "bet-slip/empty"
}
```

**Pros**:
- Clear separation of concerns
- No query string length limitations
- Can include additional metadata (user preferences, language, etc.)

**Cons**:
- Two separate endpoints to maintain
- Potential for inconsistent behavior between endpoints

### Recommendation

Start with **Option 1** for simplicity and backward compatibility. The query parameter approach allows gradual rollout and A/B testing. If complexity grows, consider adding Option 2 later for advanced use cases.

## Data Pipeline

### Content Ingestion

1. **Source**: Static HTML files used in Phase 1 become the knowledge corpus
2. **Extraction**: Parse HTML to extract meaningful text content
3. **Metadata**: Preserve article titles, context associations, and structure
4. **Storage**: Raw content stored for reference and regeneration of index

### Chunking Strategy

Help articles need to be split into chunks for effective retrieval:

- **Chunk Size**: 256-512 tokens per chunk (balances specificity and context)
- **Overlap**: 50-100 tokens of overlap between chunks (preserves continuity)
- **Boundaries**: Split at natural boundaries (paragraphs, headings, lists)
- **Metadata**: Each chunk retains reference to source article, title, and context

Example chunk:
```json
{
  "chunkId": "article-123-chunk-2",
  "text": "To place a bet, select your desired outcome...",
  "sourceArticle": "how-to-place-bet.html",
  "title": "How to Place a Bet",
  "contexts": ["bet-slip/empty", "bet-slip/single-bet"],
  "embedding": [0.123, -0.456, ...]
}
```

### Embedding Generation

- **Model**: Use a domain-appropriate embedding model (e.g., OpenAI `text-embedding-3-small`, Azure OpenAI, or open-source alternatives)
- **Frequency**: Generate embeddings during content updates, not at request time
- **Dimensionality**: Balance between quality and storage/performance (768-1536 dimensions typical)

### Indexing

- **Vector Store**: Options include:
  - Azure AI Search (if on Azure)
  - Pinecone, Weaviate, or Qdrant (managed vector DBs)
  - PostgreSQL with pgvector (self-hosted option)
  - In-memory for small datasets (FAISS, Hnswlib)

- **Hybrid Search**: Combine vector similarity with keyword/BM25 search for better recall
- **Filtering**: Support filtering by context to narrow results

## Request Flow

### Step-by-Step Process

1. **Receive Request**: `GET /assistant/{context_info}?q={question}`
   - Validate inputs
   - Parse and sanitize the question

2. **Generate Query Embedding**:
   - Convert user question to embedding vector
   - Use same embedding model as corpus

3. **Retrieve Relevant Chunks**:
   - Perform vector similarity search in the index
   - Filter by context if desired (soft constraint)
   - Retrieve top K chunks (e.g., K=5-10)

4. **Construct LLM Prompt**:
   ```
   Context: The user is in the "{context_info}" area of the sportsbook.
   
   Relevant help content:
   {retrieved_chunk_1}
   {retrieved_chunk_2}
   ...
   
   User question: {question}
   
   Provide a clear, accurate answer based on the help content above.
   Format as HTML content for an article.
   ```

5. **Call LLM**:
   - Use appropriate model (GPT-4, GPT-3.5-turbo, or open-source alternatives)
   - Apply temperature and parameters for consistent, factual output
   - Include safety and content filtering

6. **Format Response**:
   - Extract generated answer
   - Construct article object:
     ```json
     {
       "title": "{Generated or derived title}",
       "content": "{LLM-generated HTML}"
     }
     ```

7. **Return to Client**:
   - Return as JSON array (single article or multiple if needed)
   - Include metadata (e.g., `source: "llm"`) if extending the model

### Error Handling

- **LLM Unavailable**: Fall back to static Phase 1 mappings
- **Low Confidence**: If retrieval scores are low, return static content or indicate uncertainty
- **Rate Limiting**: Implement request throttling and caching for frequent questions
- **Timeouts**: Set reasonable timeouts for LLM calls (e.g., 5-10 seconds)

## Constraints and Considerations

### Latency

- **Target**: <2 seconds end-to-end for RAG queries
- **Optimizations**:
  - Cache embeddings for common questions
  - Parallel retrieval and LLM calls where possible
  - Use faster embedding and LLM models if latency is critical

### Cost

- **Embedding Generation**: One-time cost per content update
- **Vector Search**: Minimal per-request cost
- **LLM Calls**: Primary ongoing cost
  - Estimate token usage: ~500-1000 input tokens, 200-500 output tokens per request
  - Use caching for identical questions
  - Consider cheaper models (GPT-3.5-turbo) vs. premium (GPT-4) based on quality needs

### Safety

- **Input Validation**: Sanitize user questions to prevent injection attacks
- **Output Filtering**: Review LLM responses for inappropriate content
- **Grounding**: Ensure answers stay grounded in provided help content
- **Guardrails**: Implement prompt engineering to avoid off-topic responses

### Observability

- **Logging**:
  - Log all user questions and generated responses
  - Track retrieval quality metrics (relevance scores)
  - Monitor LLM call success/failure rates

- **Metrics**:
  - Response time (P50, P95, P99)
  - LLM API latency
  - Cache hit rate for embeddings and responses
  - User feedback on answer quality (if feedback mechanism exists)

- **Monitoring**:
  - Alert on high error rates or latency spikes
  - Track cost and usage trends
  - Monitor rate limit consumption

## Future Enhancements

- **Fine-tuning**: Train a custom model on sportsbook-specific Q&A pairs
- **Multi-turn Conversations**: Support follow-up questions with conversation history
- **Personalization**: Incorporate user preferences and betting history
- **Feedback Loop**: Use user feedback to improve retrieval and generation quality
- **Multi-language Support**: Extend RAG pipeline to support multiple languages
