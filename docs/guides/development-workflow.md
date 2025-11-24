# Development Workflow

## Overview

This document describes the recommended workflow for contributing to the Sportsbook Assistant Service repository. Following these practices ensures code quality, maintainability, and smooth collaboration across the team.

## Branching Strategy

### Branch Types

We use a simplified Git workflow with the following branch types:

#### `main` Branch
- **Purpose**: The primary development branch containing production-ready code
- **Protection**: Protected branch requiring pull request reviews
- **Deployment**: Automatically deployed to staging; production deployments triggered from tags or manual approval

#### Feature Branches: `feature/<short-description>`
- **Purpose**: New features or enhancements
- **Examples**: 
  - `feature/odds-api-integration`
  - `feature/user-authentication`
  - `feature/caching-layer`
- **Naming**: Use lowercase with hyphens, be descriptive but concise

#### Bugfix Branches: `bugfix/<short-description>`
- **Purpose**: Bug fixes for issues found in testing or production
- **Examples**:
  - `bugfix/null-reference-in-odds-controller`
  - `bugfix/connection-timeout-handling`
- **Naming**: Similar to feature branches, reference issue number if available

#### Hotfix Branches: `hotfix/<short-description>`
- **Purpose**: Critical fixes that need to go to production immediately
- **Examples**: 
  - `hotfix/memory-leak-in-cache`
  - `hotfix/security-vulnerability-fix`
- **Process**: May bypass normal review process for urgent issues (with retrospective review)

### Workflow Steps

1. **Sync with `main`**:
   ```bash
   git checkout main
   git pull origin main
   ```

2. **Create a Feature/Bugfix Branch**:
   ```bash
   git checkout -b feature/my-new-feature
   ```

3. **Make Changes**: Implement your feature or fix, following coding standards

4. **Commit Frequently**: Make small, logical commits with clear messages

5. **Push to Remote**:
   ```bash
   git push origin feature/my-new-feature
   ```

6. **Open a Pull Request**: Create a PR targeting `main`

7. **Code Review**: Address feedback from reviewers

8. **Merge**: Once approved, merge using the "Squash and merge" strategy (preferred) or "Rebase and merge"

9. **Delete Branch**: Clean up the feature branch after merging

## Commit Message Guidelines

Good commit messages help understand the history of changes and make it easier to identify when issues were introduced.

### Format

```
<type>(<scope>): <subject>

<body>

<footer>
```

### Commit Types

- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes (formatting, no logic change)
- `refactor`: Code refactoring without changing behavior
- `test`: Adding or updating tests
- `chore`: Maintenance tasks (dependencies, build configuration)
- `perf`: Performance improvements

### Examples

**Simple commit**:
```
feat(odds): add NFL odds endpoint
```

**Detailed commit**:
```
fix(cache): prevent null reference when cache is unavailable

The cache client can return null when Redis is unavailable.
Added null checks and fallback to direct API calls.

Fixes #123
```

**Breaking change**:
```
feat(api): change odds response format

BREAKING CHANGE: The odds API now returns odds in decimal format
instead of American format. Clients must update their parsers.
```

### Guidelines

- **Use imperative mood**: "add feature" not "added feature" or "adds feature"
- **Be concise but descriptive**: Subject line under 72 characters
- **Reference issues**: Include issue numbers (e.g., `Fixes #123`, `Relates to #456`)
- **Explain why, not just what**: The code shows what changed; the commit message explains why

## Pull Request Best Practices

### Creating a Pull Request

1. **Title**: Clear and descriptive, following commit message conventions
   - Good: `feat(odds): add NFL odds endpoint`
   - Bad: `update code`

2. **Description**: Include:
   - **What**: Summary of changes
   - **Why**: Context and motivation
   - **How**: High-level approach (if not obvious)
   - **Testing**: How to test the changes
   - **Screenshots**: For UI changes (if applicable)
   - **Related ADRs**: Link to relevant architecture decisions
   - **Related Issues**: Reference issue numbers

3. **Size**: Keep PRs small and focused
   - **Ideal**: 50-200 lines of code (excluding tests)
   - **Maximum**: 400-500 lines (split larger changes into multiple PRs)
   - **Rationale**: Smaller PRs are easier to review, less likely to have bugs, and faster to merge

### Pull Request Template

```markdown
## Summary
Brief description of what this PR does.

## Changes
- Change 1
- Change 2
- Change 3

## Motivation
Why are these changes needed?

## Testing
How to test these changes:
1. Step 1
2. Step 2

## Related
- Fixes #123
- Related to ADR [0005](../docs/architecture/adr/0005-example.md)

## Checklist
- [ ] Tests added/updated
- [ ] Documentation updated
- [ ] ADR created/updated (if architectural change)
- [ ] No breaking changes (or clearly documented)
```

### Review Process

1. **Self-Review**: Review your own PR before requesting reviews
   - Check for typos, console.logs, commented code
   - Ensure tests pass locally
   - Verify the diff matches your intent

2. **Request Reviews**: Assign reviewers (typically 1-2 team members)

3. **Address Feedback**: Respond to comments promptly
   - Make requested changes or explain why not
   - Push additional commits (don't force-push during review)
   - Mark conversations as resolved

4. **Approval**: Get required approvals (minimum 1, ideally 2)

5. **Merge**: Use "Squash and merge" to keep a clean history

## Using GitHub Copilot in the Workflow

GitHub Copilot agents can assist throughout the development workflow:

### Planning Phase
- Use the **Architecture Agent** to discuss design approaches before coding
- Reference relevant ADRs to ensure consistency

### Implementation Phase
- Use **Copilot inline suggestions** for code completion
- Use the **Backend Dev Agent** for implementing complex features
- Ask Copilot to generate tests for your code

### Review Phase
- Use the **Reviewer Agent** to get an initial code review before requesting human reviews
- Ask Copilot to explain complex code sections

### Documentation Phase
- Use Copilot to draft or update documentation
- Generate ADRs for architectural decisions

See the [Copilot Usage Guide](./copilot-usage-guide.md) for detailed examples.

## Continuous Integration

### Automated Checks

When you push code or open a PR, the following checks run automatically:

1. **Build**: Ensure code compiles
2. **Tests**: Run unit and integration tests
3. **Linting**: Check code style and standards
4. **Security Scan**: Identify potential vulnerabilities

**All checks must pass before merging.**

### Local Validation

Before pushing, run these commands locally:

```bash
# Build
dotnet build

# Run tests
dotnet test

# Check formatting (if configured)
dotnet format --verify-no-changes
```

## Code Review Guidelines

### As a Reviewer

- **Be constructive**: Suggest improvements, don't just criticize
- **Be specific**: Point to exact lines and explain the issue
- **Ask questions**: "Could we...?" instead of "You should..."
- **Consider context**: Balance perfection with pragmatism
- **Approve promptly**: Don't block on minor issues that can be fixed later

### As an Author

- **Don't take it personally**: Reviews are about the code, not you
- **Be responsive**: Address feedback quickly
- **Explain your reasoning**: If you disagree with a suggestion, explain why
- **Learn and improve**: Each review is an opportunity to grow

## Definition of Done

A task is complete when:

- [ ] Code is implemented and works as expected
- [ ] Unit tests are added/updated and passing
- [ ] Integration tests are added/updated (if applicable)
- [ ] Documentation is updated (code comments, README, guides)
- [ ] ADR is created (if architectural decision)
- [ ] Code review is completed and approved
- [ ] CI checks pass
- [ ] PR is merged
- [ ] Feature is verified in staging environment

## Tips for Success

- **Commit often**: Frequent small commits are better than infrequent large ones
- **Test locally**: Don't rely on CI to catch basic errors
- **Update main frequently**: Rebase or merge `main` into your branch regularly to avoid conflicts
- **Communicate**: If you're blocked or need help, ask early
- **Use Copilot agents**: Leverage AI assistance for routine tasks, freeing time for complex problems

## References

- [GitHub Flow Guide](https://guides.github.com/introduction/flow/)
- [Conventional Commits](https://www.conventionalcommits.org/)
- [How to Write a Git Commit Message](https://chris.beams.io/posts/git-commit/)
