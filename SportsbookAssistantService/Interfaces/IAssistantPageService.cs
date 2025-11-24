using SportsbookAssistantService.ViewModels;

namespace SportsbookAssistantService.Interfaces;

/// <summary>
/// Service to load static markdown assistant pages and convert them to HTML without external packages.
/// </summary>
public interface IAssistantPageService
{
    /// <summary>Loads the page by numeric id.</summary>
    /// <param name="pageId">Requested page id.</param>
    /// <returns>Assistant page response or null if not found.</returns>
    AssistantPageResponse? GetPage(int pageId);
}