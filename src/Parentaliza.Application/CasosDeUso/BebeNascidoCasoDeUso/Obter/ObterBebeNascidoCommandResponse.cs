using Parentaliza.Domain.Enums;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Obter;

public class ObterBebeNascidoCommandResponse
{
    public Guid Id { get; private set; }
    public string? Nome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public Sexo Sexo { get; private set; }
    public TipoSanguineo TipoSanguineo { get; private set; }
    public int IdadeMeses { get; private set; }
    public decimal Peso { get; private set; }
    public decimal Altura { get; private set; }

    public ObterBebeNascidoCommandResponse(Guid id, string? nome, DateTime dataNascimento, Sexo sexo, TipoSanguineo tipoSanguineo, int idadeMeses, decimal peso, decimal altura)
    {
        Id = id;
        Nome = nome;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        TipoSanguineo = tipoSanguineo;
        IdadeMeses = idadeMeses;
        Peso = peso;
        Altura = altura;
    }
}