namespace Parentaliza.Domain.Entidades;

public class BebeGestacao : Entity
{
    public string? Nome { get; set; }
    public Responsavel? Responsavel { get; set; }
    public DateTime DataPrevista { get; set; }
    public int DiasDeGestacao => (int)(DateTime.Now - DataPrevista.AddDays(-280)).TotalDays;
    public decimal PesoEstimado { get; set; }
    public decimal ComprimentoEstimado { get; set; }

    public BebeGestacao(string nome, Responsavel? responsavel, DateTime dataPrevista, decimal pesoEstimado, decimal comprimentoEstimado)
    {
        Nome = nome;
        Responsavel = responsavel;
        DataPrevista = dataPrevista;
        PesoEstimado = pesoEstimado;
        ComprimentoEstimado = comprimentoEstimado;
    }
}