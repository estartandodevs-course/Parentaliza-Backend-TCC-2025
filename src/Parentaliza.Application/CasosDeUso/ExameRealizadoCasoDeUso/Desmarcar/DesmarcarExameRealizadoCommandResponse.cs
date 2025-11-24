namespace Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.Desmarcar;

public class DesmarcarExameRealizadoCommandResponse
{
    public Guid Id { get; private set; }

    public DesmarcarExameRealizadoCommandResponse(Guid id)
    {
        Id = id;
    }
}

