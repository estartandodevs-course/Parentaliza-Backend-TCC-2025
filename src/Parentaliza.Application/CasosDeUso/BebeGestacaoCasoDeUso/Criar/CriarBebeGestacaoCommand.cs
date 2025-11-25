using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Criar;

public class CriarBebeGestacaoCommand : IRequest<CommandResponse<CriarBebeGestacaoCommandResponse>>
{
    public Guid ResponsavelId { get; private set; }
    public string? Nome { get; private set; }
    public DateTime DataPrevista { get; private set; }
    public int DiasDeGestacao { get; private set; }
    public decimal PesoEstimado { get; private set; }
    public decimal ComprimentoEstimado { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();
    public CriarBebeGestacaoCommand(Guid responsavelId, string? nome, DateTime dataPrevista, int diasDeGestacao, decimal pesoEstimado, decimal comprimentoEstimado)
    {
        ResponsavelId = responsavelId;
        Nome = nome;
        DataPrevista = dataPrevista;
        DiasDeGestacao = diasDeGestacao;
        PesoEstimado = pesoEstimado;
        ComprimentoEstimado = comprimentoEstimado;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<CriarBebeGestacaoCommand>();

        validacoes.RuleFor(BebeGestacao => BebeGestacao.ResponsavelId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID do responsável é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(BebeGestacao => BebeGestacao.Nome)
                  .NotEmpty()
                  .WithMessage("O nome é obrigatório.")
                  .MaximumLength(100)
                  .WithMessage("O nome deve ter no máximo 100 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(BebeGestacao => BebeGestacao.DataPrevista)
                  .NotEqual(default(DateTime))
                  .WithMessage("A data prevista é obrigatória.")
                  .GreaterThan(DateTime.UtcNow)
                  .WithMessage("A data prevista deve ser uma data futura.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(BebeGestacao => BebeGestacao.DiasDeGestacao)
                  .GreaterThanOrEqualTo(0)
                  .WithMessage("Os dias de gestação não podem ser negativos.")
                  .LessThanOrEqualTo(294)
                  .WithMessage("Os dias de gestação não podem ser maiores que 294 dias (42 semanas).")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(BebeGestacao => BebeGestacao.PesoEstimado)
                  .GreaterThan(0)
                  .WithMessage("O peso estimado deve ser maior que zero.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(BebeGestacao => BebeGestacao.ComprimentoEstimado)
                  .GreaterThan(0)
                  .WithMessage("O comprimento estimado deve ser maior que zero.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}