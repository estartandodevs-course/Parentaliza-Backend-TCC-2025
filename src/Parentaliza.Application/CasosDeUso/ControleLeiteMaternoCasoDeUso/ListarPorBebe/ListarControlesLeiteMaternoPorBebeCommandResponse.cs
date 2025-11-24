namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.ListarPorBebe;

public class ListarControlesLeiteMaternoPorBebeCommandResponse
{
    public Guid Id { get; private set; }
    public Guid BebeNascidoId { get; private set; }
    public DateTime Cronometro { get; private set; }
    public string? LadoDireito { get; private set; }
    public string? LadoEsquerdo { get; private set; }

    public ListarControlesLeiteMaternoPorBebeCommandResponse(Guid id, Guid bebeNascidoId, DateTime cronometro, string? ladoDireito, string? ladoEsquerdo)
    {
        Id = id;
        BebeNascidoId = bebeNascidoId;
        Cronometro = cronometro;
        LadoDireito = ladoDireito;
        LadoEsquerdo = ladoEsquerdo;
    }
}

