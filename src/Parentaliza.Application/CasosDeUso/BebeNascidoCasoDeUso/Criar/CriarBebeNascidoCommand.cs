using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Enums;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeNascidoCasoDeUso.Criar;

public class CriarBebeNascidoCommand : IRequest<CommandResponse<CriarBebeNascidoCommandResponse>>
{
    public int ResponsavelIdN { get; private set; }
    public string? Nome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public SexoEnum Sexo { get; private set; }
    public TipoSanguineoEnum TipoSanguineo { get; private set; }
    public int IdadeMeses { get; private set; }
    public decimal Peso { get; private set; }
    public decimal Altura { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();
    public CriarBebeNascidoCommand(string? nome, DateTime dataNascimento, SexoEnum sexo, TipoSanguineoEnum tipoSanguineo, int idadeMeses, decimal peso, decimal altura)
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
           .ChildRules(dataNascimento =>
           {
               dataNascimento.RuleFor(dn => dn)
                   .LessThanOrEqualTo(DateTime.Now)
                   .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                   .WithMessage("A data de nascimento não pode ser uma data futura.");
           });

        validacoes.RuleFor(BebeNascido => BebeNascido.Sexo)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .ChildRules(sexo =>
            {
                sexo.RuleFor(s => s)
                    .IsInEnum()
                    .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                    .WithMessage("O sexo informado é inválido.");
            });

        validacoes.RuleFor(BebeNascido => BebeNascido.TipoSanguineo)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .ChildRules(tipoSanguineo =>
            {
                tipoSanguineo.RuleFor(ts => ts)
                    .IsInEnum()
                    .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                    .WithMessage("O tipo sanguíneo informado é inválido.");
            });

        validacoes.RuleFor(BebeNascido => BebeNascido.IdadeMeses)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .ChildRules(idadeMeses =>
            {
                idadeMeses.RuleFor(im => im)
                    .GreaterThanOrEqualTo(0)
                    .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                    .WithMessage("A idade em meses não pode ser negativa.");
            });

        validacoes.RuleFor(BebeNascido => BebeNascido.Peso)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .ChildRules(peso =>
            {
                peso.RuleFor(p => p)
                    .GreaterThan(0)
                    .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                    .WithMessage("O peso deve ser maior que zero.");
            });

        validacoes.RuleFor(BebeNascido => BebeNascido.Altura)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .ChildRules(altura =>
            {
                altura.RuleFor(a => a)
                    .GreaterThan(0)
                    .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                    .WithMessage("A altura deve ser maior que zero.");
            });

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}