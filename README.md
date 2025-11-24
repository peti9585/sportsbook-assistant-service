# sportsbook-assistant-service

A REST API service that provides contextual help information for a sportsbook frontend application.

## Overview

This service delivers help articles to the sportsbook frontend based on context information, with plans to support intelligent question answering through LLM and RAG integration.

### Phases

- **Phase 1**: Static context-based help article delivery
- **Phase 2**: Intelligent question answering using LLM and RAG

## Technology Stack

- **Framework**: ASP.NET Core (.NET 10)
- **Language**: C# 13
- **API Style**: Minimal APIs

## Getting Started

### Prerequisites

- .NET 10 SDK

### Building

```bash
dotnet build SportsbookAssistantService.sln
```

### Configuration

#### OpenAI API Key

The service requires an OpenAI API key to use the question answering functionality. Configure it in one of the following ways:

**Option 1: User Secrets (Recommended for Development)**

```bash
cd SportsbookAssistantService
dotnet user-secrets init
dotnet user-secrets set "OpenAI:ApiKey" "your-api-key-here"
```

**Option 2: Environment Variable**

```bash
export OpenAI__ApiKey="your-api-key-here"
```

**Option 3: appsettings.Development.json**

```json
{
  "OpenAI": {
    "ApiKey": "your-api-key-here"
  }
}
```

**Note**: Never commit API keys to source control. The service will start without an API key but will return an error when the question answering endpoint is called.

### Running

```bash
dotnet run --project SportsbookAssistantService
```

## Documentation

- **Architecture**: See `docs/architecture/` for architecture decision records and design documentation
- **API**: See `docs/api/` for API documentation

## GitHub Copilot Configuration

This repository is configured with custom GitHub Copilot agents and instructions to provide specialized assistance:

- **Agent Definitions**: `.github/agents/` - Three specialized agents for development, architecture, and LLM/RAG design
- **General Instructions**: `.github/copilot-instructions.md` - Repository-wide Copilot guidance
- **Documentation**: `.github/copilot/README.md` - Details about the Copilot configuration

For more information about using Copilot with this repository, see [.github/copilot/README.md](.github/copilot/README.md).