namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.ListarPorResponsavel;

public class ListarBebeGestacaoPorResponsavelCommandResponse
{
    public Guid Id { get; private set; }
    public Guid ResponsavelId { get; private set; }
    public string? Nome { get; private set; }
    public DateTime DataPrevista { get; private set; }
    public int DiasDeGestacao { get; private set; }
    public decimal PesoEstimado { get; private set; }
    public decimal ComprimentoEstimado { get; private set; }

    public ListarBebeGestacaoPorResponsavelCommandResponse(
        Guid id,
        Guid responsavelId,
        string? nome,
        DateTime dataPrevista,
        int diasDeGestacao,
        decimal pesoEstimado,
        decimal comprimentoEstimado)
    {
        Id = id;
        ResponsavelId = responsavelId;
        Nome = nome;
        DataPrevista = dataPrevista;
        DiasDeGestacao = diasDeGestacao;
        PesoEstimado = pesoEstimado;
        ComprimentoEstimado = comprimentoEstimado;
    }
}

