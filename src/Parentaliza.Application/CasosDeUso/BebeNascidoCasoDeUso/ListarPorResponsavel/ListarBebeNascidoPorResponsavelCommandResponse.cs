using Parentaliza.Domain.Enums;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.ListarPorResponsavel;

public class ListarBebeNascidoPorResponsavelCommandResponse
{
    public Guid Id { get; private set; }
    public Guid ResponsavelId { get; private set; }
    public string? Nome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public SexoEnum Sexo { get; private set; }
    public TipoSanguineoEnum TipoSanguineo { get; private set; }
    public int IdadeMeses { get; private set; }
    public decimal Peso { get; private set; }
    public decimal Altura { get; private set; }

    public ListarBebeNascidoPorResponsavelCommandResponse(
        Guid id,
        Guid responsavelId,
        string? nome,
        DateTime dataNascimento,
        SexoEnum sexo,
        TipoSanguineoEnum tipoSanguineo,
        int idadeMeses,
        decimal peso,
        decimal altura)
    {
        Id = id;
        ResponsavelId = responsavelId;
        Nome = nome;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        TipoSanguineo = tipoSanguineo;
        IdadeMeses = idadeMeses;
        Peso = peso;
        Altura = altura;
    }
}

