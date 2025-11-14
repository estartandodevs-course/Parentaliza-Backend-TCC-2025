using FluentValidation;
using MediatR;
using FluentValidation.Results;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Enums;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Criar;

public class CriarBebeNascidoCommand : IRequest<CommandResponse<CriarBebeNascidoCommandResponse>>
{
    public string? Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public SexoEnum Sexo { get; set; }
    public TipoSanguineoEnum TipoSanguineo { get; set; }
    public int? IdadeMeses { get; set; }
    public decimal Peso { get; set; }
    public decimal Altura { get; set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();
    public CriarBebeNascidoCommand(string? nome, DateTime dataNascimento, SexoEnum sexo, TipoSanguineoEnum tipoSanguineo, int? idadeMeses, decimal peso, decimal altura)
    {
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

        validacoes.RuleFor(BebeNascido => BebeNascido.Nome)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O nome do fornecedor deve ser informado.");

        validacoes.RuleFor(BebeNascido => BebeNascido.DataNascimento)
           .NotEmpty()
           .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
           .WithMessage("O documento do fornecedor precisa ser informado");

        validacoes.RuleFor(BebeNascido => BebeNascido.Sexo)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O tipo do fornecedor precisa ser 1 (Pessoa Física) ou 2 (Pessoa Jurídica)");

        validacoes.RuleFor(BebeNascido => BebeNascido.TipoSanguineo)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O tipo do fornecedor precisa ser 1 (Pessoa Física) ou 2 (Pessoa Jurídica)");

        validacoes.RuleFor(BebeNascido => BebeNascido.IdadeMeses)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O tipo do fornecedor precisa ser 1 (Pessoa Física) ou 2 (Pessoa Jurídica)");

        validacoes.RuleFor(BebeNascido => BebeNascido.Peso)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O tipo do fornecedor precisa ser 1 (Pessoa Física) ou 2 (Pessoa Jurídica)");

        validacoes.RuleFor(BebeNascido => BebeNascido.Altura)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O tipo do fornecedor precisa ser 1 (Pessoa Física) ou 2 (Pessoa Jurídica)");

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}