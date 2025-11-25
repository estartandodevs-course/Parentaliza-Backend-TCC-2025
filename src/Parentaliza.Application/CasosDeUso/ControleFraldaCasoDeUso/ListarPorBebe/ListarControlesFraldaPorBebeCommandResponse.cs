namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.ListarPorBebe;

public class ListarControlesFraldaPorBebeCommandResponse
{
    public Guid Id { get; private set; }
    public Guid BebeNascidoId { get; private set; }
    public DateTime HoraTroca { get; private set; }
    public string? TipoFralda { get; private set; }
    public string? Observacoes { get; private set; }

    public ListarControlesFraldaPorBebeCommandResponse(Guid id, Guid bebeNascidoId, DateTime horaTroca, string? tipoFralda, string? observacoes)
    {
        Id = id;
        BebeNascidoId = bebeNascidoId;
        HoraTroca = horaTroca;
        TipoFralda = tipoFralda;
        Observacoes = observacoes;
    }
}

