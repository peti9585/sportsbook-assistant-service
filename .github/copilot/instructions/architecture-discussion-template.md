# Architecture Discussion Template

Use this template when starting an architecture discussion with GitHub Copilot. Copy the template below, fill in the placeholders, and paste it into Copilot Chat with the `@architecture-agent` mention.

---

## Template

```
@architecture-agent I need to discuss an architectural decision for the 
Sportsbook Assistant Service.

### Feature/Change Description
[Describe what you want to build or change. Be specific about functional 
requirements and expected behavior.]

### Context
[Provide relevant context:]
- Current implementation (if any): [link to files or describe]
- Related features: [list related functionality]
- Relevant ADRs: [list ADR numbers/names, or "None identified"]
- Constraints: [technical, business, or resource constraints]
- Non-functional requirements: [performance, scalability, security needs]

### Specific Questions
[What do you need help deciding? Examples:]
- Which integration pattern should I use?
- How should this be structured?
- What are the trade-offs between approach A and B?
- Does this align with our existing architecture?

### Desired Output
[What would be most helpful? Examples:]
- List of options with pros/cons
- Recommendation with rationale
- Draft ADR
- Architecture diagram
- Implementation approach

### Additional Information
[Any other relevant details, constraints, or considerations]
```

---

## Filled Example 1: API Integration Decision

```
@architecture-agent I need to discuss an architectural decision for the 
Sportsbook Assistant Service.

### Feature/Change Description
We need to integrate with the DraftKings sportsbook API to fetch real-time 
odds for NFL games. The API provides odds updates every 30 seconds and we 
need to serve this data to multiple downstream consumers with minimal latency.

### Context
- Current implementation: None, this is a new integration
- Related features: We already have a basic odds model defined in 
  SportsbookAssistantService/Models/OddsData.cs
- Relevant ADRs: None identified yet
- Constraints:
  - DraftKings API has rate limit of 100 requests/minute
  - We expect 5-10 downstream consumers initially, growing to 50+
  - Must maintain odds history for 24 hours
- Non-functional requirements:
  - Response time < 100ms for odds requests
  - 99.9% availability
  - Handle API downtime gracefully

### Specific Questions
1. Should we use a direct integration or an adapter/provider pattern?
2. What caching strategy is appropriate given the 30-second update frequency?
3. How should we handle rate limiting to avoid exhausting our API quota?
4. Should we poll the API or use webhooks (if available)?

### Desired Output
- List of integration patterns with pros/cons
- Caching strategy recommendation
- High-level component diagram
- Recommendation on whether this needs an ADR (and draft if yes)

### Additional Information
We may need to integrate with FanDuel and Caesars APIs in the future, so 
extensibility is important. The team has experience with REST APIs but not 
with WebSockets or webhooks.
```

---

## Filled Example 2: Authentication Approach

```
@architecture-agent I need to discuss an architectural decision for the 
Sportsbook Assistant Service.

### Feature/Change Description
We need to secure our API endpoints. Currently, all endpoints are publicly 
accessible with no authentication. We need to add authentication and 
authorization to control access to different endpoints based on client roles.

### Context
- Current implementation: No authentication, all endpoints are open
- Related features: None directly related
- Relevant ADRs: None identified
- Constraints:
  - Must support machine-to-machine authentication (API keys)
  - May need to support user authentication in the future
  - Team prefers industry-standard approaches
  - No budget for third-party auth services yet
- Non-functional requirements:
  - Minimal impact on API response time (< 10ms overhead)
  - Easy to integrate for downstream consumers
  - Secure storage of credentials

### Specific Questions
1. Should we use API keys, JWT tokens, OAuth 2.0, or something else?
2. How should we implement role-based access control?
3. Where should we store API keys/secrets?
4. Should this be middleware or attribute-based?

### Desired Output
- Comparison of authentication approaches (API keys vs JWT vs OAuth 2.0)
- Recommendation with rationale
- Draft ADR documenting the decision
- Example implementation snippet for the chosen approach

### Additional Information
We're using ASP.NET Core 10.0, so prefer built-in framework features when 
possible. The API will be called by both internal services and third-party 
clients.
```

---

## Filled Example 3: Data Storage Strategy

```
@architecture-agent I need to discuss an architectural decision for the 
Sportsbook Assistant Service.

### Feature/Change Description
We need to persist odds history for analytics and reporting. Currently, we 
only cache odds temporarily. We need to store historical odds data for at 
least 30 days, potentially up to 1 year for premium features.

### Context
- Current implementation: In-memory caching only, no persistence
- Related features: Odds fetching service, caching layer
- Relevant ADRs: None identified
- Constraints:
  - Expected data volume: ~10K odds updates per day, ~100 bytes each
  - Query patterns: Time-range queries, filters by sport/event
  - Budget: Prefer cost-effective solutions
- Non-functional requirements:
  - Write performance: Must not slow down real-time odds serving
  - Read performance: Analytics queries can tolerate 1-2 second latency
  - Data retention: 30-365 days based on client tier

### Specific Questions
1. What database technology is appropriate (SQL vs NoSQL)?
2. Should we use separate databases for operational data vs analytics?
3. How should we handle data retention and archival?
4. Do we need to consider time-series databases given the data pattern?

### Desired Output
- Database technology recommendation with pros/cons
- Data model sketch
- Archival strategy
- Draft ADR

### Additional Information
Team has experience with PostgreSQL and SQL Server. No NoSQL experience. 
Hosting on Azure, so Azure-native services are preferred but not required.
```

---

## Tips for Effective Architecture Discussions

1. **Be Specific**: Provide concrete details about requirements and constraints
2. **Include Context**: Reference existing code, ADRs, and related features
3. **State Constraints Explicitly**: Technical, business, resource, or skill constraints
4. **Ask Focused Questions**: Break down complex decisions into specific questions
5. **Request Structured Output**: Specify the format that would be most helpful
6. **Iterate**: If the first response isn't helpful, refine your prompt with more details
7. **Reference Documentation**: Point to relevant files using `#file:` syntax

## After the Discussion

Once you've had a productive architecture discussion:

1. **Document the Decision**: Create an ADR if the decision is significant
2. **Share with Team**: Discuss the recommendation with team members
3. **Update Documentation**: If the decision changes architecture docs, update them
4. **Link in PRs**: Reference the ADR in pull requests that implement the decision
5. **Iterate**: If implementation reveals issues, revisit and update the ADR

## See Also

- [ADR README](../../../docs/architecture/adr/README.md)
- [Architecture Documentation](../../../docs/architecture/README.md)
- [Copilot Usage Guide](../../../docs/guides/copilot-usage-guide.md)
