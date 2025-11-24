# GitHub Copilot Usage Guide

## Overview

This guide explains how to effectively use GitHub Copilot and the custom agents configured for the Sportsbook Assistant Service repository. Copilot can significantly accelerate development, improve code quality, and help maintain architectural consistency.

## Copilot Capabilities

GitHub Copilot provides several ways to assist with development:

1. **Inline Suggestions**: Real-time code completion as you type
2. **Chat Interface**: Conversational assistance for questions, explanations, and code generation
3. **Custom Agents**: Specialized agents tailored to specific tasks in this repository

## Custom Agents for This Repository

The Sportsbook Assistant Service has three custom agents, each specialized for different aspects of development:

### 1. Architecture Agent

**Purpose**: Facilitate architectural discussions, draft ADRs, and maintain architectural consistency.

**When to Use**:
- Planning a new feature that requires architectural decisions
- Evaluating different design approaches
- Creating or reviewing Architecture Decision Records (ADRs)
- Ensuring new code aligns with existing architecture
- Discussing trade-offs between different solutions

**Example Prompts**:

```
@architecture-agent I need to integrate with a sportsbook API. 
What are the key architectural considerations? Should we use a 
direct integration or an adapter pattern? Please provide options 
with pros/cons.
```

```
@architecture-agent Draft an ADR for choosing between REST and 
GraphQL for our API. Consider our use case: providing real-time 
odds data to multiple client types.
```

```
@architecture-agent Review the changes in this PR against our 
existing ADRs. Does this implementation align with our documented 
architectural decisions?
```

**Output Style**: The Architecture Agent provides high-level, trade-off focused analysis with diagrams when useful.

---

### 2. Backend Dev Agent

**Purpose**: Assist with C#/.NET backend implementation, following best practices and architectural guidelines.

**When to Use**:
- Implementing new API endpoints
- Refactoring existing code
- Writing or updating services, repositories, or domain logic
- Troubleshooting backend issues
- Ensuring code follows .NET conventions

**Example Prompts**:

```
@backend-dev-agent Implement a new endpoint GET /api/odds/{sport} 
that fetches odds from our sportsbook provider. Include proper error 
handling, logging, and caching.
```

```
@backend-dev-agent Refactor the OddsService to use the Repository 
pattern. Ensure it follows our dependency injection guidelines and 
includes appropriate unit tests.
```

```
@backend-dev-agent I'm getting a null reference exception in the 
OddsController. Review the code and suggest a fix that follows our 
nullability conventions.
```

**Output Style**: The Backend Dev Agent provides specific, implementation-ready code with explanations, respecting the repository's C#/.NET best practices.

---

### 3. Reviewer Agent

**Purpose**: Provide thorough code reviews focusing on correctness, architecture alignment, and maintainability.

**When to Use**:
- Before requesting human code reviews (to catch obvious issues early)
- When you want a second opinion on your implementation
- To check if your changes align with coding standards and architecture
- For learning purposes (understanding what makes good code)

**Example Prompts**:

```
@reviewer-agent Review this PR for correctness, architecture alignment, 
and adherence to our coding standards. Focus on the OddsService changes.
```

```
@reviewer-agent Check if this implementation follows our ADR on API 
design (ADR-0003). Are there any violations or areas for improvement?
```

```
@reviewer-agent Review the test coverage for the new OddsController. 
Are there edge cases or error scenarios we should test?
```

**Output Style**: The Reviewer Agent provides structured reviews with sections for Summary, Strengths, Concerns/Suggestions, Tests, and Documentation notes.

---

## General Copilot Usage Tips

### Inline Suggestions

**Best Practices**:
- Start typing a comment describing what you want, then let Copilot suggest the implementation
- Review suggestions carefully before accepting
- Use Tab to accept, Esc to reject
- Iterate on suggestions by modifying and re-triggering

**Example**:
```csharp
// Calculate implied probability from American odds
// (Copilot will suggest the implementation)
```

### Chat Interface

**When to Use Chat**:
- Asking questions about code or concepts
- Generating boilerplate code
- Explaining existing code
- Debugging issues
- Getting suggestions for improvements

**Effective Chat Prompts**:

1. **Be Specific**:
   - ❌ "How do I make this better?"
   - ✅ "How can I improve error handling in this method? Consider our logging conventions."

2. **Provide Context**:
   - ❌ "Write an API endpoint"
   - ✅ "Write an API endpoint for GET /api/odds/{sport} that returns odds for a specific sport. Use async/await, include logging, and follow our repository's patterns."

3. **Reference Relevant Files**:
   - Use `#file:path/to/file.cs` to include context
   - Example: "Refactor #file:Services/OddsService.cs to use dependency injection"

4. **Reference Documentation**:
   - "Review this code against our coding standards at #file:docs/guides/coding-standards.md"

### Workspace Context

Copilot can access your entire workspace. Help it by:
- Keeping relevant files open
- Referencing specific files with `#file:`
- Mentioning relevant ADRs or documentation

## Workflow Examples

### Example 1: Implementing a New Feature

```
Step 1: Architecture Discussion
@architecture-agent I need to implement a feature to compare odds 
across multiple sportsbooks. What architectural approach should I take? 
Consider caching, external API rate limits, and extensibility.

Step 2: Review ADRs
(Review suggested ADRs to ensure consistency)

Step 3: Implementation
@backend-dev-agent Implement the odds comparison service based on the 
architectural approach we discussed. Include:
- Service interface and implementation
- Caching layer
- Error handling for external API failures
- Unit tests

Step 4: Self-Review
@reviewer-agent Review the implementation I just created. Check for:
- Adherence to coding standards
- Proper error handling
- Test coverage
- Performance considerations

Step 5: Update Documentation
(If architectural decision made)
@architecture-agent Create an ADR for the odds comparison architecture 
we chose, including alternatives considered and trade-offs.

Step 6: Human Code Review
(Request review from team members)
```

### Example 2: Debugging an Issue

```
Step 1: Describe the Problem
"I'm getting a NullReferenceException in OddsController.GetOdds when 
the cache is unavailable. Here's the stack trace: [paste trace]"

Step 2: Ask Copilot to Analyze
"What's causing this exception? Check #file:Controllers/OddsController.cs 
and #file:Services/OddsService.cs"

Step 3: Request a Fix
@backend-dev-agent Implement proper null checking and fallback logic 
when the cache is unavailable. Follow our nullability conventions.

Step 4: Add Tests
"Generate unit tests to cover the scenario where the cache returns null"
```

### Example 3: Architecture Review

```
Step 1: Context
@architecture-agent I'm working on PR #123 which adds a new 
authentication layer. Review the changes against our existing architecture.

Step 2: ADR Check
@architecture-agent Check if there are existing ADRs related to 
authentication and authorization. Do my changes align with them?

Step 3: Document Decision
(If new architectural pattern introduced)
@architecture-agent Draft an ADR documenting the authentication approach 
used in this PR, including the decision to use JWT tokens and OAuth 2.0.
```

## Prompt Templates

### For Architecture Discussions
See [Architecture Discussion Template](../../.github/copilot/instructions/architecture-discussion-template.md)

### For PR Reviews
See [PR Review Template](../../.github/copilot/instructions/pr-review-template.md)

## Best Practices

### Do's ✅

- **Use specific prompts**: Provide context and detail
- **Reference relevant files and ADRs**: Help Copilot understand your codebase
- **Iterate**: If the first suggestion isn't perfect, refine your prompt
- **Verify suggestions**: Always review generated code before committing
- **Use custom agents**: Leverage specialized agents for their expertise areas
- **Combine with human review**: Use Copilot to augment, not replace, human judgment

### Don'ts ❌

- **Don't blindly accept**: Always review and test generated code
- **Don't commit secrets**: Copilot might suggest placeholder secrets; replace with real config
- **Don't skip tests**: Even if Copilot generates code, ensure it's tested
- **Don't ignore coding standards**: Review suggestions against our conventions
- **Don't bypass architectural decisions**: Use the Architecture Agent to ensure consistency

## Privacy and Security

- Copilot sends code snippets to OpenAI for processing
- Avoid including sensitive data in prompts (API keys, passwords, PII)
- Review Copilot suggestions for security vulnerabilities
- Follow your organization's policies on AI-assisted development

## Learning Resources

- [GitHub Copilot Documentation](https://docs.github.com/en/copilot)
- [Copilot Best Practices](https://github.blog/2023-06-20-how-to-write-better-prompts-for-github-copilot/)
- [Architecture Documentation](../architecture/README.md)
- [Coding Standards](./coding-standards.md)
- [Development Workflow](./development-workflow.md)

## Feedback and Improvement

This guide and the custom agents will evolve based on team feedback. If you discover effective prompt patterns or have suggestions for improving the agents:

1. Share in team discussions
2. Propose updates to this guide via PR
3. Suggest agent improvements to the team leads

## Summary

GitHub Copilot and custom agents are powerful tools that can accelerate development when used effectively. Remember:

- **Architecture Agent**: For design decisions and ADRs
- **Backend Dev Agent**: For C#/.NET implementation
- **Reviewer Agent**: For code review and quality checks

Use them in combination with your expertise and team collaboration for the best results.
