namespace Parentaliza.Domain.Entidades;
public class ExameSus : Entity
{
    public string? NomeExame { get; set; }
    public string? CategoriaFaixaEtaria { get; set; }
    public string? IdadeMinMeses { get; set; }
    public string? IdadeMaxMeses { get; set; }
    public ExameSus(string? nomeExame, string? categoriaFaixa, string? idadeMinMeses, string? idadeMaxMeses)
    {
        NomeExame = nomeExame;
        CategoriaFaixaEtaria = categoriaFaixa;
        IdadeMinMeses = idadeMinMeses;
        IdadeMaxMeses = idadeMaxMeses;
    }
}