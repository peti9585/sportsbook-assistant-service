using Microsoft.Extensions.Options;
using OpenAI.Chat;
using SportsbookAssistantService.Configuration;
using SportsbookAssistantService.Interfaces;
using SportsbookAssistantService.ViewModels;

namespace SportsbookAssistantService.Services;

/// <summary>
/// OpenAI-based implementation of the question answering service.
/// Uses OpenAI's chat completion API to answer user questions.
/// </summary>
public sealed class OpenAIQuestionAnsweringService : IQuestionAnsweringService
{
    private readonly ChatClient _chatClient;
    private readonly OpenAISettings _settings;
    private readonly ILogger<OpenAIQuestionAnsweringService> _logger;

    public OpenAIQuestionAnsweringService(
        IOptions<OpenAISettings> settings,
        ILogger<OpenAIQuestionAnsweringService> logger)
    {
        _settings = settings.Value;
        _logger = logger;

        // Validate API key
        if (string.IsNullOrWhiteSpace(_settings.ApiKey))
        {
            throw new InvalidOperationException(
                "OpenAI API key is not configured. Please set the OpenAI:ApiKey configuration value.");
        }

        // Initialize the OpenAI chat client
        _chatClient = new ChatClient(_settings.Model, _settings.ApiKey);
    }

    public async Task<QuestionResponse> AnswerAsync(
        string question,
        string? contextInfo,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(question))
        {
            throw new ArgumentException("Question cannot be empty.", nameof(question));
        }

        try
        {
            _logger.LogInformation(
                "Processing question with OpenAI - Context: {Context}, Question length: {Length}",
                contextInfo ?? "none",
                question.Length);

            // Build the system prompt
            var systemPrompt = BuildSystemPrompt(contextInfo);

            // Create chat messages
            var messages = new List<ChatMessage>
            {
                new SystemChatMessage(systemPrompt),
                new UserChatMessage(question)
            };

            // Call OpenAI API with timeout
            var chatOptions = new ChatCompletionOptions
            {
                MaxOutputTokenCount = _settings.MaxTokens,
                Temperature = _settings.Temperature
            };

            // Create a timeout token source combined with the provided cancellation token
            using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(_settings.TimeoutSeconds));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

            var completion = await _chatClient.CompleteChatAsync(
                messages,
                chatOptions,
                linkedCts.Token);

            var answer = completion.Value.Content[0].Text;

            _logger.LogInformation(
                "Successfully generated answer - Input tokens: {InputTokens}, Output tokens: {OutputTokens}",
                completion.Value.Usage.InputTokenCount,
                completion.Value.Usage.OutputTokenCount);

            return new QuestionResponse
            {
                Question = question,
                Answer = answer
            };
        }
        catch (OperationCanceledException ex) when (ex.CancellationToken.IsCancellationRequested && !cancellationToken.IsCancellationRequested)
        {
            _logger.LogWarning("OpenAI API request timed out after {Timeout} seconds", _settings.TimeoutSeconds);
            throw new TimeoutException($"The request to OpenAI timed out after {_settings.TimeoutSeconds} seconds.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling OpenAI API for question answering");
            throw new InvalidOperationException("Failed to generate answer using OpenAI.", ex);
        }
    }

    private static string BuildSystemPrompt(string? contextInfo)
    {
        var prompt = "You are a helpful assistant for a sportsbook application. " +
                     "Provide clear, accurate, and concise answers to user questions about using the sportsbook.";

        if (!string.IsNullOrWhiteSpace(contextInfo))
        {
            prompt += $" The user is currently in the '{contextInfo}' area of the application.";
        }

        prompt += " Keep your answers focused on sportsbook functionality and betting-related topics.";

        return prompt;
    }
}
