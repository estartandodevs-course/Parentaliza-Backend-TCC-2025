namespace Parentaliza.Domain.Entidades;
public class ExameSus : Entity
{
    public string? NomeExame { get; private set; }
    public string? Descricao { get; private set; }
    public string? CategoriaFaixaEtaria { get; private set; }
    public int? IdadeMinMesesExame { get; private set; }
    public int? IdadeMaxMesesExame { get; private set; }

    public ExameSus() { }

    public ExameSus(string? nomeExame, string? descricao, string? categoriaFaixaEtaria, int? idadeMinMesesExame, int? idadeMaxMesesExame)
    {
        if (string.IsNullOrWhiteSpace(nomeExame)) throw new ArgumentException("O nome do exame é obrigatório.", nameof(nomeExame));
        if (idadeMinMesesExame.HasValue && idadeMinMesesExame.Value < 0) throw new ArgumentOutOfRangeException(nameof(idadeMinMesesExame), "A idade mínima não pode ser negativa.");
        if (idadeMaxMesesExame.HasValue && idadeMaxMesesExame.Value < 0) throw new ArgumentOutOfRangeException(nameof(idadeMaxMesesExame), "A idade máxima não pode ser negativa.");
        if (idadeMinMesesExame.HasValue && idadeMaxMesesExame.HasValue && idadeMinMesesExame.Value > idadeMaxMesesExame.Value) 
            throw new ArgumentException("A idade mínima não pode ser maior que a idade máxima.", nameof(idadeMinMesesExame));

        NomeExame = nomeExame;
        Descricao = descricao;
        CategoriaFaixaEtaria = categoriaFaixaEtaria;
        IdadeMinMesesExame = idadeMinMesesExame;
        IdadeMaxMesesExame = idadeMaxMesesExame;
    }
}