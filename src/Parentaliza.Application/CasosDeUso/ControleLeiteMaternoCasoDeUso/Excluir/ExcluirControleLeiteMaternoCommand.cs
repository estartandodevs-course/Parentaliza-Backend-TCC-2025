using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Excluir;

public class ExcluirControleLeiteMaternoCommand : IRequest<CommandResponse<Unit>>
{
    public Guid Id { get; private set; }
    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public ExcluirControleLeiteMaternoCommand(Guid id)
    {
        Id = id;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<ExcluirControleLeiteMaternoCommand>();

        validacoes.RuleFor(controle => controle.Id)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}

