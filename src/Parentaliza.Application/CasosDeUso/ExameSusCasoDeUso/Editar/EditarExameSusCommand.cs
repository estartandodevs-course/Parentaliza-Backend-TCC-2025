using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Editar;

public class EditarExameSusCommand : IRequest<CommandResponse<EditarExameSusCommandResponse>>
{
    public Guid Id { get; private set; }
    public string? NomeExame { get; private set; }
    public string? Descricao { get; private set; }
    public string? CategoriaFaixaEtaria { get; private set; }
    public int? IdadeMinMesesExame { get; private set; }
    public int? IdadeMaxMesesExame { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public EditarExameSusCommand(Guid id, string? nomeExame, string? descricao, string? categoriaFaixaEtaria, int? idadeMinMesesExame, int? idadeMaxMesesExame)
    {
        Id = id;
        NomeExame = nomeExame;
        Descricao = descricao;
        CategoriaFaixaEtaria = categoriaFaixaEtaria;
        IdadeMinMesesExame = idadeMinMesesExame;
        IdadeMaxMesesExame = idadeMaxMesesExame;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<EditarExameSusCommand>();

        validacoes.RuleFor(exame => exame.Id)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID do exame é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(exame => exame.NomeExame)
                  .NotEmpty()
                  .WithMessage("O nome do exame é obrigatório.")
                  .MaximumLength(200)
                  .WithMessage("O nome do exame deve ter no máximo 200 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(exame => exame.Descricao)
                  .MaximumLength(1000)
                  .WithMessage("A descrição deve ter no máximo 1000 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(exame => exame.CategoriaFaixaEtaria)
                  .MaximumLength(100)
                  .WithMessage("A categoria de faixa etária deve ter no máximo 100 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(exame => exame.IdadeMinMesesExame)
                  .GreaterThanOrEqualTo(0)
                  .When(exame => exame.IdadeMinMesesExame.HasValue)
                  .WithMessage("A idade mínima em meses não pode ser negativa.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(exame => exame.IdadeMaxMesesExame)
                  .GreaterThanOrEqualTo(0)
                  .When(exame => exame.IdadeMaxMesesExame.HasValue)
                  .WithMessage("A idade máxima em meses não pode ser negativa.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(exame => exame)
                  .Must(exame => !exame.IdadeMinMesesExame.HasValue || !exame.IdadeMaxMesesExame.HasValue || exame.IdadeMinMesesExame.Value <= exame.IdadeMaxMesesExame.Value)
                  .WithMessage("A idade mínima não pode ser maior que a idade máxima.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}

