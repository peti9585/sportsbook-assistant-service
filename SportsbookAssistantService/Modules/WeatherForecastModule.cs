using Carter;
using SportsbookAssistantService.Interfaces;
using SportsbookAssistantService.ViewModels;

namespace SportsbookAssistantService.Modules;

/// <summary>
/// Carter module exposing weather forecast endpoints.
/// </summary>
public sealed class WeatherForecastModule : CarterModule
{
    // Remove legacy constructor-based route registration and implement AddRoutes override required by Carter 8
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/assistant/{pageId:int}", (int pageId, IAssistantPageService service, ILogger<WeatherForecastModule> logger) =>
        {
            var page = service.GetPage(pageId);
            if (page is null)
            {
                logger.LogInformation("Assistant page {PageId} not found", pageId);
                return Results.NotFound();
            }
            logger.LogInformation("Assistant page {PageId} served: {Title}", pageId, page.Title);
            return Results.Ok(page);
        })
        .WithName("GetAssistantPage")
        .Produces<AssistantPageResponse>()
        .Produces(StatusCodes.Status404NotFound);
    }
}
