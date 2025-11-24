using Carter;
using SportsbookAssistantService.Interfaces;
using SportsbookAssistantService.ViewModels;

namespace SportsbookAssistantService.Modules;

/// <summary>
/// Carter module exposing free-form question endpoint.
/// </summary>
public sealed class QuestionModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/assistant/query", async (
                QuestionRequest request,
                IQuestionAnsweringService service,
                ILogger<QuestionModule> logger,
                CancellationToken cancellationToken) =>
            {
                if (string.IsNullOrWhiteSpace(request.Question))
                {
                    logger.LogInformation("Received empty assistant question");
                    return Results.BadRequest(new
                    {
                        error = "Invalid question",
                        message = "Question must not be empty."
                    });
                }

                var response = await service.AnswerAsync(request.Question, request.Context, cancellationToken);

                logger.LogInformation("Answered assistant question for context {Context}", request.Context ?? "none");

                return Results.Ok(response);
            })
            .WithName("PostAssistantQuery")
            .Produces<QuestionResponse>()
            .Produces(StatusCodes.Status400BadRequest);
    }
}
