namespace Parentaliza.Domain.Entidades;

public class VacinaAplicada : Entity
{
    public Guid BebeNascidoId { get; private set; }
    public Guid VacinaSusId { get; private set; }
    public DateTime? DataAplicacao { get; private set; }
    public bool Aplicada { get; private set; }
    public string? Observacoes { get; private set; }
    public string? Lote { get; private set; }
    public string? LocalAplicacao { get; private set; }
    public BebeNascido? BebeNascido { get; private set; }
    public VacinaSus? VacinaSus { get; private set; }

    public VacinaAplicada() { }

    public VacinaAplicada(Guid bebeNascidoId, Guid vacinaSusId, DateTime? dataAplicacao, bool aplicada, string? observacoes, string? lote, string? localAplicacao)
    {
        if (bebeNascidoId == Guid.Empty) throw new ArgumentException("O ID do bebê é obrigatório.", nameof(bebeNascidoId));
        if (vacinaSusId == Guid.Empty) throw new ArgumentException("O ID da vacina SUS é obrigatório.", nameof(vacinaSusId));
        if (aplicada && dataAplicacao == null) throw new ArgumentException("A data de aplicação é obrigatória quando a vacina foi aplicada.", nameof(dataAplicacao));
        if (dataAplicacao.HasValue && dataAplicacao.Value > DateTime.UtcNow) throw new ArgumentException("A data de aplicação não pode ser no futuro.", nameof(dataAplicacao));

        BebeNascidoId = bebeNascidoId;
        VacinaSusId = vacinaSusId;
        DataAplicacao = dataAplicacao;
        Aplicada = aplicada;
        Observacoes = observacoes;
        Lote = lote;
        LocalAplicacao = localAplicacao;
    }

    public void MarcarComoAplicada(DateTime dataAplicacao, string? lote = null, string? localAplicacao = null, string? observacoes = null)
    {
        if (dataAplicacao > DateTime.UtcNow) throw new ArgumentException("A data de aplicação não pode ser no futuro.", nameof(dataAplicacao));

        Aplicada = true;
        DataAplicacao = dataAplicacao;
        Lote = lote;
        LocalAplicacao = localAplicacao;
        Observacoes = observacoes;
    }

    public void MarcarComoNaoAplicada()
    {
        Aplicada = false;
        DataAplicacao = null;
        Lote = null;
        LocalAplicacao = null;
    }
}