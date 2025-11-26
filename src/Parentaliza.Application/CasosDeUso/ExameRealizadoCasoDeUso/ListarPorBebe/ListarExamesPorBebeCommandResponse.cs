namespace Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.ListarPorBebe;

public class ListarExamesPorBebeCommandResponse
{
    public Guid ExameSusId { get; private set; }
    public string? NomeExame { get; private set; }
    public string? CategoriaFaixaEtaria { get; private set; }
    public int? IdadeMinMeses { get; private set; }
    public int? IdadeMaxMeses { get; private set; }
    public bool Realizado { get; private set; }
    public DateTime? DataRealizacao { get; private set; }
    public string? Observacoes { get; private set; }

    public ListarExamesPorBebeCommandResponse(
        Guid exameSusId,
        string? nomeExame,
        string? categoriaFaixaEtaria,
        int? idadeMinMeses,
        int? idadeMaxMeses,
        bool realizado,
        DateTime? dataRealizacao,
        string? observacoes)
    {
        ExameSusId = exameSusId;
        NomeExame = nomeExame;
        CategoriaFaixaEtaria = categoriaFaixaEtaria;
        IdadeMinMeses = idadeMinMeses;
        IdadeMaxMeses = idadeMaxMeses;
        Realizado = realizado;
        DataRealizacao = dataRealizacao;
        Observacoes = observacoes;
    }
}