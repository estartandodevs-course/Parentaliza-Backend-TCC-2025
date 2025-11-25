using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Obter;

public class ObterBebeNascidoCommand : IRequest<CommandResponse<ObterBebeNascidoCommandResponse>>
{
    public Guid Id { get; private set; }
    public ValidationResult ResultadoDasValidacoes { get; private set; }

    public ObterBebeNascidoCommand(Guid id)
    {
        Id = id;
        ResultadoDasValidacoes = new ValidationResult();
    }
    public bool Validar()
    {
        var validacoes = new InlineValidator<ObterBebeNascidoCommand>();

        validacoes.RuleFor(BebeNascido => BebeNascido.Id)
            .NotEqual(Guid.Empty)
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O ID do Bebê deve ser informado.");

        ResultadoDasValidacoes = validacoes.Validate(this);

        return ResultadoDasValidacoes.IsValid;
    }
}
