namespace SportsbookAssistantService.Configuration;

/// <summary>
/// Configuration settings for OpenAI integration.
/// </summary>
public sealed class OpenAISettings
{
    /// <summary>
    /// The OpenAI API key.
    /// </summary>
    public string? ApiKey { get; init; }

    /// <summary>
    /// The model to use for completions (e.g., "gpt-3.5-turbo", "gpt-4").
    /// </summary>
    public string Model { get; init; } = "gpt-3.5-turbo";

    /// <summary>
    /// Maximum number of tokens to generate in the response.
    /// </summary>
    public int MaxTokens { get; init; } = 500;

    /// <summary>
    /// Temperature for response generation (0.0-2.0).
    /// Higher values make output more random, lower values more deterministic.
    /// </summary>
    public float Temperature { get; init; } = 0.7f;

    /// <summary>
    /// Timeout for OpenAI API requests in seconds.
    /// </summary>
    public int TimeoutSeconds { get; init; } = 30;
}
