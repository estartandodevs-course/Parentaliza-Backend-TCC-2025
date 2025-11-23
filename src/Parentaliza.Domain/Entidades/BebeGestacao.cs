namespace Parentaliza.Domain.Entidades;

public class BebeGestacao : Entity
{
    public string? Nome { get; private set; }
    public DateTime DataPrevista { get; private set; }
    public int DiasDeGestacao { get; private set; }
    public decimal PesoEstimado { get; private set; }
    public decimal ComprimentoEstimado { get; private set; }

    public Responsavel? Responsavel { get; private set; }

    public BebeGestacao() { }

    public BebeGestacao(string? nome,
                        DateTime dataPrevista,
                        int diasDeGestacao,
                        decimal pesoEstimado,
                        decimal comprimentoEstimado)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome do bebê não pode ser vazio.", nameof(nome));
        if (dataPrevista.Date < DateTime.Today) throw new ArgumentException("Data prevista não pode ser no passado.", nameof(dataPrevista));
        if (pesoEstimado < 0) throw new ArgumentOutOfRangeException(nameof(pesoEstimado), "Peso estimado não pode ser negativo.");
        if (comprimentoEstimado < 0) throw new ArgumentOutOfRangeException(nameof(pesoEstimado), "Comprimento estimado não pode ser negativo.");

        Nome = nome;
        DataPrevista = dataPrevista;
        DiasDeGestacao = diasDeGestacao;
        PesoEstimado = pesoEstimado;
        ComprimentoEstimado = comprimentoEstimado;
    }
}