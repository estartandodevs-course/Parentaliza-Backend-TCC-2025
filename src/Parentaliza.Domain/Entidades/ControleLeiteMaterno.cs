namespace Parentaliza.Domain.Entidades;

public class ControleLeiteMaterno : Entity
{
    public Guid BebeNascidoId { get; private set; }
    public DateTime Cronometro { get; private set; }
    public string? LadoDireito { get; private set; }
    public string? LadoEsquerdo { get; private set; }
    public BebeNascido? BebeNascido { get; private set; }

    public ControleLeiteMaterno() { }

    public ControleLeiteMaterno(Guid bebeNascidoId, DateTime cronometro, string? ladoDireito, string? ladoEsquerdo)
    {
        if (bebeNascidoId == Guid.Empty) throw new ArgumentException("O ID do bebê é obrigatório.", nameof(bebeNascidoId));
        if (cronometro > DateTime.UtcNow) throw new ArgumentException("O cronômetro não pode ser no futuro.", nameof(cronometro));

        BebeNascidoId = bebeNascidoId;
        Cronometro = cronometro;
        LadoDireito = ladoDireito;
        LadoEsquerdo = ladoEsquerdo;
    }
}