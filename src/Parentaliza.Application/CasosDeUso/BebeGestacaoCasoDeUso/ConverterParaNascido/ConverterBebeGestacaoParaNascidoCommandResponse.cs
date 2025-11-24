namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.ConverterParaNascido;

public class ConverterBebeGestacaoParaNascidoCommandResponse
{
    public Guid BebeNascidoId { get; private set; }
    public Guid BebeGestacaoId { get; private set; }
    public bool BebeGestacaoExcluido { get; private set; }

    public ConverterBebeGestacaoParaNascidoCommandResponse(
        Guid bebeNascidoId,
        Guid bebeGestacaoId,
        bool bebeGestacaoExcluido)
    {
        BebeNascidoId = bebeNascidoId;
        BebeGestacaoId = bebeGestacaoId;
        BebeGestacaoExcluido = bebeGestacaoExcluido;
    }
}

