namespace SportsbookAssistantService.ViewModels;

/// <summary>
/// Response payload for free-form assistant questions.
/// </summary>
public sealed class QuestionResponse
{
    /// <summary>The original question.</summary>
    public required string Question { get; init; }

    /// <summary>The mock answer (currently the reversed question).</summary>
    public required string Answer { get; init; }
}
