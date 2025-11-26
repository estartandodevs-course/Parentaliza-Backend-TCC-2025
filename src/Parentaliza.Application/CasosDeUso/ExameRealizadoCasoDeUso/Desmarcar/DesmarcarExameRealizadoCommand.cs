using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.Desmarcar;

public class DesmarcarExameRealizadoCommand : IRequest<CommandResponse<DesmarcarExameRealizadoCommandResponse>>
{
    public Guid BebeNascidoId { get; private set; }
    public Guid ExameSusId { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public DesmarcarExameRealizadoCommand(Guid bebeNascidoId, Guid exameSusId)
    {
        BebeNascidoId = bebeNascidoId;
        ExameSusId = exameSusId;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<DesmarcarExameRealizadoCommand>();

        validacoes.RuleFor(command => command.BebeNascidoId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID do bebê é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.ExameSusId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID do exame SUS é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}