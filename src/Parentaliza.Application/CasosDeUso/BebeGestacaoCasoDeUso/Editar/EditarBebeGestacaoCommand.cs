using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.PerfilBebeGestacaoCasoDeUso.Editar;

public class EditarBebeGestacaoCommand : IRequest<CommandResponse<EditarBebeGestacaoCommandResponse>>
{
    public Guid Id { get; private set; }
    public string? Nome { get; private set; }
    public DateTime DataPrevista { get; private set; }
    public int DiasDeGestacao { get; private set; }
    public decimal PesoEstimado { get; private set; }
    public decimal ComprimetoEstimado { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public EditarBebeGestacaoCommand(Guid id, string? nome, DateTime dataPrevista, int diasDeGestacao, decimal pesoEstimado, decimal comprimetoEstimado)
    {
        Id = id;
        Nome = nome;
        DataPrevista = dataPrevista;
        DiasDeGestacao = diasDeGestacao;
        PesoEstimado = pesoEstimado;
        ComprimetoEstimado = comprimetoEstimado;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<EditarBebeGestacaoCommand>();

        validacoes.RuleFor(bebeGestacao => bebeGestacao.Nome)
                  .NotEmpty().WithMessage("O nome do bêbe não pode ser vazio.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeGestacao => bebeGestacao.DataPrevista)
                  .NotEmpty().WithMessage("A data prevista deve ser informada.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeGestacao => bebeGestacao.DiasDeGestacao)
                  .NotEmpty().WithMessage("Os dias de gestação deve ser informado.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeGestacao => bebeGestacao.PesoEstimado)
                  .NotEmpty().WithMessage("O peso estimado dever ser informado.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeGestacao => bebeGestacao.ComprimetoEstimado)
                  .NotEmpty().WithMessage("O comprimento estimado de ser informado.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}
