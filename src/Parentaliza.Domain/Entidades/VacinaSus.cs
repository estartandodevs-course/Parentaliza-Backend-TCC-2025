namespace Parentaliza.Domain.Entidades;
public class VacinaSus : Entity
{
    public string? NomeVacina { get; private set; }
    public string? Descricao { get; private set; }
    public string? CategoriaFaixaEtaria { get; private set; }
    public int? IdadeMinMesesVacina { get; private set; }
    public int? IdadeMaxMesesVacina { get; private set; }

    public VacinaSus() { }

    public VacinaSus(string? nomeVacina, string? descricao, string? categoriaFaixaEtaria, int? idadeMinMesesVacina, int? idadeMaxMesesVacina)
    {
        if (string.IsNullOrWhiteSpace(nomeVacina)) throw new ArgumentException("O nome da vacina é obrigatório.", nameof(nomeVacina));
        if (idadeMinMesesVacina.HasValue && idadeMinMesesVacina.Value < 0) throw new ArgumentOutOfRangeException(nameof(idadeMinMesesVacina), "A idade mínima não pode ser negativa.");
        if (idadeMaxMesesVacina.HasValue && idadeMaxMesesVacina.Value < 0) throw new ArgumentOutOfRangeException(nameof(idadeMaxMesesVacina), "A idade máxima não pode ser negativa.");
        if (idadeMinMesesVacina.HasValue && idadeMaxMesesVacina.HasValue && idadeMinMesesVacina.Value > idadeMaxMesesVacina.Value) 
            throw new ArgumentException("A idade mínima não pode ser maior que a idade máxima.", nameof(idadeMinMesesVacina));

        NomeVacina = nomeVacina;
        Descricao = descricao;
        CategoriaFaixaEtaria = categoriaFaixaEtaria;
        IdadeMinMesesVacina = idadeMinMesesVacina;
        IdadeMaxMesesVacina = idadeMaxMesesVacina;
    }
}