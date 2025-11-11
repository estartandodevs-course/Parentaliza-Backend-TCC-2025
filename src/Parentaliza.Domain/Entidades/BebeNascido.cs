using Parentaliza.Domain.Enums;

namespace Parentaliza.Domain.Entidades;

public class BebeNascido : Entity
{
    public string? Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public SexoEnum Sexo { get; set; }
    public Guid ResponsavelId { get; set; }
    public Responsavel? Responsavel { get; set; }
    public TipoSanguineoEnum TipoSanguineo { get; set; }
    public int IdadeMeses
    {
        get
        {
            var now = DateTime.UtcNow.Date;
            var dob = DataNascimento.Date;
            int months = (now.Year - dob.Year) * 12 + now.Month - dob.Month;
            if (now.Day < dob.Day) months--;
            return Math.Max(0, months);
        }
    }
    public decimal Peso { get; set; }
    public decimal Altura { get; set; }

    public BebeNascido() { }

    public BebeNascido(string? nome, DateTime dataNascimento, SexoEnum sexo, TipoSanguineoEnum tipoSanguineo, Guid responsavelId)
    {
        if (responsavelId == Guid.Empty) throw new ArgumentException("ResponsavelId inválido.", nameof(responsavelId));
        if (dataNascimento > DateTime.UtcNow) throw new ArgumentException("Data de nascimento não pode ser no futuro.", nameof(dataNascimento));
        if (!Enum.IsDefined(typeof(SexoEnum), sexo)) throw new ArgumentException("Sexo inválido.", nameof(sexo));
        if (!Enum.IsDefined(typeof(TipoSanguineoEnum), tipoSanguineo)) throw new ArgumentException("Tipo sanguíneo inválido.", nameof(tipoSanguineo));

        Id = Guid.NewGuid();
        Nome = nome;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        TipoSanguineo = tipoSanguineo;
        ResponsavelId = responsavelId;
    }
}