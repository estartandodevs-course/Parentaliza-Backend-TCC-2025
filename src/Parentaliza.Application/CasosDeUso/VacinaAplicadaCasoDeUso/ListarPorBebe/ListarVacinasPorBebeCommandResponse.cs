namespace Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.ListarPorBebe;

public class ListarVacinasPorBebeCommandResponse
{
    public Guid VacinaSusId { get; private set; }
    public string? NomeVacina { get; private set; }
    public string? Descricao { get; private set; }
    public string? CategoriaFaixaEtaria { get; private set; }
    public int? IdadeMinMeses { get; private set; }
    public int? IdadeMaxMeses { get; private set; }
    public bool Aplicada { get; private set; }
    public DateTime? DataAplicacao { get; private set; }
    public string? Lote { get; private set; }
    public string? LocalAplicacao { get; private set; }
    public string? Observacoes { get; private set; }

    public ListarVacinasPorBebeCommandResponse(
        Guid vacinaSusId,
        string? nomeVacina,
        string? descricao,
        string? categoriaFaixaEtaria,
        int? idadeMinMeses,
        int? idadeMaxMeses,
        bool aplicada,
        DateTime? dataAplicacao,
        string? lote,
        string? localAplicacao,
        string? observacoes)
    {
        VacinaSusId = vacinaSusId;
        NomeVacina = nomeVacina;
        Descricao = descricao;
        CategoriaFaixaEtaria = categoriaFaixaEtaria;
        IdadeMinMeses = idadeMinMeses;
        IdadeMaxMeses = idadeMaxMeses;
        Aplicada = aplicada;
        DataAplicacao = dataAplicacao;
        Lote = lote;
        LocalAplicacao = localAplicacao;
        Observacoes = observacoes;
    }
}

