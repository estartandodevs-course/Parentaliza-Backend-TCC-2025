using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using Parentaliza.Domain.Enums;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ResponsavelCasoDeUso.Editar;

public class EditarResponsavelCommand : IRequest<CommandResponse<EditarResponsavelCommandResponse>>
{
    public Guid Id { get; private set; }
    public string? Nome { get; private set; }
    public string? Email { get; private set; }
    public TipoResponsavel TipoResponsavel { get; private set; }
    public string? Senha { get; private set; }
    public string? FaseNascimento { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public EditarResponsavelCommand(Guid id, string? nome, string? email, TipoResponsavel tipoResponsavel, string? senha, string? faseNascimento)
    {
        Id = id;
        Nome = nome;
        Email = email;
        TipoResponsavel = tipoResponsavel;
        Senha = senha;
        FaseNascimento = faseNascimento;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<EditarResponsavelCommand>();

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

        validacoes.RuleFor(responsavel => responsavel.TipoResponsavel)
                  .IsInEnum()
                  .WithMessage("O tipo de responsável informado é inválido.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}

