using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Enums;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Editar;

public class EditarBebeNascidoCommand : IRequest<CommandResponse<EditarBebeNascidoCommandResponse>>
{
    public Guid Id { get; private set; }
    public Guid ResponsavelId { get; private set; }
    public string? Nome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public Sexo Sexo { get; private set; }
    public TipoSanguineo TipoSanguineo { get; private set; }
    public int IdadeMeses { get; private set; }
    public decimal Peso { get; private set; }
    public decimal Altura { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();
    public EditarBebeNascidoCommand(Guid id, Guid responsavelId, string? nome, DateTime dataNascimento, Sexo sexo, TipoSanguineo tipoSanguineo, int idadeMeses, decimal peso, decimal altura)
    {
        Id = id;
        ResponsavelId = responsavelId;
        Nome = nome;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        TipoSanguineo = tipoSanguineo;
        IdadeMeses = idadeMeses;
        Peso = peso;
        Altura = altura;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<EditarBebeNascidoCommand>();

        validacoes.RuleFor(bebeNascido => bebeNascido.ResponsavelId)
                  .NotEqual(Guid.Empty).WithMessage("O ID do responsável é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeNascido => bebeNascido.Nome)
                  .NotEmpty().WithMessage("O nome do bebê é obrigatório.")
                  .MaximumLength(100).WithMessage("O nome do bebê não pode exceder 100 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeNascido => bebeNascido.DataNascimento)
                  .NotEqual(default(DateTime)).WithMessage("A data de nascimento é obrigatória.")
                  .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data de nascimento não pode ser no futuro.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeNascido => bebeNascido.Sexo)
                  .IsInEnum().WithMessage("O sexo informado é inválido.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeNascido => bebeNascido.TipoSanguineo)
                  .IsInEnum().WithMessage("O tipo sanguíneo informado é inválido.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeNascido => bebeNascido.IdadeMeses)
                  .GreaterThanOrEqualTo(0).WithMessage("A idade em meses não pode ser negativa.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeNascido => bebeNascido.Peso)
                  .GreaterThan(0).WithMessage("O peso deve ser maior que zero.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(bebeNascido => bebeNascido.Altura)
                  .GreaterThan(0).WithMessage("A altura deve ser maior que zero.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}