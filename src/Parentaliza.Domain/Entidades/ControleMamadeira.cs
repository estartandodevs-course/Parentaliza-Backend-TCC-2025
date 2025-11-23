namespace Parentaliza.Domain.Entidades;

public class ControleMamadeira : Entity
{
    public DateTime Data { get; private set; }
    public TimeSpan Hora { get; private set; }
    public decimal? QuantidadeLeite { get; private set; }
    public string? Anotacao { get; private set; }
    public ControleMamadeira(DateTime data, TimeSpan hora, decimal? quantidadeLeite, string? anotacao)
    {
        Data = data;
        Hora = hora;
        QuantidadeLeite = quantidadeLeite;
        Anotacao = anotacao;
    }
}