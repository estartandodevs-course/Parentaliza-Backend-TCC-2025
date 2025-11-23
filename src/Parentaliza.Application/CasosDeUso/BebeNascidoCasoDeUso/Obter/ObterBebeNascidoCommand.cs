using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Obter;
using Parentaliza.Application.Mediator;
using System.Net;


namespace Parentaliza.Application.CasosDeUso.PerfilBebe.Obter;

public class ObterBebeNascidoCommand : IRequest<CommandResponse<ObterBebeNascidoCommandResponse>>
{
    public Guid Id { get; private set; }
    public ValidationResult ResultadoDasValidacoes { get; private set; }

    public ObterBebeNascidoCommand(Guid id)
    {
        Id = id;

    }
    public bool Validar()
    {
        var validacoes = new InlineValidator<ObterBebeNascidoCommand>();

        validacoes.RuleFor(BebeNascido => BebeNascido.Id)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O ID do Bebê deve ser informado.");

        ResultadoDasValidacoes = validacoes.Validate(this);

        return ResultadoDasValidacoes.IsValid;
    }
}
