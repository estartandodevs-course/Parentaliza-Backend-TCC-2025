namespace Parentaliza.Domain.Entidades;

public class ControleLeiteMaterno
{
    public DateTime Cronometro { get; set; }
    public string? LadoDireito { get; set; }
    public string? LadoEsquerdo { get; set; }
    public ControleLeiteMaterno(DateTime cronometro, string? ladoDireito, string? ladoEsquerdo)
    {
        Cronometro = cronometro;
        LadoDireito = ladoDireito;
        LadoEsquerdo = ladoEsquerdo;
    }
}
