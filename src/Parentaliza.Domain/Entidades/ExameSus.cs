namespace Parentaliza.Domain.Entidades;
public class ExameSus : Entity
{
    public string? NomeExame { get; private set; }
    public string? CategoriaFaixaEtaria { get; private set; }
    public string? IdadeMinMeses { get; private set; }
    public string? IdadeMaxMeses { get; private set; }
    public ExameSus(string? nomeExame, string? categoriaFaixaEtaria, string? idadeMinMeses, string? idadeMaxMeses)
    {
        NomeExame = nomeExame;
        CategoriaFaixaEtaria = categoriaFaixaEtaria;
        IdadeMinMeses = idadeMinMeses;
        IdadeMaxMeses = idadeMaxMeses;
    }
}