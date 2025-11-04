using Amazon.Lambda.AspNetCoreServer.Hosting;
using Parentaliza.Domain.Repository;
using Parentaliza.API.Infrastructure;
using Parentaliza.ServiceDefaults;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add Lambda hosting
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

// Configure EF Core MySQL DbContext
var rawConnectionString = builder.Configuration.GetConnectionString("Default") ?? string.Empty;
string BuildPomeloConnectionString(string value)
{
    if (!string.IsNullOrWhiteSpace(value) && value.StartsWith("mysql://", StringComparison.OrdinalIgnoreCase))
    {
        var uri = new Uri(value);
        var userInfoParts = (uri.UserInfo ?? string.Empty).Split(':', 2);
        var user = userInfoParts.Length > 0 ? Uri.UnescapeDataString(userInfoParts[0]) : string.Empty;
        var password = userInfoParts.Length > 1 ? Uri.UnescapeDataString(userInfoParts[1]) : string.Empty;
        var host = uri.Host;
        var port = uri.IsDefaultPort ? 3306 : uri.Port;
        var database = uri.AbsolutePath.Trim('/');
        return $"Server={host};Port={port};Database={database};User={user};Password={password};SslMode=None;";
    }
    return value;
}

var pomeloConnectionString = BuildPomeloConnectionString(rawConnectionString);

builder.Services.AddDbContext<ParentalizaDbContext>(options =>
{
    options.UseMySql(pomeloConnectionString, ServerVersion.AutoDetect(pomeloConnectionString));
});

// Use EF repository instead of in-memory
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Add Controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Parentaliza API",
        Description = "AWS Lambda ASP.NET Core API Parentaliza",
    });
    
    // Include XML comments
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add Exception Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseExceptionHandler();

// Configure Swagger (before MapDefaultEndpoints to avoid conflicts)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parentaliza API V1");
        c.RoutePrefix = "swagger"; // Swagger UI at /swagger
        c.DocumentTitle = "Parentaliza API Documentation";
        c.DefaultModelsExpandDepth(-1); // Hide schemas by default
        c.DisplayRequestDuration(); // Show request duration in Swagger UI
    });
}

app.MapDefaultEndpoints();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

// Map Controllers
app.MapControllers();

app.Run();

/// <summary>
/// Global exception handler for better error responses
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred");

        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "An error occurred while processing your request",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status ?? 500;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
