using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.Criar;

public class CriarBebeGestacaoCommand : IRequest<CommandResponse<CriarBebeGestacaoCommandResponse>>
{
    public string? Nome { get; private set; }
    public DateTime DataPrevista { get; private set; }
    public int DiasDeGestacao { get; private set; }
    public decimal PesoEstimado { get; private set; }
    public decimal ComprimentoEstimado { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();
    public CriarBebeGestacaoCommand(string? nome, DateTime dataPrevista, int diasDeGestacao, decimal pesoEstimado, decimal comprimentoEstimado)
    {
        Nome = nome;
        DataPrevista = dataPrevista;
        DiasDeGestacao = diasDeGestacao;
        PesoEstimado = pesoEstimado;
        ComprimentoEstimado = comprimentoEstimado;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<CriarBebeGestacaoCommand>();

        validacoes.RuleFor(BebeGestacao => BebeGestacao.Nome)
                  .NotEmpty()
                  .WithMessage("O nome é obrigatória.")
                  .ChildRules(nome =>
                  {
                      nome.RuleFor(n => n.Length).LessThanOrEqualTo(100)
                  .WithMessage("O nome deve ter no máximo 100 caracteres.");
                  })
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(BebeGestacao => BebeGestacao.DataPrevista)
                  .NotEmpty()
                  .WithMessage("A data prevista é obrigatória.")
                  .GreaterThan(DateTime.Now).WithMessage("A data prevista deve ser uma data futura.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(BebeGestacao => BebeGestacao.DiasDeGestacao)
                  .NotEmpty()
                  .WithMessage("Os dias de gestação são obrigatórios.")
                  .GreaterThan(0).WithMessage("Os dias de gestação devem ser maiores que zero.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(BebeGestacao => BebeGestacao.PesoEstimado)
                  .NotEmpty()
                  .WithMessage("O peso estimado é obrigatório.")
                  .GreaterThan(0).WithMessage("O peso estimado deve ser maior que zero.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(BebeGestacao => BebeGestacao.ComprimentoEstimado)
                  .NotEmpty()
                  .WithMessage("O comprimento estimado é obrigatório.")
                  .GreaterThan(0).WithMessage("O comprimento estimado deve ser maior que zero.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}