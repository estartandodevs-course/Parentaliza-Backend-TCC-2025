using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Criar;

public class CriarControleMamadeiraCommand : IRequest<CommandResponse<CriarControleMamadeiraCommandResponse>>
{
    public Guid BebeNascidoId { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Hora { get; private set; }
    public decimal? QuantidadeLeite { get; private set; }
    public string? Anotacao { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public CriarControleMamadeiraCommand(Guid bebeNascidoId, DateTime data, TimeSpan hora, decimal? quantidadeLeite, string? anotacao)
    {
        BebeNascidoId = bebeNascidoId;
        Data = data;
        Hora = hora;
        QuantidadeLeite = quantidadeLeite;
        Anotacao = anotacao;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<CriarControleMamadeiraCommand>();

        validacoes.RuleFor(controle => controle.BebeNascidoId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID do bebê é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(controle => controle.Data)
                  .NotEqual(default(DateTime))
                  .WithMessage("A data é obrigatória.")
                  .LessThanOrEqualTo(DateTime.UtcNow.Date)
                  .WithMessage("A data não pode ser no futuro.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(controle => controle.QuantidadeLeite)
                  .GreaterThanOrEqualTo(0)
                  .When(controle => controle.QuantidadeLeite.HasValue)
                  .WithMessage("A quantidade de leite não pode ser negativa.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(controle => controle.Anotacao)
                  .MaximumLength(500)
                  .WithMessage("A anotação deve ter no máximo 500 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}