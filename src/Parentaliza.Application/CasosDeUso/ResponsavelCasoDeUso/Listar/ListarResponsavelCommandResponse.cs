using Parentaliza.Domain.Enums;

namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Listar;

public class ListarResponsavelCommandResponse
{
    public Guid Id { get; private set; }
    public string? Nome { get; private set; }
    public string? Email { get; private set; }
    public TipoResponsavel TipoResponsavel { get; private set; }
    public string? FaseNascimento { get; private set; }

    public ListarResponsavelCommandResponse(Guid id, string? nome, string? email, TipoResponsavel tipoResponsavel, string? faseNascimento)
    {
        Id = id;
        Nome = nome;
        Email = email;
        TipoResponsavel = tipoResponsavel;
        FaseNascimento = faseNascimento;
    }
}

