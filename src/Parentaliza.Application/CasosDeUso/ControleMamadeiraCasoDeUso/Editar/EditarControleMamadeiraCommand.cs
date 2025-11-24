using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ControleMamadeiraCasoDeUso.Editar;

public class EditarControleMamadeiraCommand : IRequest<CommandResponse<EditarControleMamadeiraCommandResponse>>
{
    public Guid Id { get; private set; }
    public Guid BebeNascidoId { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Hora { get; private set; }
    public decimal? QuantidadeLeite { get; private set; }
    public string? Anotacao { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public EditarControleMamadeiraCommand(Guid id, Guid bebeNascidoId, DateTime data, TimeSpan hora, decimal? quantidadeLeite, string? anotacao)
    {
        Id = id;
        BebeNascidoId = bebeNascidoId;
        Data = data;
        Hora = hora;
        QuantidadeLeite = quantidadeLeite;
        Anotacao = anotacao;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<EditarControleMamadeiraCommand>();

        validacoes.RuleFor(controle => controle.Id)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

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

