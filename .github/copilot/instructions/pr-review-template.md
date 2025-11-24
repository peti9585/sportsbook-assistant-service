# PR Review Template

Use this template when requesting a code review from GitHub Copilot's Reviewer Agent. Copy the template below, fill in the placeholders, and paste it into Copilot Chat with the `@reviewer-agent` mention.

---

## Template

```
@reviewer-agent Please review this pull request for the Sportsbook Assistant Service.

### PR Information
- PR URL: [paste GitHub PR URL or number]
- Branch: [feature/bugfix branch name]
- Target: [usually 'main']

### Summary of Changes
[Provide a brief overview of what this PR does:]
- [Change 1]
- [Change 2]
- [Change 3]

### Scope of Review
[What should the review focus on? Check all that apply:]
- [ ] Correctness (logic, algorithms, edge cases)
- [ ] Architecture alignment (follows ADRs and patterns)
- [ ] Code quality (readability, maintainability)
- [ ] Performance implications
- [ ] Security concerns
- [ ] Test coverage
- [ ] Documentation accuracy
- [ ] Breaking changes

### Risk Level
[Indicate the risk level of these changes:]
- [ ] Low - Minor changes, well-tested, limited scope
- [ ] Medium - Moderate changes, some risk, standard review needed
- [ ] High - Significant changes, architectural impact, thorough review needed

### Relevant Context
- Related ADRs: [list ADR numbers or "None"]
- Related Issues: [GitHub issue numbers or "None"]
- Dependencies: [any external dependencies or related PRs]
- Special Considerations: [anything the reviewer should know]

### Specific Concerns
[Do you have specific concerns or areas you'd like extra scrutiny?]
- [Concern 1]
- [Concern 2]

### Desired Output
Please provide a structured review with these sections:
- Summary: High-level assessment
- Strengths: What's done well
- Concerns/Suggestions: Issues found and improvement suggestions
- Tests: Assessment of test coverage
- Documentation: Check if docs/ADRs need updates
```

---

## Filled Example 1: New Feature Implementation

```
@reviewer-agent Please review this pull request for the Sportsbook Assistant Service.

### PR Information
- PR URL: #42
- Branch: feature/odds-api-integration
- Target: main

### Summary of Changes
Implements integration with the DraftKings sportsbook API:
- Added OddsProvider interface and DraftKingsOddsProvider implementation
- Implemented caching layer using IMemoryCache
- Added OddsController with GET endpoint for fetching odds
- Included unit tests for OddsProvider and integration tests for the endpoint
- Updated configuration to include DraftKings API settings

### Scope of Review
Focus on:
- [x] Correctness (logic, algorithms, edge cases)
- [x] Architecture alignment (follows ADRs and patterns)
- [x] Code quality (readability, maintainability)
- [x] Performance implications
- [x] Security concerns
- [x] Test coverage
- [ ] Documentation accuracy (docs not yet updated)
- [x] Breaking changes

### Risk Level
- [x] Medium - Moderate changes, new external integration, standard review needed

### Relevant Context
- Related ADRs: ADR-0005-sportsbook-api-integration-pattern
- Related Issues: #38 (Add DraftKings integration)
- Dependencies: None
- Special Considerations: This is the first external API integration; the 
  pattern established here will be used for future integrations

### Specific Concerns
- Is the error handling robust enough for API failures?
- Is the caching strategy appropriate (30-second TTL)?
- Should I add circuit breaker pattern now or later?
- Are there security concerns with API key storage?

### Desired Output
Please provide a structured review with these sections:
- Summary: High-level assessment
- Strengths: What's done well
- Concerns/Suggestions: Issues found and improvement suggestions
- Tests: Assessment of test coverage
- Documentation: Check if docs/ADRs need updates
```

---

## Filled Example 2: Bug Fix

```
@reviewer-agent Please review this pull request for the Sportsbook Assistant Service.

### PR Information
- PR URL: #55
- Branch: bugfix/null-reference-in-odds-service
- Target: main

### Summary of Changes
Fixes NullReferenceException in OddsService when cache is unavailable:
- Added null checks in OddsService.GetOddsAsync method
- Implemented fallback to direct API call when cache returns null
- Added unit test to cover the null cache scenario
- Improved error logging for cache failures

### Scope of Review
Focus on:
- [x] Correctness (logic, algorithms, edge cases)
- [ ] Architecture alignment (no architectural changes)
- [x] Code quality (readability, maintainability)
- [ ] Performance implications (minimal impact)
- [ ] Security concerns (none expected)
- [x] Test coverage
- [ ] Documentation accuracy (no doc changes needed)
- [ ] Breaking changes (none)

### Risk Level
- [x] Low - Targeted bug fix, well-tested, limited scope

### Relevant Context
- Related ADRs: None
- Related Issues: #54 (Production incident: NullReferenceException in OddsService)
- Dependencies: None
- Special Considerations: This was causing production errors, so accuracy is critical

### Specific Concerns
- Are there other places in the codebase with similar null handling issues?
- Is the fallback approach correct, or should we fail fast?
- Should we add monitoring/alerting for cache failures?

### Desired Output
Please provide a structured review with these sections:
- Summary: High-level assessment
- Strengths: What's done well
- Concerns/Suggestions: Issues found and improvement suggestions
- Tests: Assessment of test coverage
- Documentation: Check if docs/ADRs need updates
```

---

## Filled Example 3: Refactoring

```
@reviewer-agent Please review this pull request for the Sportsbook Assistant Service.

### PR Information
- PR URL: #67
- Branch: refactor/odds-service-repository-pattern
- Target: main

### Summary of Changes
Refactors OddsService to use Repository pattern:
- Created IOddsRepository interface
- Implemented InMemoryOddsRepository (for now, will add SQL later)
- Updated OddsService to use the repository
- Refactored existing tests to use mock repository
- No changes to public API surface

### Scope of Review
Focus on:
- [x] Correctness (ensure behavior unchanged)
- [x] Architecture alignment (Repository pattern)
- [x] Code quality (readability, maintainability)
- [ ] Performance implications (should be equivalent)
- [ ] Security concerns (none expected)
- [x] Test coverage (should remain the same)
- [ ] Documentation accuracy (architecture docs to be updated separately)
- [x] Breaking changes (none intended)

### Risk Level
- [x] Medium - Refactoring with potential for unintended behavior changes

### Relevant Context
- Related ADRs: Will create ADR-0008 for repository pattern decision
- Related Issues: #60 (Standardize data access patterns)
- Dependencies: None
- Special Considerations: This sets the pattern for future repository 
  implementations (SQL, NoSQL)

### Specific Concerns
- Does the abstraction make sense for our use case?
- Is the repository interface too generic or too specific?
- Should the repository handle caching, or should that be separate?
- Are there any behavior changes I missed during refactoring?

### Desired Output
Please provide a structured review with these sections:
- Summary: High-level assessment
- Strengths: What's done well
- Concerns/Suggestions: Issues found and improvement suggestions
- Tests: Assessment of test coverage
- Documentation: Check if docs/ADRs need updates
```

---

## Tips for Effective PR Reviews

### Before Requesting Review

1. **Self-Review First**: Review your own PR using this checklist
2. **Run All Tests**: Ensure tests pass locally
3. **Check Formatting**: Run `dotnet format` if configured
4. **Update Documentation**: If needed, update docs before review

### Requesting the Review

1. **Be Specific**: Indicate what areas need attention
2. **Provide Context**: Link related issues, ADRs, and documentation
3. **State Concerns**: If you're unsure about something, call it out
4. **Set Expectations**: Indicate risk level and review scope

### After the Review

1. **Address Feedback**: Implement suggested changes or explain why not
2. **Update PR**: Push changes and re-request review if significant
3. **Human Review**: Always get human review in addition to Copilot
4. **Thank the Reviewer**: Acknowledge helpful feedback

## Review Output Structure

The Reviewer Agent will provide output in this format:

### Summary
High-level assessment of the PR (good to merge, needs changes, etc.)

### Strengths
- Positive aspects of the implementation
- Good practices followed
- Well-done areas

### Concerns/Suggestions
- Issues found (critical, major, minor)
- Improvement suggestions
- Questions about implementation choices

### Tests
- Test coverage assessment
- Missing test scenarios
- Test quality feedback

### Documentation
- Documentation accuracy check
- ADR creation/update needs
- Comment quality feedback

## See Also

- [Development Workflow](../../../docs/guides/development-workflow.md)
- [Coding Standards](../../../docs/guides/coding-standards.md)
- [Copilot Usage Guide](../../../docs/guides/copilot-usage-guide.md)
