namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Obter;

public class ObterBebeGestacaoCommandResponse
{
    public Guid Id { get; private set; }
    public string? Nome { get; private set; }
    public DateTime DataPrevista { get; private set; }
    public int DiasDeGestacao { get; private set; }
    public decimal PesoEstimado { get; private set; }
    public decimal ComprimentoEstimado { get; private set; }

    public ObterBebeGestacaoCommandResponse(Guid id, string? nome, DateTime dataPrevista, int diasDeGestacao, decimal pesoEstimado, decimal comprimentoEstimado)
    {
        Id = id;
        Nome = nome;
        DataPrevista = dataPrevista;
        DiasDeGestacao = diasDeGestacao;
        PesoEstimado = pesoEstimado;
        ComprimentoEstimado = comprimentoEstimado;
    }
}