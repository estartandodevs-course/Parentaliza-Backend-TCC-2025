using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Enums;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.BebeGestacaoCasoDeUso.ConverterParaNascido;

public class ConverterBebeGestacaoParaNascidoCommand : IRequest<CommandResponse<ConverterBebeGestacaoParaNascidoCommandResponse>>
{
    public Guid BebeGestacaoId { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public SexoEnum Sexo { get; private set; }
    public TipoSanguineoEnum TipoSanguineo { get; private set; }
    public int IdadeMeses { get; private set; }
    public decimal Peso { get; private set; }
    public decimal Altura { get; private set; }
    public bool ExcluirBebeGestacao { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public ConverterBebeGestacaoParaNascidoCommand(
        Guid bebeGestacaoId,
        DateTime dataNascimento,
        SexoEnum sexo,
        TipoSanguineoEnum tipoSanguineo,
        int idadeMeses,
        decimal peso,
        decimal altura,
        bool excluirBebeGestacao)
    {
        BebeGestacaoId = bebeGestacaoId;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        TipoSanguineo = tipoSanguineo;
        IdadeMeses = idadeMeses;
        Peso = peso;
        Altura = altura;
        ExcluirBebeGestacao = excluirBebeGestacao;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<ConverterBebeGestacaoParaNascidoCommand>();

        validacoes.RuleFor(command => command.BebeGestacaoId)
                  .NotEqual(Guid.Empty)
                  .WithMessage("O ID do bebê em gestação é obrigatório.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.DataNascimento)
                  .NotEqual(default(DateTime))
                  .WithMessage("A data de nascimento é obrigatória.")
                  .LessThanOrEqualTo(DateTime.UtcNow)
                  .WithMessage("A data de nascimento não pode ser no futuro.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.Sexo)
                  .IsInEnum()
                  .WithMessage("O sexo informado é inválido.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.TipoSanguineo)
                  .IsInEnum()
                  .WithMessage("O tipo sanguíneo informado é inválido.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.IdadeMeses)
                  .GreaterThanOrEqualTo(0)
                  .WithMessage("A idade em meses não pode ser negativa.")
                  .LessThanOrEqualTo(120)
                  .WithMessage("A idade em meses não pode ser maior que 120.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.Peso)
                  .GreaterThan(0)
                  .WithMessage("O peso deve ser maior que zero.")
                  .LessThanOrEqualTo(20)
                  .WithMessage("O peso não pode ser maior que 20 kg.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(command => command.Altura)
                  .GreaterThan(0)
                  .WithMessage("A altura deve ser maior que zero.")
                  .LessThanOrEqualTo(100)
                  .WithMessage("A altura não pode ser maior que 100 cm.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}

