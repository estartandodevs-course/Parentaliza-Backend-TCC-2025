using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleLeiteMaternoCasoDeUso.Criar;

public class CriarControleLeiteMaternoCommand : IRequest<CommandResponse<CriarControleLeiteMaternoCommandResponse>>
{
    public Guid BebeNascidoId { get; private set; }
    public DateTime Cronometro { get; private set; }
    public string? LadoDireito { get; private set; }
    public string? LadoEsquerdo { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public CriarControleLeiteMaternoCommand(Guid bebeNascidoId, DateTime cronometro, string? ladoDireito, string? ladoEsquerdo)
    {
        BebeNascidoId = bebeNascidoId;
        Cronometro = cronometro;
        LadoDireito = ladoDireito;
        LadoEsquerdo = ladoEsquerdo;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<CriarControleLeiteMaternoCommand>();

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

