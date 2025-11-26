namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Criar;

public class CriarControleMamadeiraCommandResponse
{
    public Guid Id { get; private set; }

    public CriarControleMamadeiraCommandResponse(Guid id)
    {
        Id = id;
    }
}