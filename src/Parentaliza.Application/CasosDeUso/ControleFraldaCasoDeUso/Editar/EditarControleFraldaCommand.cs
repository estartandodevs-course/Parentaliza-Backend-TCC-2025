using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleFraldaCasoDeUso.Editar;

public class EditarControleFraldaCommand : IRequest<CommandResponse<EditarControleFraldaCommandResponse>>
{
    public Guid Id { get; private set; }
    public Guid BebeNascidoId { get; private set; }
    public DateTime HoraTroca { get; private set; }
    public string? TipoFralda { get; private set; }
    public string? Observacoes { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public EditarControleFraldaCommand(Guid id, Guid bebeNascidoId, DateTime horaTroca, string? tipoFralda, string? observacoes)
    {
        Id = id;
        BebeNascidoId = bebeNascidoId;
        HoraTroca = horaTroca;
        TipoFralda = tipoFralda;
        Observacoes = observacoes;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<EditarControleFraldaCommand>();

        validacoes.RuleFor(controle => controle.Id)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(controle => controle.BebeNascidoId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID do bebê é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(controle => controle.HoraTroca)
                  .NotEqual(default(DateTime))
                  .WithMessage("A hora da troca é obrigatória.")
                  .LessThanOrEqualTo(DateTime.UtcNow)
                  .WithMessage("A hora da troca não pode ser no futuro.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(controle => controle.TipoFralda)
                  .MaximumLength(50)
                  .WithMessage("O tipo de fralda deve ter no máximo 50 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(controle => controle.Observacoes)
                  .MaximumLength(500)
                  .WithMessage("As observações devem ter no máximo 500 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}