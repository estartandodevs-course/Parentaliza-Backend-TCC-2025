namespace Parentaliza.Domain.Entidades;

public class ControleFralda : Entity
{
    public DateTime HoraTroca { get; set; }
    public string? TipoFralda { get; set; }
    public string? Observacoes { get; set; }
    public ControleFralda(DateTime horaTroca, string? tipoFralda, string? observacoes)
    {
        HoraTroca = horaTroca;
        TipoFralda = tipoFralda;
        Observacoes = observacoes;
    }
}