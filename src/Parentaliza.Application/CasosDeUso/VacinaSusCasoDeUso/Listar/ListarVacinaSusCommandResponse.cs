namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Listar;

public class ListarVacinaSusCommandResponse
{
    public Guid Id { get; private set; }
    public string? NomeVacina { get; private set; }
    public string? Descricao { get; private set; }
    public string? CategoriaFaixaEtaria { get; private set; }
    public int? IdadeMinMesesVacina { get; private set; }
    public int? IdadeMaxMesesVacina { get; private set; }

    public ListarVacinaSusCommandResponse(Guid id, string? nomeVacina, string? descricao, string? categoriaFaixaEtaria, int? idadeMinMesesVacina, int? idadeMaxMesesVacina)
    {
        Id = id;
        NomeVacina = nomeVacina;
        Descricao = descricao;
        CategoriaFaixaEtaria = categoriaFaixaEtaria;
        IdadeMinMesesVacina = idadeMinMesesVacina;
        IdadeMaxMesesVacina = idadeMaxMesesVacina;
    }
}