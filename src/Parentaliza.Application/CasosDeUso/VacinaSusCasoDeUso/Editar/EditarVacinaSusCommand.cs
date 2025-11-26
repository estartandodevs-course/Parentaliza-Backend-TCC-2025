using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Editar;

public class EditarVacinaSusCommand : IRequest<CommandResponse<EditarVacinaSusCommandResponse>>
{
    public Guid Id { get; private set; }
    public string? NomeVacina { get; private set; }
    public string? Descricao { get; private set; }
    public string? CategoriaFaixaEtaria { get; private set; }
    public int? IdadeMinMesesVacina { get; private set; }
    public int? IdadeMaxMesesVacina { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public EditarVacinaSusCommand(Guid id, string? nomeVacina, string? descricao, string? categoriaFaixaEtaria, int? idadeMinMesesVacina, int? idadeMaxMesesVacina)
    {
        Id = id;
        NomeVacina = nomeVacina;
        Descricao = descricao;
        CategoriaFaixaEtaria = categoriaFaixaEtaria;
        IdadeMinMesesVacina = idadeMinMesesVacina;
        IdadeMaxMesesVacina = idadeMaxMesesVacina;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<EditarVacinaSusCommand>();

        validacoes.RuleFor(vacina => vacina.Id)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID da vacina é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(vacina => vacina.NomeVacina)
                  .NotEmpty()
                  .WithMessage("O nome da vacina é obrigatório.")
                  .MaximumLength(200)
                  .WithMessage("O nome da vacina deve ter no máximo 200 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(vacina => vacina.Descricao)
                  .MaximumLength(1000)
                  .WithMessage("A descrição deve ter no máximo 1000 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(vacina => vacina.CategoriaFaixaEtaria)
                  .MaximumLength(100)
                  .WithMessage("A categoria de faixa etária deve ter no máximo 100 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(vacina => vacina.IdadeMinMesesVacina)
                  .GreaterThanOrEqualTo(0)
                  .When(vacina => vacina.IdadeMinMesesVacina.HasValue)
                  .WithMessage("A idade mínima em meses não pode ser negativa.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(vacina => vacina.IdadeMaxMesesVacina)
                  .GreaterThanOrEqualTo(0)
                  .When(vacina => vacina.IdadeMaxMesesVacina.HasValue)
                  .WithMessage("A idade máxima em meses não pode ser negativa.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(vacina => vacina)
                  .Must(vacina => !vacina.IdadeMinMesesVacina.HasValue || !vacina.IdadeMaxMesesVacina.HasValue || vacina.IdadeMinMesesVacina.Value <= vacina.IdadeMaxMesesVacina.Value)
                  .WithMessage("A idade mínima não pode ser maior que a idade máxima.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}