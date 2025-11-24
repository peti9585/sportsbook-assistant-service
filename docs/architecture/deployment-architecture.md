# Deployment Architecture

## Overview

This document describes the deployment architecture for the Sportsbook Assistant Service, a .NET 10.0 ASP.NET Core Web API application. The details below represent typical deployment patterns for .NET backend services and should be adapted by the team based on actual infrastructure requirements.

## Environments

### Local Development
- **Purpose**: Developer workstations for feature development and testing
- **Runtime**: .NET 10.0 SDK
- **Configuration**: `appsettings.Development.json`
- **Dependencies**: 
  - Mock or local instances of external services
  - In-memory caching (no Redis required locally)
  - SQLite or LocalDB for database needs
- **Access**: Localhost only

### Staging / Pre-Production
- **Purpose**: Integration testing and QA validation before production release
- **Runtime**: .NET 10.0 Runtime (production configuration)
- **Configuration**: `appsettings.Staging.json`, environment variables
- **Dependencies**: 
  - Staging instances of external APIs
  - Shared cache (e.g., Azure Cache for Redis, AWS ElastiCache)
  - Test database with production-like schema
- **Access**: Limited to internal networks or VPN

### Production
- **Purpose**: Live environment serving real users and consumers
- **Runtime**: .NET 10.0 Runtime (optimized, production configuration)
- **Configuration**: `appsettings.Production.json`, secure environment variables, secret management
- **Dependencies**: 
  - Production sportsbook APIs
  - Production-grade cache with high availability
  - Production database with backup and replication
- **Access**: Public internet (with appropriate security measures)

## Deployment Topology

### Placeholder: Cloud-Native Deployment (Example)

The following topology is a **generic placeholder**. The team should refine it based on chosen cloud provider (Azure, AWS, GCP) or on-premises infrastructure.

```
┌─────────────────────────────────────────────────────────────┐
│                       Load Balancer                          │
│                 (Azure Front Door / AWS ALB                  │
│                  / Kubernetes Ingress)                       │
└────────────────────────┬────────────────────────────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────────┐
│              Application Tier (Auto-scaled)                  │
│   ┌──────────────┐  ┌──────────────┐  ┌──────────────┐     │
│   │  App Instance│  │  App Instance│  │  App Instance│     │
│   │  (Container/ │  │  (Container/ │  │  (Container/ │     │
│   │   App Service│  │   App Service│  │   App Service│     │
│   │      Pod)    │  │      Pod)    │  │      Pod)    │     │
│   └──────────────┘  └──────────────┘  └──────────────┘     │
└─────────────────────────────────────────────────────────────┘
                         │
         ┌───────────────┼───────────────┐
         ▼               ▼               ▼
┌─────────────┐  ┌──────────────┐  ┌──────────────┐
│   Cache     │  │   Database   │  │  External    │
│  (Redis)    │  │ (SQL Server/ │  │  Sportsbook  │
│             │  │  PostgreSQL) │  │    APIs      │
└─────────────┘  └──────────────┘  └──────────────┘
```

### Application Tier Options

**Option A: Container-based (Kubernetes)**
- Deploy as Docker containers to a Kubernetes cluster (AKS, EKS, GKE)
- Benefits: Portability, orchestration, advanced deployment strategies
- Considerations: Requires K8s expertise, more operational overhead

**Option B: Platform-as-a-Service (Azure App Service / AWS App Runner)**
- Deploy directly to managed app service
- Benefits: Lower operational overhead, automatic scaling, easy SSL/DNS management
- Considerations: Potential vendor lock-in, less control over infrastructure

**Option C: Serverless (AWS Lambda / Azure Functions with Container Support)**
- Deploy as serverless functions (note: requires specific architectural adjustments)
- Benefits: Pay-per-use, automatic scaling
- Considerations: Cold start latency, execution time limits

**Recommendation**: Option B (PaaS) for initial deployment, with Option A (Kubernetes) considered for future scaling needs. This should be captured in an ADR.

## Deployment Process

### CI/CD Pipeline (Generic)

1. **Trigger**: Code merged to main branch or release tag created
2. **Build Stage**:
   - Restore NuGet packages
   - Compile C# code
   - Run unit tests
   - Static code analysis (if configured)
3. **Package Stage**:
   - Publish .NET application (`dotnet publish`)
   - Create deployment artifact (ZIP, Docker image, etc.)
4. **Deploy to Staging**:
   - Deploy artifact to staging environment
   - Run integration tests
   - Smoke tests
5. **Manual Approval** (optional gate for production)
6. **Deploy to Production**:
   - Blue-green or rolling deployment
   - Health check validation
   - Rollback capability if health checks fail

### Deployment Strategies

- **Rolling Deployment**: Update instances gradually (default for many PaaS platforms)
- **Blue-Green Deployment**: Maintain two identical environments, switch traffic after validation
- **Canary Deployment**: Route small percentage of traffic to new version, gradually increase

## Reliability & Scalability

### Health Checks

Implement ASP.NET Core health checks:

```csharp
// Liveness: Is the service running?
app.MapHealthChecks("/health/live");

// Readiness: Is the service ready to accept traffic?
app.MapHealthChecks("/health/ready");
```

Health checks should verify:
- Application process is running
- Database connectivity
- External API connectivity
- Cache availability

### Scaling

**Horizontal Scaling**: Add more application instances based on:
- CPU utilization (threshold: 70-80%)
- Memory utilization (threshold: 70-80%)
- Request queue length
- Custom metrics (e.g., API response time)

**Vertical Scaling**: Increase resources per instance (less common for cloud deployments)

### Observability

- **Logging**: Centralized logging to Azure Monitor / CloudWatch / ELK stack
- **Metrics**: Application Insights / Prometheus metrics
- **Tracing**: Distributed tracing for request flows across services
- **Alerts**: Configured for error rates, latency thresholds, availability

### Disaster Recovery

- **Database Backups**: Automated daily backups with point-in-time recovery
- **Multi-Region Deployment** (optional): Deploy to multiple regions for high availability
- **RTO/RPO Targets**: Define and document recovery time and recovery point objectives

## Configuration Management

- **Secrets**: Use Azure Key Vault, AWS Secrets Manager, or Kubernetes Secrets
- **Environment Variables**: Non-sensitive configuration
- **Feature Flags**: LaunchDarkly, Azure App Configuration, or custom solution

## Security Considerations

- **HTTPS Only**: All traffic encrypted in transit
- **Authentication**: OAuth 2.0 / JWT tokens for API authentication (if required)
- **Network Security**: Firewall rules, VPC/VNet isolation, private endpoints
- **Secrets Management**: Never store secrets in code or configuration files
- **Vulnerability Scanning**: Regular scanning of container images and dependencies

## Cost Optimization

- **Auto-scaling**: Scale down during low-traffic periods
- **Reserved Instances**: Consider reserved capacity for predictable workloads
- **Caching**: Reduce database and external API calls
- **Resource Right-sizing**: Monitor and adjust instance sizes based on actual usage

## Next Steps

1. **Select Infrastructure Platform**: Decide on cloud provider or on-premises deployment
2. **Create ADR**: Document deployment platform decision (see [ADR Template](./adr/README.md))
3. **Set Up CI/CD Pipeline**: Implement automated build and deployment
4. **Configure Monitoring**: Set up observability tools and alerts
5. **Define Runbooks**: Document operational procedures for common scenarios

## Notes

This deployment architecture is a **starting point and placeholder**. The team should:
- Replace generic examples with actual infrastructure choices
- Create ADRs for significant deployment decisions
- Update this document as the architecture evolves
- Document environment-specific configurations and credentials securely
