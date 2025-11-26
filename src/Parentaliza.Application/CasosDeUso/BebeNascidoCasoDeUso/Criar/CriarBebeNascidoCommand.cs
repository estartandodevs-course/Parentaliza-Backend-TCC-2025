using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Enums;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Criar;

public class CriarBebeNascidoCommand : IRequest<CommandResponse<CriarBebeNascidoCommandResponse>>
{
    public Guid ResponsavelId { get; private set; }
    public string? Nome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public Sexo Sexo { get; private set; }
    public TipoSanguineo TipoSanguineo { get; private set; }
    public int IdadeMeses { get; private set; }
    public decimal Peso { get; private set; }
    public decimal Altura { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();
    public CriarBebeNascidoCommand(Guid responsavelId, string? nome, DateTime dataNascimento, Sexo sexo, TipoSanguineo tipoSanguineo, int idadeMeses, decimal peso, decimal altura)
    {
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
        var validacoes = new InlineValidator<CriarBebeNascidoCommand>();

        validacoes.RuleFor(BebeNascido => BebeNascido.ResponsavelId)
            .NotEqual(Guid.Empty)
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O ID do responsável é obrigatório.");

        validacoes.RuleFor(BebeNascido => BebeNascido.Nome)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O nome do bebê deve ser informado.");

        validacoes.RuleFor(BebeNascido => BebeNascido.DataNascimento)
            .NotEqual(default(DateTime))
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("A data de nascimento é obrigatória.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("A data de nascimento não pode ser uma data futura.");

        validacoes.RuleFor(BebeNascido => BebeNascido.Sexo)
            .IsInEnum()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O sexo informado é inválido.");

        validacoes.RuleFor(BebeNascido => BebeNascido.TipoSanguineo)
            .IsInEnum()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O tipo sanguíneo informado é inválido.");

        validacoes.RuleFor(BebeNascido => BebeNascido.IdadeMeses)
            .GreaterThanOrEqualTo(0)
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("A idade em meses não pode ser negativa.");

        validacoes.RuleFor(BebeNascido => BebeNascido.Peso)
            .GreaterThan(0)
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O peso deve ser maior que zero.");

        validacoes.RuleFor(BebeNascido => BebeNascido.Altura)
            .GreaterThan(0)
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("A altura deve ser maior que zero.");

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}