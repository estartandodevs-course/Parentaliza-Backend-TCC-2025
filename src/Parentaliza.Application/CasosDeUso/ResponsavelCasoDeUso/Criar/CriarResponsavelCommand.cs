using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Enums;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Criar;

public class CriarResponsavelCommand : IRequest<CommandResponse<CriarResponsavelCommandResponse>>
{
    public string? Nome { get; private set; }
    public string? Email { get; private set; }
    public TipoResponsavel TipoResponsavel { get; private set; }
    public string? Senha { get; private set; }
    public string? FaseNascimento { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public CriarResponsavelCommand(string? nome, string? email, TipoResponsavel tipoResponsavel, string? senha, string? faseNascimento)
    {
        Nome = nome;
        Email = email;
        TipoResponsavel = tipoResponsavel;
        Senha = senha;
        FaseNascimento = faseNascimento;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<CriarResponsavelCommand>();

        validacoes.RuleFor(responsavel => responsavel.Nome)
                  .NotEmpty()
                  .WithMessage("O nome é obrigatório.")
                  .MaximumLength(100)
                  .WithMessage("O nome deve ter no máximo 100 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(responsavel => responsavel.Email)
                  .NotEmpty()
                  .WithMessage("O email é obrigatório.")
                  .EmailAddress()
                  .WithMessage("O email deve ser válido.")
                  .MaximumLength(255)
                  .WithMessage("O email deve ter no máximo 255 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(responsavel => responsavel.Senha)
                  .NotEmpty()
                  .WithMessage("A senha é obrigatória.")
                  .MinimumLength(6)
                  .WithMessage("A senha deve ter no mínimo 6 caracteres.")
                  .MaximumLength(100)
                  .WithMessage("A senha deve ter no máximo 100 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(responsavel => responsavel.TipoResponsavel)
                  .IsInEnum()
                  .WithMessage("O tipo de responsável informado é inválido.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}