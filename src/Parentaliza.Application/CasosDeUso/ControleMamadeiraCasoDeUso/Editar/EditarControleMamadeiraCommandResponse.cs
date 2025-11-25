namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Editar;

public class EditarControleMamadeiraCommandResponse
{
    public Guid Id { get; private set; }

    public EditarControleMamadeiraCommandResponse(Guid id)
    {
        Id = id;
    }
}

