namespace Parentaliza.Domain.Entidades;

public class ControleFralda : Entity
{
    public Guid BebeNascidoId { get; private set; }
    public DateTime HoraTroca { get; private set; }
    public string? TipoFralda { get; private set; }
    public string? Observacoes { get; private set; }
    public BebeNascido? BebeNascido { get; private set; }

    public ControleFralda() { }

    public ControleFralda(Guid bebeNascidoId, DateTime horaTroca, string? tipoFralda, string? observacoes)
    {
        if (bebeNascidoId == Guid.Empty) throw new ArgumentException("O ID do bebê é obrigatório.", nameof(bebeNascidoId));
        if (horaTroca > DateTime.UtcNow) throw new ArgumentException("A hora da troca não pode ser no futuro.", nameof(horaTroca));

        BebeNascidoId = bebeNascidoId;
        HoraTroca = horaTroca;
        TipoFralda = tipoFralda;
        Observacoes = observacoes;
    }
}