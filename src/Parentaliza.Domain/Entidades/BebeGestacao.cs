namespace Parentaliza.Domain.Entidades;

public class BebeGestacao : Entity
{
    public Guid ResponsavelId { get; private set; }
    public string? Nome { get; private set; }
    public DateTime DataPrevista { get; private set; }
    public int DiasDeGestacao { get; private set; }
    public decimal PesoEstimado { get; private set; }
    public decimal ComprimentoEstimado { get; private set; }
    public Responsavel? Responsavel { get; private set; }

    public BebeGestacao() { }

    public BebeGestacao(Guid responsavelId,
                        string? nome,
                        DateTime dataPrevista,
                        int diasDeGestacao,
                        decimal pesoEstimado,
                        decimal comprimentoEstimado)
    {
        if (responsavelId == Guid.Empty) throw new ArgumentException("O ID do responsável é obrigatório.", nameof(responsavelId));
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome do bebê não pode ser vazio.", nameof(nome));
        if (dataPrevista.Date < DateTime.Today) throw new ArgumentException("Data prevista não pode ser no passado.", nameof(dataPrevista));
        if (diasDeGestacao < 0 || diasDeGestacao > 294) throw new ArgumentOutOfRangeException(nameof(diasDeGestacao), "Os dias de gestação devem estar entre 0 e 294 dias (42 semanas).");
        if (pesoEstimado < 0) throw new ArgumentOutOfRangeException(nameof(pesoEstimado), "Peso estimado não pode ser negativo.");
        if (comprimentoEstimado < 0) throw new ArgumentOutOfRangeException(nameof(comprimentoEstimado), "Comprimento estimado não pode ser negativo.");

        ResponsavelId = responsavelId;
        Nome = nome;
        DataPrevista = dataPrevista;
        DiasDeGestacao = diasDeGestacao;
        PesoEstimado = pesoEstimado;
        ComprimentoEstimado = comprimentoEstimado;
    }
}