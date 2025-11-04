using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parentaliza.API.Infrastructure;

namespace Parentaliza.API.Controllers;

/// <summary>
/// Controller for health checks and database connectivity verification
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;
    private readonly ApplicationDbContext _dbContext;

    public HealthController(ILogger<HealthController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Verifica se a conexão com o banco de dados está OK
    /// </summary>
    [HttpGet("database")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> CheckDatabase(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Verificando conexão com o banco de dados...");

            // Tenta abrir a conexão e executar uma query simples
            var canConnect = await _dbContext.Database.CanConnectAsync(cancellationToken);
            
            if (!canConnect)
            {
                _logger.LogWarning("Não foi possível conectar ao banco de dados");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new
                {
                    status = "unavailable",
                    message = "Não foi possível conectar ao banco de dados",
                    timestamp = DateTime.UtcNow
                });
            }

            // Executa uma query simples para garantir que a conexão está funcionando
            await _dbContext.Database.ExecuteSqlRawAsync("SELECT 1", cancellationToken);

            _logger.LogInformation("Conexão com o banco de dados verificada com sucesso");

            return Ok(new
            {
                status = "ok",
                message = "Conexão com o banco de dados está OK",
                database = _dbContext.Database.GetDbConnection().Database,
                server = _dbContext.Database.GetDbConnection().DataSource,
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao verificar conexão com o banco de dados");
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                status = "error",
                message = "Erro ao verificar conexão com o banco de dados",
                error = ex.Message,
                timestamp = DateTime.UtcNow
            });
        }
    }
}

