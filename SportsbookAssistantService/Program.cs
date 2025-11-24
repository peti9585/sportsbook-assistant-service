using Carter;
using Microsoft.Extensions.FileProviders;
using SportsbookAssistantService.Configuration;
using SportsbookAssistantService.Interfaces;
using SportsbookAssistantService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure OpenAI settings
builder.Services.Configure<OpenAISettings>(builder.Configuration.GetSection("OpenAI"));

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "https://localhost:4200"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Carter registration
builder.Services.AddCarter();
// Assistant page service (phase 1: static HTML-backed)
builder.Services.AddSingleton<IAssistantPageService, MarkdownAssistantPageService>();
// Free-form question answering service using OpenAI
builder.Services.AddSingleton<IQuestionAnsweringService, OpenAIQuestionAnsweringService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Enable CORS
app.UseCors("AllowFrontend");

// Serve raw markdown content if needed under /content
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Content")),
    RequestPath = "/content"
});

// Map Carter modules
app.MapCarter();

app.Run();
