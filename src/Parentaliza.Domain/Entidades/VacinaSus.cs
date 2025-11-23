namespace Parentaliza.Domain.Entidades;
public class VacinaSus : Entity
{
    public string? NomeVacina { get; private set; }
    public string? Descricao { get; private set; }
    public string? CategoriaFaixaEtaria { get; private set; }
    public string? IdadeMinMesesVacina { get; private set; }
    public string? IdadeMaxMesesVacina { get; private set; }
    public VacinaSus(string? nomeVacina, string? descricao, string? categoriaFaixaEtaria, string? idadeMinMesesVacina, string? idadeMaxMesesVacina)
    {
        NomeVacina = nomeVacina;
        Descricao = descricao;
        CategoriaFaixaEtaria = categoriaFaixaEtaria;
        IdadeMinMesesVacina = idadeMinMesesVacina;
        IdadeMaxMesesVacina = idadeMaxMesesVacina;
    }
}