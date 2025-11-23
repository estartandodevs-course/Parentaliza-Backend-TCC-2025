namespace Parentaliza.Domain.Entidades;
public class ExameSus : Entity
{
    public string? NomeExame { get; private set; }
    public string? CategoriaFaixaEtaria { get; private set; }
    public string? IdadeMinMesesExame { get; private set; }
    public string? IdadeMaxMesesExame { get; private set; }
    public ExameSus(string? nomeExame, string? categoriaFaixaEtaria, string? idadeMinMesesExame, string? idadeMaxMesesExame)
    {
        NomeExame = nomeExame;
        CategoriaFaixaEtaria = categoriaFaixaEtaria;
        IdadeMinMesesExame = idadeMinMesesExame;
        IdadeMaxMesesExame = idadeMaxMesesExame;
    }
}