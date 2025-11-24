using Carter;
using SportsbookAssistantService.Interfaces;
using SportsbookAssistantService.ViewModels;

namespace SportsbookAssistantService.Modules;

/// <summary>
/// Carter module exposing assistant help endpoints.
/// </summary>
public sealed class MarkdownAssistantModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/assistant/{contextInfo}", async (
                string contextInfo,
                string? q,
                IAssistantPageService service,
                ILogger<MarkdownAssistantModule> logger) =>
        {
            var page = await service.GetPageAsync(contextInfo);
            if (page is null)
            {
                logger.LogInformation("Assistant page {ContextInfo} not found", contextInfo);
                return Results.NotFound();
            }

            logger.LogInformation("Assistant page {ContextInfo} served: {Title}", contextInfo, page.Title);

            // Wrap single page into an array to match article[] contract
            var articles = new[] { page };

            return Results.Ok(articles);
        })
        .WithName("GetAssistantPage")
        .Produces<AssistantPageResponse[]>()
        .Produces(StatusCodes.Status404NotFound);
    }
}
