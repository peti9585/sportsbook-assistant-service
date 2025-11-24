using SportsbookAssistantService.ViewModels;

namespace SportsbookAssistantService.Interfaces;

/// <summary>
/// Service for retrieving assistant help articles by context.
/// </summary>
public interface IAssistantPageService
{
    /// <summary>
    /// Loads a single assistant page for the specified context info.
    /// </summary>
    /// <param name="contextInfo">Context identifier, e.g. "bet-slip/empty".</param>
    /// <returns>The page, or null if not found.</returns>
    Task<AssistantPageResponse?> GetPageAsync(string contextInfo);
}
