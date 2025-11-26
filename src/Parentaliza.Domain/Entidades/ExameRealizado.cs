namespace Parentaliza.Domain.Entidades;

public class ExameRealizado : Entity
{
    public Guid BebeNascidoId { get; private set; }
    public Guid ExameSusId { get; private set; }
    public DateTime? DataRealizacao { get; private set; }
    public bool Realizado { get; private set; }
    public string? Observacoes { get; private set; }
    public BebeNascido? BebeNascido { get; private set; }
    public ExameSus? ExameSus { get; private set; }

    public ExameRealizado() { }

    public ExameRealizado(Guid bebeNascidoId, Guid exameSusId, DateTime? dataRealizacao, bool realizado, string? observacoes)
    {
        if (bebeNascidoId == Guid.Empty) throw new ArgumentException("O ID do bebê é obrigatório.", nameof(bebeNascidoId));
        if (exameSusId == Guid.Empty) throw new ArgumentException("O ID do exame SUS é obrigatório.", nameof(exameSusId));
        if (realizado && dataRealizacao == null) throw new ArgumentException("A data de realização é obrigatória quando o exame foi realizado.", nameof(dataRealizacao));
        if (dataRealizacao.HasValue && dataRealizacao.Value > DateTime.UtcNow) throw new ArgumentException("A data de realização não pode ser no futuro.", nameof(dataRealizacao));

        BebeNascidoId = bebeNascidoId;
        ExameSusId = exameSusId;
        DataRealizacao = dataRealizacao;
        Realizado = realizado;
        Observacoes = observacoes;
    }

    public void MarcarComoRealizado(DateTime dataRealizacao, string? observacoes = null)
    {
        if (dataRealizacao > DateTime.UtcNow) throw new ArgumentException("A data de realização não pode ser no futuro.", nameof(dataRealizacao));

        Realizado = true;
        DataRealizacao = dataRealizacao;
        Observacoes = observacoes;
    }

    public void MarcarComoNaoRealizado()
    {
        Realizado = false;
        DataRealizacao = null;
    }
}