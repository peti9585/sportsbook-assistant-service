using SportsbookAssistantService.ViewModels;

namespace SportsbookAssistantService.Interfaces;

/// <summary>
/// Service for handling free-form assistant questions.
/// </summary>
public interface IQuestionAnsweringService
{
    /// <summary>
    /// Answers a free-form question, optionally using context information.
    /// </summary>
    /// <param name="question">The user's question text.</param>
    /// <param name="contextInfo">Optional context identifier, e.g. "bet-slip/empty".</param>
    /// <returns>A mock response for now.</returns>
    Task<QuestionResponse> AnswerAsync(string question, string? contextInfo, CancellationToken cancellationToken = default);
}
