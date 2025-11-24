using Carter;
using Microsoft.Extensions.FileProviders;
using SportsbookAssistantService.Interfaces;
using SportsbookAssistantService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Carter registration
builder.Services.AddCarter();
// Assistant page service (phase 1: static markdown-backed)
builder.Services.AddSingleton<IAssistantPageService, MarkdownAssistantPageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Serve raw markdown content if needed under /content
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Content")),
    RequestPath = "/content"
});

// Map Carter modules
app.MapCarter();

app.Run();
