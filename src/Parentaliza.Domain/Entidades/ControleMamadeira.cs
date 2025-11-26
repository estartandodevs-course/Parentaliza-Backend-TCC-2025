namespace Parentaliza.Domain.Entidades;

public class ControleMamadeira : Entity
{
    public Guid BebeNascidoId { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Hora { get; private set; }
    public decimal? QuantidadeLeite { get; private set; }
    public string? Anotacao { get; private set; }
    public BebeNascido? BebeNascido { get; private set; }

    public ControleMamadeira() { }

    public ControleMamadeira(Guid bebeNascidoId, DateTime data, TimeSpan hora, decimal? quantidadeLeite, string? anotacao)
    {
        if (bebeNascidoId == Guid.Empty) throw new ArgumentException("O ID do bebê é obrigatório.", nameof(bebeNascidoId));
        if (data > DateTime.UtcNow.Date) throw new ArgumentException("A data não pode ser no futuro.", nameof(data));
        if (quantidadeLeite.HasValue && quantidadeLeite.Value < 0) throw new ArgumentException("A quantidade de leite não pode ser negativa.", nameof(quantidadeLeite));

        BebeNascidoId = bebeNascidoId;
        Data = data;
        Hora = hora;
        QuantidadeLeite = quantidadeLeite;
        Anotacao = anotacao;
    }
}