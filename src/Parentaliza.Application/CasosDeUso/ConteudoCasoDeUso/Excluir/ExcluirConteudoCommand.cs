using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Excluir;

public class ExcluirConteudoCommand : IRequest<CommandResponse<Unit>>
{
    public Guid Id { get; private set; }
    public ValidationResult ResultadoDasValidacoes { get; private set; }

    public ExcluirConteudoCommand(Guid id)
    {
        Id = id;
    }
    public bool Validar()
    {
        var validacoes = new InlineValidator<ExcluirConteudoCommand>();

        validacoes.RuleFor(conteudo => conteudo.Id)
            .NotEqual(Guid.Empty)
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O ID do conteúdo deve ser informado.");


        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}