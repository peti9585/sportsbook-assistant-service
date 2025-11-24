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
        app.MapGet("/assistant/{pageId:int}", async (
                int pageId,
                IAssistantPageService service,
                ILogger<MarkdownAssistantModule> logger) =>
        {
            var page = await service.GetPageAsync(pageId);
            if (page is null)
            {
                logger.LogInformation("Assistant page {PageId} not found", pageId);
                return Results.NotFound();
            }

            logger.LogInformation("Assistant page {PageId} served: {Title}", pageId, page.Title);

            // Wrap single page into an array to match article[] contract
            var articles = new[] { page };
            
            return Results.Ok(articles);
        })
        .WithName("GetAssistantPage")
        .Produces<AssistantPageResponse[]>()
        .Produces(StatusCodes.Status404NotFound);
    }
}
