using Carter;
using SportsbookAssistantService.Services;
using Microsoft.Extensions.FileProviders;
using SportsbookAssistantService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Carter registration
builder.Services.AddCarter();
builder.Services.AddSingleton<IAssistantPageService, MarkdownAssistantPageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Optionally serve raw markdown files if needed
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Content")),
    RequestPath = "/content"
});

// Map Carter modules
app.MapCarter();

app.Run();
