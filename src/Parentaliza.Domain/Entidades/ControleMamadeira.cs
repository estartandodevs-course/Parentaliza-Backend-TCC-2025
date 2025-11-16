namespace Parentaliza.Domain.Entidades;

public class ControleMamadeira : Entity
{
    public DateTime Data { get; set; }
    public DateTime Hora { get; set; }
    public decimal? QuantidadeLeite { get; set; }
    public string? Anotacao { get; set; }
    public ControleMamadeira(DateTime data, DateTime hora, decimal? quantidadeLeite, string? anotacoes)
    {
        Data = data;
        Hora = hora;
        QuantidadeLeite = quantidadeLeite;
        Anotacao = anotacoes;
    }
}