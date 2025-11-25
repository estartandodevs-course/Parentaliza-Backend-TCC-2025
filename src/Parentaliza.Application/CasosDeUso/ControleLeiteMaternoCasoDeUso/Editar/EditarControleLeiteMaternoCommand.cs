using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Editar;

public class EditarControleLeiteMaternoCommand : IRequest<CommandResponse<EditarControleLeiteMaternoCommandResponse>>
{
    public Guid Id { get; private set; }
    public Guid BebeNascidoId { get; private set; }
    public DateTime Cronometro { get; private set; }
    public string? LadoDireito { get; private set; }
    public string? LadoEsquerdo { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public EditarControleLeiteMaternoCommand(Guid id, Guid bebeNascidoId, DateTime cronometro, string? ladoDireito, string? ladoEsquerdo)
    {
        Id = id;
        BebeNascidoId = bebeNascidoId;
        Cronometro = cronometro;
        LadoDireito = ladoDireito;
        LadoEsquerdo = ladoEsquerdo;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<EditarControleLeiteMaternoCommand>();

        validacoes.RuleFor(controle => controle.Id)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(controle => controle.BebeNascidoId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID do bebê é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(controle => controle.Cronometro)
                  .NotEqual(default(DateTime))
                  .WithMessage("O cronômetro é obrigatório.")
                  .LessThanOrEqualTo(DateTime.UtcNow)
                  .WithMessage("O cronômetro não pode ser no futuro.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(controle => controle.LadoDireito)
                  .MaximumLength(50)
                  .WithMessage("O lado direito deve ter no máximo 50 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(controle => controle.LadoEsquerdo)
                  .MaximumLength(50)
                  .WithMessage("O lado esquerdo deve ter no máximo 50 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}

