using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.Desmarcar;

public class DesmarcarVacinaAplicadaCommand : IRequest<CommandResponse<DesmarcarVacinaAplicadaCommandResponse>>
{
    public Guid BebeNascidoId { get; private set; }
    public Guid VacinaSusId { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public DesmarcarVacinaAplicadaCommand(Guid bebeNascidoId, Guid vacinaSusId)
    {
        BebeNascidoId = bebeNascidoId;
        VacinaSusId = vacinaSusId;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<DesmarcarVacinaAplicadaCommand>();

        validacoes.RuleFor(command => command.BebeNascidoId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID do bebê é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.VacinaSusId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID da vacina SUS é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}