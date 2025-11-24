using SportsbookAssistantService.Interfaces;
using SportsbookAssistantService.ViewModels;

namespace SportsbookAssistantService.Services;

/// <summary>
/// File-system based implementation mapping contextInfo to HTML file.
/// </summary>
public sealed class MarkdownAssistantPageService(IWebHostEnvironment env) : IAssistantPageService
{
    private const string startTag = "<h1";
    private readonly string _pagesRoot = Path.Combine(env.ContentRootPath, "Content", "AssistantPages");

    public async Task<AssistantPageResponse?> GetPageAsync(string contextInfo)
    {
        if (string.IsNullOrWhiteSpace(contextInfo)) return null;
        if (!Directory.Exists(_pagesRoot)) return null;

        // Map contextInfo like "bet-slip/empty" to a filename pattern, e.g. "bet-slip-empty.html"
        var safeName = contextInfo.Replace('/', '-');
        var file = Directory.GetFiles(_pagesRoot, $"{safeName}.html").OrderBy(f => f).FirstOrDefault();
        if (file == null) return null;

        var html = await File.ReadAllTextAsync(file);
        var title = ExtractTitleFromHtml(html) ?? Path.GetFileNameWithoutExtension(file);

        return new AssistantPageResponse { Title = title, Content = html };
    }

    private static string? ExtractTitleFromHtml(string html)
    {
        if (string.IsNullOrWhiteSpace(html)) return null;

        // Try to extract from <h1>...</h1>
        var idx = html.IndexOf(startTag, StringComparison.OrdinalIgnoreCase);
        if (idx >= 0)
        {
            var closeStart = html.IndexOf('>', idx);
            var closeEnd = html.IndexOf("</h1>", idx, StringComparison.OrdinalIgnoreCase);
            if (closeStart >= 0 && closeEnd > closeStart)
            {
                var inner = html.Substring(closeStart + 1, closeEnd - closeStart - 1);
                return System.Net.WebUtility.HtmlDecode(inner.Trim());
            }
        }

        // Fallback: try <title>...</title>
        var titleStart = html.IndexOf("<title>", StringComparison.OrdinalIgnoreCase);
        if (titleStart < 0) return null;
        {
            var titleEnd = html.IndexOf("</title>", titleStart, StringComparison.OrdinalIgnoreCase);
            
            if (titleEnd <= titleStart) return null;
            
            var inner = html.Substring(titleStart + "<title>".Length, titleEnd - titleStart - "<title>".Length);
            return System.Net.WebUtility.HtmlDecode(inner.Trim());
        }
    }
}
