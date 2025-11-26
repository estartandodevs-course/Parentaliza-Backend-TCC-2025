using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ExameRealizadoCasoDeUso.MarcarRealizado;

public class MarcarExameRealizadoCommand : IRequest<CommandResponse<MarcarExameRealizadoCommandResponse>>
{
    public Guid BebeNascidoId { get; private set; }
    public Guid ExameSusId { get; private set; }
    public DateTime DataRealizacao { get; private set; }
    public string? Observacoes { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public MarcarExameRealizadoCommand(Guid bebeNascidoId, Guid exameSusId, DateTime dataRealizacao, string? observacoes)
    {
        BebeNascidoId = bebeNascidoId;
        ExameSusId = exameSusId;
        DataRealizacao = dataRealizacao;
        Observacoes = observacoes;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<MarcarExameRealizadoCommand>();

        validacoes.RuleFor(command => command.BebeNascidoId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID do bebê é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.ExameSusId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID do exame SUS é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.DataRealizacao)
                  .NotEqual(default(DateTime))
                  .WithMessage("A data de realização é obrigatória.")
                  .LessThanOrEqualTo(DateTime.UtcNow)
                  .WithMessage("A data de realização não pode ser no futuro.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.Observacoes)
                  .MaximumLength(500)
                  .WithMessage("As observações devem ter no máximo 500 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}