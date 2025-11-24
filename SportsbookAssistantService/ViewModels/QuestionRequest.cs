namespace SportsbookAssistantService.ViewModels;

/// <summary>
/// Request payload for free-form assistant questions.
/// </summary>
public sealed class QuestionRequest
{
    /// <summary>The user's question text.</summary>
    public required string Question { get; init; }

    /// <summary>Optional context identifier, e.g. "bet-slip/empty".</summary>
    public string? Context { get; init; }
}
