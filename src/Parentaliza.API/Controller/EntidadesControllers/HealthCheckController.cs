using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parentaliza.API.Controller.Base;
using Parentaliza.Infrastructure.Context;

namespace Parentaliza.API.Controller.EntidadesControllers;

/// <summary>
/// Faz a verifica��o de integridade da API e do banco de dados
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class HealthCheckController : BaseController
{
    private readonly ILogger<HealthCheckController> _logger;
    private readonly ApplicationDbContext _dbContext;

    public HealthCheckController(ILogger<HealthCheckController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Verifica a conex�o com o banco de dados
    /// </summary>
    /// <returns>Status de conex�o</returns>
    [HttpGet("database")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> CheckDatabase(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Checking database connection...");

            // Checa se a conex�o com o banco de dados pode ser estabelecida
            var canConnect = await _dbContext.Database.CanConnectAsync(cancellationToken);

            if (!canConnect)
            {
                _logger.LogWarning("Database connection failed");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new
                {
                    status = "unhealthy",
                    database = "disconnected",
                    timestamp = DateTime.UtcNow,
                    message = "Unable to connect to the database"
                });
            }

            // Executa uma consulta simples para garantir que o banco de dados est� respondendo e obter informa��es adicionais
            await _dbContext.Database.ExecuteSqlRawAsync("SELECT 1", cancellationToken);

            // Obtem informa��es adicionais sobre o banco de dados enquanto a conex�o est� aberta
            var connection = _dbContext.Database.GetDbConnection();
            var connectionString = _dbContext.Database.GetConnectionString();
            string databaseName = connection.Database ?? "unknown";
            string? serverVersion = null;

            try
            {
                // certifica que a conex�o est� aberta para obter a vers�o do servidor
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    await connection.OpenAsync(cancellationToken);
                }
                serverVersion = connection.ServerVersion;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not retrieve server version");
            }

            _logger.LogInformation("Database connection successful");

            return Ok(new
            {
                status = "healthy",
                database = "connected",
                timestamp = DateTime.UtcNow,
                databaseName,
                serverVersion = serverVersion ?? "unknown",
                message = "Database connection is working correctly"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking database connection");

            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                status = "unhealthy",
                database = "error",
                timestamp = DateTime.UtcNow,
                message = ex.Message,
                error = ex.GetType().Name
            });
        }
    }

    /// <summary>
    /// Endpoint geral de verifica��o de integridade da API
    /// </summary>
    /// <returns>Status de integridade da API</returns>
    [HttpGet]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public ActionResult GetHealth()
    {
        return Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow,
            message = "API is running"
        });
    }
}