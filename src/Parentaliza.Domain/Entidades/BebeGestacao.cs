using Parentaliza.Domain.Enums;

namespace Parentaliza.Domain.Entidades;

public class BebeGestacao : BebeNascido 
{
    public DateTime DataPrevista { get; set; }
    public int DiasDeGestacao => (int)(DateTime.Now - DataPrevista.AddDays(-280)).TotalDays;
    public decimal PesoEstimado { get; set; }
    public decimal ComprimentoEstimado { get; set; }

    public BebeGestacao(string? nome, SexoEnum sexo, DateTime dataPrevista, decimal pesoEstimado, decimal comprimentoEstimado)
    { 
        Nome = nome;
        Sexo = sexo;
        DataPrevista = dataPrevista;
        PesoEstimado = pesoEstimado;
        ComprimentoEstimado = comprimentoEstimado;
    }
}