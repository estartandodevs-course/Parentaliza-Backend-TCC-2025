namespace Parentaliza.Domain.Entidades;
public class VacinaSus : Entity
{
    public string? NomeVacina { get; set; }
    public string? Descricao { get; set; }
    public string? CategoriaFaixaEtaria { get; set; }
    public string? IdadeMinMeses { get; set; }
    public string? IdadeMaxMeses { get; set; }
    public VacinaSus(string? nomeVacina,  string? descricao, string? categoriaFaixa, string? idadeMinMeses, string? idadeMaxMeses)
    {
        NomeVacina = nomeVacina;
        Descricao = descricao;
        CategoriaFaixaEtaria = categoriaFaixa;
        IdadeMinMeses = idadeMinMeses;
        IdadeMaxMeses = idadeMaxMeses;
    }
}