using Parentaliza.Domain.Enums;

namespace Parentaliza.Domain.Entidades;

public class BebeGestacao : Entity
{
    public string? Nome { get; set; }
    public DateTime DataPrevista { get; set; }
    public int DiasDeGestacao => (int)(DateTime.Now - DataPrevista.AddDays(-280)).TotalDays;
    public decimal PesoEstimado { get; set; }
    public decimal ComprimentoEstimado { get; set; }
    public BebeGestacao(string nome, DateTime dataPrevista, decimal pesoEstimado, decimal comprimentoEstimado)
    { 
        Nome = nome;
        DataPrevista = dataPrevista;
        PesoEstimado = pesoEstimado;
        ComprimentoEstimado = comprimentoEstimado;
    }
}