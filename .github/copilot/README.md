# GitHub Copilot Configuration

This directory contains legacy Copilot configuration files. As of the reorganization, the repository follows GitHub's 2024-2025 conventions for Copilot agents and instructions.

## Current Structure (Recommended)

The repository now uses the following structure:

```
.github/
├── agents/                           # Repository-level agent definitions
│   ├── dev-assistant.agent.md       # Development guidance agent
│   ├── architecture-assistant.agent.md  # Architecture decisions agent
│   └── llm-rag-assistant.agent.md   # LLM/RAG design agent
├── copilot-instructions.md          # General Copilot instructions for this repository
└── copilot/                         # Legacy folder (deprecated)
    ├── README.md                    # This file
    └── instructions/                # Old location (can be removed)
```

## Agent Definitions

The repository provides three specialized agents to assist with different aspects of development:

### 1. Development Assistant (`dev-assistant.agent.md`)

**Purpose**: Provides coding guidance, best practices, and implementation support for C#/.NET development.

**When to use**:
- Writing new features or endpoints
- Implementing services and business logic
- Creating tests
- Following code style and patterns

**Key areas**:
- ASP.NET Core Minimal APIs
- C# 13 features and best practices
- Dependency injection
- Error handling and logging

### 2. Architecture Assistant (`architecture-assistant.agent.md`)

**Purpose**: Guides architectural decisions, system design, and technical trade-offs.

**When to use**:
- Making architectural decisions
- Designing new components or layers
- Planning Phase 2 evolution
- Creating or updating ADRs
- Discussing patterns and principles

**Key areas**:
- Layered architecture
- Design patterns for .NET
- Incremental evolution strategy
- Interface design and abstraction

### 3. LLM & RAG Design Assistant (`llm-rag-assistant.agent.md`)

**Purpose**: Specializes in LLM integration, RAG design, and AI-powered features.

**When to use**:
- Planning Phase 2 LLM integration
- Designing semantic search
- Selecting LLM SDKs and vector databases
- Optimizing for cost and latency
- Handling LLM-specific concerns

**Key areas**:
- Azure OpenAI and Semantic Kernel
- Vector databases and embeddings
- RAG pipeline design
- Performance and security

## General Instructions

The file `.github/copilot-instructions.md` contains general guidance for all Copilot interactions in this repository, including:

- Project overview and technology stack
- C#/.NET coding standards
- Architecture guidelines
- Testing strategy
- Documentation conventions
- Common task examples

## Migration Notes

### What Changed (Reorganization)

1. **New location for agents**: `.github/agents/` (repository level)
   - Follows GitHub's 2024-2025 conventions
   - Uses `.agent.md` suffix for clarity
   - Better discoverability and precedence

2. **General instructions**: `.github/copilot-instructions.md`
   - Centralized repository-level guidance
   - Covers C#/.NET specific practices
   - Aligned with project architecture

3. **Enhanced content**:
   - Added C#/.NET specific guidance
   - Included technology stack details
   - Provided common task examples
   - Added testing and documentation guidelines

### Old Structure (Deprecated)

The previous structure used `.github/copilot/instructions/` with files named `*-agent.md`. These files have been migrated to the new structure with enhanced content.

**Old location**: `.github/copilot/instructions/`
- `dev-agent.md` → `.github/agents/dev-assistant.agent.md`
- `arch-agent.md` → `.github/agents/architecture-assistant.agent.md`
- `llm-rag-agent.md` → `.github/agents/llm-rag-assistant.agent.md`

## Folder Structure Conventions

GitHub Copilot supports agents at three levels:

1. **Organization Level**: `{org}/.github/agents/`
   - Applies to all repositories in the organization
   - Useful for company-wide standards

2. **Repository Level**: `.github/agents/` (used here)
   - Specific to this repository
   - Overrides organization-level agents with same name

3. **User/Workspace Level**: `~/.copilot/agents/`
   - Personal developer preferences
   - Lowest precedence

## Best Practices

- Keep agent files focused on their specific domain
- Update agents when project architecture evolves
- Reference docs in `docs/architecture/` for detailed decisions
- Use general instructions for cross-cutting concerns
- Document any custom agents in this README

## Future Enhancements

As the project grows, consider:

- Adding more specialized agents (testing, deployment, etc.)
- Creating prompt snippets in `.github/prompts/`
- Documenting organization-level agents if created
- Updating agents for Phase 2 LLM/RAG features

## References

- [GitHub Copilot Custom Agents Documentation](https://docs.github.com/en/copilot/reference/custom-agents-configuration)
- [Custom Agents for GitHub Copilot](https://github.blog/changelog/2025-10-28-custom-agents-for-github-copilot/)
- Project Architecture Docs: `docs/architecture/`
