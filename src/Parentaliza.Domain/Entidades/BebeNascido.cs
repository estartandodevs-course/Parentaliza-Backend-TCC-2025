using Parentaliza.Domain.Enums;

namespace Parentaliza.Domain.Entidades;

public class BebeNascido : Entity
{
    public string? Nome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public SexoEnum Sexo { get; private set; }
    public TipoSanguineoEnum TipoSanguineo { get; private set; }
    public int IdadeMeses { get; private set; }
    public decimal Peso { get; private set; }
    public decimal Altura { get; private set; }

    public Responsavel? Responsavel { get; private set; }

    public BebeNascido() { }

    public BebeNascido(string? nome,
                       DateTime dataNascimento,
                       SexoEnum sexo,
                       TipoSanguineoEnum tipoSanguineo,
                       int idadeMeses,
                       decimal peso,
                       decimal altura)
    {
        if (dataNascimento > DateTime.UtcNow) throw new ArgumentException("Data de nascimento não pode ser no futuro.", nameof(dataNascimento));
        if (!Enum.IsDefined(typeof(SexoEnum), sexo)) throw new ArgumentException("Sexo inválido.", nameof(sexo));
        if (!Enum.IsDefined(typeof(TipoSanguineoEnum), tipoSanguineo)) throw new ArgumentException("Tipo sanguíneo inválido.", nameof(tipoSanguineo));

        Nome = nome;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        TipoSanguineo = tipoSanguineo;
        IdadeMeses = idadeMeses;
        Peso = peso;
        Altura = altura;
    }
}