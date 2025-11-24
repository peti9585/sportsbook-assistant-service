using System.Text;
using SportsbookAssistantService.Interfaces;
using SportsbookAssistantService.ViewModels;

namespace SportsbookAssistantService.Services;

/// <summary>
/// File-system based implementation mapping pageId to markdown file whose filename starts with that id.
/// </summary>
public sealed class MarkdownAssistantPageService(IWebHostEnvironment env) : IAssistantPageService
{
    private readonly string _pagesRoot = Path.Combine(env.ContentRootPath, "Content", "AssistantPages");

    public AssistantPageResponse? GetPage(int pageId)
    {
        if (pageId <= 0) return null;
        if (!Directory.Exists(_pagesRoot)) return null;

        var file = Directory.GetFiles(_pagesRoot, $"{pageId}-*.md").OrderBy(f => f).FirstOrDefault();
        if (file == null) return null;
        var markdown = File.ReadAllText(file);
        var title = ExtractTitle(markdown) ?? Path.GetFileNameWithoutExtension(file);
        var html = ConvertMarkdownToHtml(markdown);
        return new AssistantPageResponse { Title = title, Content = html };
    }

    private static string? ExtractTitle(string markdown)
    {
        foreach (var line in markdown.Split('\n'))
        {
            var trimmed = line.Trim();
            if (trimmed.StartsWith("# "))
            {
                return trimmed[2..].Trim();
            }
        }
        
        return null;
    }

    // Very naive markdown to HTML conversion supporting headings (#, ##), paragraphs and unordered lists.
    private static string ConvertMarkdownToHtml(string markdown)
    {
        var lines = markdown.Replace("\r", string.Empty).Split('\n');
        var sb = new StringBuilder();
        var inList = false;
        foreach (var raw in lines)
        {
            var line = raw.TrimEnd();
            if (string.IsNullOrWhiteSpace(line))
            {
                if (inList)
                {
                    sb.AppendLine("</ul>");
                    inList = false;
                }
                continue;
            }
            if (line.StartsWith("# "))
            {
                if (inList) { sb.AppendLine("</ul>"); inList = false; }
                sb.Append("<h1>").Append(System.Net.WebUtility.HtmlEncode(line[2..].Trim())).AppendLine("</h1>");
            }
            else if (line.StartsWith("## "))
            {
                if (inList) { sb.AppendLine("</ul>"); inList = false; }
                sb.Append("<h2>").Append(System.Net.WebUtility.HtmlEncode(line[3..].Trim())).AppendLine("</h2>");
            }
            else if (line.StartsWith("* "))
            {
                if (!inList)
                {
                    sb.AppendLine("<ul>");
                    inList = true;
                }
                sb.Append("<li>").Append(System.Net.WebUtility.HtmlEncode(line[2..].Trim())).AppendLine("</li>");
            }
            else
            {
                if (inList) { sb.AppendLine("</ul>"); inList = false; }
                sb.Append("<p>").Append(System.Net.WebUtility.HtmlEncode(line.Trim())).AppendLine("</p>");
            }
        }
        if (inList) sb.AppendLine("</ul>");
        
        return sb.ToString();
    }
}
