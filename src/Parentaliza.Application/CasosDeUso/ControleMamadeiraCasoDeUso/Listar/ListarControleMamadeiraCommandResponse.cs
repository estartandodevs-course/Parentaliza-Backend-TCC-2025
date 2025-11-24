namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Listar;

public class ListarControleMamadeiraCommandResponse
{
    public Guid Id { get; private set; }
    public Guid BebeNascidoId { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Hora { get; private set; }
    public decimal? QuantidadeLeite { get; private set; }
    public string? Anotacao { get; private set; }

    public ListarControleMamadeiraCommandResponse(Guid id, Guid bebeNascidoId, DateTime data, TimeSpan hora, decimal? quantidadeLeite, string? anotacao)
    {
        Id = id;
        BebeNascidoId = bebeNascidoId;
        Data = data;
        Hora = hora;
        QuantidadeLeite = quantidadeLeite;
        Anotacao = anotacao;
    }
}

