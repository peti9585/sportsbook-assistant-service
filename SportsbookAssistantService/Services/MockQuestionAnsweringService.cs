using System.Text;
using SportsbookAssistantService.Interfaces;
using SportsbookAssistantService.ViewModels;

namespace SportsbookAssistantService.Services;

/// <summary>
/// Mock implementation that simply reverses the question text.
/// </summary>
public sealed class MockQuestionAnsweringService : IQuestionAnsweringService
{
    public Task<QuestionResponse> AnswerAsync(string question, string? contextInfo, CancellationToken cancellationToken = default)
    {
        var safeQuestion = question ?? string.Empty;
        var reversed = Reverse(safeQuestion);
        var answer = $"Mock answer (context: {contextInfo ?? "none"}): {reversed}";

        return Task.FromResult(new QuestionResponse
        {
            Question = safeQuestion,
            Answer = answer
        });
    }

    private static string Reverse(string input)
    {
        if (input.Length == 0)
        {
            return input;
        }

        var builder = new StringBuilder(input.Length);
        for (var i = input.Length - 1; i >= 0; i--)
        {
            builder.Append(input[i]);
        }

        return builder.ToString();
    }
}
