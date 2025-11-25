using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Editar;

public class EditarBebeGestacaoCommand : IRequest<CommandResponse<EditarBebeGestacaoCommandResponse>>
{
    public Guid Id { get; private set; }
    public Guid ResponsavelId { get; private set; }
    public string? Nome { get; private set; }
    public DateTime DataPrevista { get; private set; }
    public int DiasDeGestacao { get; private set; }
    public decimal PesoEstimado { get; private set; }
    public decimal ComprimentoEstimado { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public EditarBebeGestacaoCommand(Guid id, Guid responsavelId, string? nome, DateTime dataPrevista, int diasDeGestacao, decimal pesoEstimado, decimal comprimentoEstimado)
    {
        Id = id;
        ResponsavelId = responsavelId;
        Nome = nome;
        DataPrevista = dataPrevista;
        DiasDeGestacao = diasDeGestacao;
        PesoEstimado = pesoEstimado;
        ComprimentoEstimado = comprimentoEstimado;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<EditarBebeGestacaoCommand>();

        validacoes.RuleFor(bebeGestacao => bebeGestacao.ResponsavelId)
                  .NotEqual(Guid.Empty).WithMessage("O ID do responsável é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeGestacao => bebeGestacao.Nome)
                  .NotEmpty().WithMessage("O nome do bebê não pode ser vazio.")
                  .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeGestacao => bebeGestacao.DataPrevista)
                  .NotEqual(default(DateTime)).WithMessage("A data prevista deve ser informada.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeGestacao => bebeGestacao.DiasDeGestacao)
                  .GreaterThanOrEqualTo(0)
                  .WithMessage("Os dias de gestação não podem ser negativos.")
                  .LessThanOrEqualTo(294)
                  .WithMessage("Os dias de gestação não podem ser maiores que 294 dias (42 semanas).")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeGestacao => bebeGestacao.PesoEstimado)
                  .GreaterThan(0).WithMessage("O peso estimado deve ser informado.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeGestacao => bebeGestacao.ComprimentoEstimado)
                  .GreaterThan(0).WithMessage("O comprimento estimado deve ser informado.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}
