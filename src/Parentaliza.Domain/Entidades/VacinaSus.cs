namespace Parentaliza.Domain.Entidades;
public class VacinaSus : Entity
{
    public string? NomeVacina { get; private set; }
    public string? Descricao { get; private set; }
    public string? CategoriaFaixaEtaria { get; private set; }
    public string? IdadeMinMeses { get; private set; }
    public string? IdadeMaxMeses { get; private set; }
    public VacinaSus(string? nomeVacina, string? descricao, string? categoriaFaixa, string? idadeMinMeses, string? idadeMaxMeses)
    {
        NomeVacina = nomeVacina;
        Descricao = descricao;
        CategoriaFaixaEtaria = categoriaFaixa;
        IdadeMinMeses = idadeMinMeses;
        IdadeMaxMeses = idadeMaxMeses;
    }
}