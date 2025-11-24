namespace SportsbookAssistantService.ViewModels;

/// <summary>
/// Represents an assistant page response with title and HTML content.
/// </summary>
public sealed class AssistantPageResponse
{
    /// <summary>Page title extracted from markdown first heading.</summary>
    public required string Title { get; init; }
    /// <summary>HTML converted content.</summary>
    public required string Content { get; init; }
}
