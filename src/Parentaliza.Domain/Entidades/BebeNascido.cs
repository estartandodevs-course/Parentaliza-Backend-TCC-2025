using Parentaliza.Domain.Enums;

namespace Parentaliza.Domain.Entidades;

public class BebeNascido : Entity
{
    public Guid ResponsavelId { get; private set; }
    public string? Nome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public Sexo Sexo { get; private set; }
    public TipoSanguineo TipoSanguineo { get; private set; }
    public int IdadeMeses { get; private set; }
    public decimal Peso { get; private set; }
    public decimal Altura { get; private set; }
    public Responsavel? Responsavel { get; private set; }

    public BebeNascido() { }

    public BebeNascido(Guid responsavelId,
                       string? nome,
                       DateTime dataNascimento,
                       Sexo sexo,
                       TipoSanguineo tipoSanguineo,
                       int idadeMeses,
                       decimal peso,
                       decimal altura)
    {
        if (responsavelId == Guid.Empty) throw new ArgumentException("O ID do responsável é obrigatório.", nameof(responsavelId));
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome é obrigatório.", nameof(nome));
        if (dataNascimento > DateTime.UtcNow) throw new ArgumentException("Data de nascimento não pode ser no futuro.", nameof(dataNascimento));
        if (!Enum.IsDefined(typeof(Sexo), sexo)) throw new ArgumentException("Sexo inválido.", nameof(sexo));
        if (!Enum.IsDefined(typeof(TipoSanguineo), tipoSanguineo)) throw new ArgumentException("Tipo sanguíneo inválido.", nameof(tipoSanguineo));
        if (idadeMeses < 0) throw new ArgumentOutOfRangeException(nameof(idadeMeses), "A idade em meses não pode ser negativa.");
        if (peso <= 0) throw new ArgumentOutOfRangeException(nameof(peso), "O peso deve ser maior que zero.");
        if (altura <= 0) throw new ArgumentOutOfRangeException(nameof(altura), "A altura deve ser maior que zero.");

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