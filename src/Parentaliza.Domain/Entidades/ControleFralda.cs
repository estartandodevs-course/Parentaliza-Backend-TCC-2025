namespace Parentaliza.Domain.Entidades;

public class ControleFralda : Entity
{
    public DateTime HoraTroca { get; private set; }
    public string? TipoFralda { get; private set; }
    public string? Observacoes { get; private set; }
    public ControleFralda(DateTime horaTroca, string? tipoFralda, string? observacoes)
    {
        HoraTroca = horaTroca;
        TipoFralda = tipoFralda;
        Observacoes = observacoes;
    }
}