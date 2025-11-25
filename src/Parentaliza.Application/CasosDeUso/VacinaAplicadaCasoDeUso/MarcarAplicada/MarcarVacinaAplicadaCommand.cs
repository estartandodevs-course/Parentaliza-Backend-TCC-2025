using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.VacinaAplicadaCasoDeUso.MarcarAplicada;

public class MarcarVacinaAplicadaCommand : IRequest<CommandResponse<MarcarVacinaAplicadaCommandResponse>>
{
    public Guid BebeNascidoId { get; private set; }
    public Guid VacinaSusId { get; private set; }
    public DateTime DataAplicacao { get; private set; }
    public string? Lote { get; private set; }
    public string? LocalAplicacao { get; private set; }
    public string? Observacoes { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public MarcarVacinaAplicadaCommand(Guid bebeNascidoId, Guid vacinaSusId, DateTime dataAplicacao, string? lote, string? localAplicacao, string? observacoes)
    {
        BebeNascidoId = bebeNascidoId;
        VacinaSusId = vacinaSusId;
        DataAplicacao = dataAplicacao;
        Lote = lote;
        LocalAplicacao = localAplicacao;
        Observacoes = observacoes;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<MarcarVacinaAplicadaCommand>();

        validacoes.RuleFor(command => command.BebeNascidoId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID do bebê é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.VacinaSusId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID da vacina SUS é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.DataAplicacao)
                  .NotEqual(default(DateTime))
                  .WithMessage("A data de aplicação é obrigatória.")
                  .LessThanOrEqualTo(DateTime.UtcNow)
                  .WithMessage("A data de aplicação não pode ser no futuro.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.Lote)
                  .MaximumLength(50)
                  .WithMessage("O lote deve ter no máximo 50 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.LocalAplicacao)
                  .MaximumLength(100)
                  .WithMessage("O local de aplicação deve ter no máximo 100 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.Observacoes)
                  .MaximumLength(500)
                  .WithMessage("As observações devem ter no máximo 500 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}

