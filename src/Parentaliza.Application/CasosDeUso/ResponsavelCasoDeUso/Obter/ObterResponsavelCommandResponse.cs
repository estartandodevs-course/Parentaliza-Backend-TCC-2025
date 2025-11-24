using Parentaliza.Domain.Enums;

namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Obter;

public class ObterResponsavelCommandResponse
{
    public Guid Id { get; private set; }
    public string? Nome { get; private set; }
    public string? Email { get; private set; }
    public TiposEnum TipoResponsavel { get; private set; }
    public string? FaseNascimento { get; private set; }

    public ObterResponsavelCommandResponse(Guid id, string? nome, string? email, TiposEnum tipoResponsavel, string? faseNascimento)
    {
        Id = id;
        Nome = nome;
        Email = email;
        TipoResponsavel = tipoResponsavel;
        FaseNascimento = faseNascimento;
    }
}

