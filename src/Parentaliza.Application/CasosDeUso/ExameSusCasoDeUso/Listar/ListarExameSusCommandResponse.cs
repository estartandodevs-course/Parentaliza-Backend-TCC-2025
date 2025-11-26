namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Listar;

public class ListarExameSusCommandResponse
{
    public Guid Id { get; private set; }
    public string? NomeExame { get; private set; }
    public string? Descricao { get; private set; }
    public string? CategoriaFaixaEtaria { get; private set; }
    public int? IdadeMinMesesExame { get; private set; }
    public int? IdadeMaxMesesExame { get; private set; }

    public ListarExameSusCommandResponse(Guid id, string? nomeExame, string? descricao, string? categoriaFaixaEtaria, int? idadeMinMesesExame, int? idadeMaxMesesExame)
    {
        Id = id;
        NomeExame = nomeExame;
        Descricao = descricao;
        CategoriaFaixaEtaria = categoriaFaixaEtaria;
        IdadeMinMesesExame = idadeMinMesesExame;
        IdadeMaxMesesExame = idadeMaxMesesExame;
    }
}