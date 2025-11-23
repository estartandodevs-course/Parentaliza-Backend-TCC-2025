namespace Parentaliza.Domain.Entidades;

public class ControleLeiteMaterno : Entity
{
    public DateTime Cronometro { get; private set; }
    public string? LadoDireito { get; private set; }
    public string? LadoEsquerdo { get; private set; }
    public ControleLeiteMaterno(DateTime cronometro, string? ladoDireito, string? ladoEsquerdo)
    {
        Cronometro = cronometro;
        LadoDireito = ladoDireito;
        LadoEsquerdo = ladoEsquerdo;
    }
}
