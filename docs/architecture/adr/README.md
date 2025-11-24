# Architecture Decision Records (ADRs)

## Purpose

Architecture Decision Records (ADRs) document significant architectural decisions made for the Sportsbook Assistant Service. An ADR captures the context, decision, and consequences of each choice, providing a historical record that helps current and future team members understand why the system is built the way it is.

## When to Write an ADR

Create an ADR when making decisions about:

- **Technology Selection**: Choosing frameworks, libraries, databases, or cloud services
- **Architectural Patterns**: Selecting design patterns or architectural styles (e.g., layered architecture, microservices, event-driven)
- **Integration Approaches**: Deciding how to integrate with external systems
- **Data Models**: Defining significant data structures or storage strategies
- **Security Practices**: Establishing authentication, authorization, or data protection mechanisms
- **Operational Practices**: Defining deployment strategies, monitoring approaches, or scaling patterns
- **Development Processes**: Setting standards for testing, code review, or versioning

**Rule of Thumb**: If a decision will be hard to reverse or significantly impacts the system, write an ADR.

## ADR File Naming Convention

ADRs should follow this naming pattern:

```
NNNN-short-title-with-dashes.md
```

Where:
- `NNNN` is a four-digit sequential number (0001, 0002, 0003, etc.)
- `short-title-with-dashes` is a brief, descriptive title in lowercase with hyphens

**Examples**:
- `0001-record-architecture-decisions.md`
- `0002-use-postgresql-for-primary-database.md`
- `0003-adopt-cqrs-pattern-for-reporting.md`

## ADR Structure

Each ADR should include the following sections:

### Title
Format: `NNNN – [Decision Title]`

### Status
One of:
- **Proposed**: Decision is under discussion
- **Accepted**: Decision has been approved and should be implemented
- **Deprecated**: Decision is no longer recommended but may still be in use
- **Superseded**: Decision has been replaced by a newer ADR (link to the new ADR)

### Date
The date the decision was made or last updated (YYYY-MM-DD format).

### Deciders
List of people or roles involved in the decision (e.g., "Backend team", "Architecture review board").

### Technical Area
Category of the decision (e.g., "Backend", "Infrastructure", "Security", "Testing").

### Context
Describe the problem or situation that requires a decision. Include:
- What is the issue we're trying to solve?
- What constraints do we have?
- What requirements must be met?

### Decision
State the decision clearly and concisely. This is the "what" we've decided to do.

### Consequences
Document both positive and negative outcomes of the decision:
- **Positive**: Benefits, improvements, or problems solved
- **Negative**: Trade-offs, limitations, or new problems introduced

### Alternatives Considered
List other options that were evaluated and why they were not chosen. This provides valuable context for future reviews.

### References (Optional)
Links to related documents, external resources, or other ADRs.

## Workflow for Creating ADRs

### 1. Identify the Need
Recognize that an architectural decision needs to be documented.

### 2. Draft the ADR
Create a new file with the next sequential number and a descriptive title. Fill in all sections.

**Tip**: Use the GitHub Copilot Architecture Agent to help draft ADRs. See the [Architecture Discussion Template](../../.github/copilot/instructions/architecture-discussion-template.md) for guidance.

### 3. Review and Discuss
Share the draft ADR with relevant stakeholders:
- Post in a pull request for team review
- Discuss in architecture meetings
- Use the Architecture Agent to evaluate trade-offs

### 4. Finalize and Merge
Once consensus is reached:
- Update the status to **Accepted**
- Merge the ADR into the main branch
- Link the ADR in any pull requests that implement the decision

### 5. Implement
Follow the decision during implementation. If you need to deviate, update the ADR or create a new one that supersedes it.

### 6. Update as Needed
If circumstances change:
- Mark the ADR as **Deprecated** or **Superseded**
- Create a new ADR documenting the new decision
- Link between related ADRs

## Using GitHub Copilot for ADRs

The repository includes a custom **Architecture Agent** that can assist with ADR creation:

1. **Start an Architecture Discussion**: Use the [Architecture Discussion Template](../../.github/copilot/instructions/architecture-discussion-template.md)
2. **Generate an ADR Draft**: Ask the Architecture Agent to draft an ADR based on the discussion
3. **Refine the ADR**: Iterate with the agent to improve clarity and completeness
4. **Review Trade-offs**: Use the agent to identify potential consequences and alternatives

See the [Copilot Usage Guide](../../guides/copilot-usage-guide.md) for detailed examples.

## ADR Index

| Number | Title | Status | Date | Area |
|--------|-------|--------|------|------|
| [0001](./0001-record-architecture-decisions.md) | Record Architecture Decisions | Accepted | 2025-11-24 | Governance |

## References

- [Michael Nygard's ADR Template](https://cognitect.com/blog/2011/11/15/documenting-architecture-decisions)
- [GitHub ADR Organization](https://adr.github.io/)
