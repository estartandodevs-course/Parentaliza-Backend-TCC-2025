using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Parentaliza.Infrastructure.Context;
using Parentaliza.Domain.InterfacesRepository;
using Parentaliza.Infrastructure.Repository;
using Parentaliza.ServiceDefaults;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Use a specific MySQL version or get it from configuration
    // AutoDetect can fail during startup in serverless environments
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 21)); // Adjust to your MySQL version
    options.UseMySql(connectionString, serverVersion, options =>
    {
        options.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    });
});

// Add MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Criar.CriarEventoAgendaCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Criar.CriarBebeGestacaoCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Criar.CriarResponsavelCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Criar.CriarExameSusCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Criar.CriarVacinaSusCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.MarcarRealizado.MarcarExameRealizadoCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.MarcarAplicada.MarcarVacinaAplicadaCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Criar.CriarControleFraldaCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Criar.CriarControleLeiteMaternoCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Criar.CriarControleMamadeiraCommand).Assembly);
});

// Register Repositories
builder.Services.AddScoped<IEventoAgendaRepository, TasksEventoAgendaRepository>();
builder.Services.AddScoped<IBebeNascidoRepository, TasksBebeNascidoRepository>();
builder.Services.AddScoped<IBebeGestacaoRepository, TasksBebeGestacaoRepository>();
builder.Services.AddScoped<IConteudoRepository, TasksConteudoRepository>();
builder.Services.AddScoped<IResponsavelRepository, TasksResponsavelRepository>();
builder.Services.AddScoped<IExameSusRepository, TasksExameSusRepository>();
builder.Services.AddScoped<IVacinaSusRepository, TasksVacinaSusRepository>();
builder.Services.AddScoped<IExameRealizadoRepository, TasksExameRealizadoRepository>();
builder.Services.AddScoped<IVacinaAplicadaRepository, TasksVacinaAplicadaRepository>();
builder.Services.AddScoped<IControleFraldaRepository, TasksControleFraldaRepository>();
builder.Services.AddScoped<IControleLeiteMaternoRepository, TasksControleLeiteMaternoRepository>();
builder.Services.AddScoped<IControleMamadeiraRepository, TasksControleMamadeiraRepository>();

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
// Apply pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        if (db.Database.GetPendingMigrations().Any())
        {
            logger.LogInformation("Applying pending migrations...");
            db.Database.Migrate();
            logger.LogInformation("Migrations applied successfully.");
        }
        else
        {
            logger.LogInformation("No pending migrations. Ensuring database is created...");
            db.Database.EnsureCreated();
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error applying migrations or ensuring database is created. " +
                           "The application will continue but database operations may fail.");
        // Don't throw - allow the app to start even if migrations fail
        // This is important for Lambda cold starts
    }
}

// Configure the HTTP request pipeline
app.UseExceptionHandler();

// Configure Swagger (before MapDefaultEndpoints to avoid conflicts)
// habilitando swagger para producao e desenvolvimento
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Em desenvolvimento, usa o caminho padrão; em produção, usa o configurado
    var jsonPath = app.Environment.IsDevelopment() 
        ? "/swagger/v1/swagger.json" 
        : builder.Configuration["Swagger:JsonPath"] ?? "/swagger/v1/swagger.json";
    
    var routePrefix = app.Environment.IsDevelopment() 
        ? "swagger" 
        : "api/swagger";
    
    c.SwaggerEndpoint(jsonPath, "Parentaliza API V1");
    c.RoutePrefix = routePrefix;
    c.DocumentTitle = "Parentaliza API Documentation";
    c.DefaultModelsExpandDepth(-1);
    c.DisplayRequestDuration();
});

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

