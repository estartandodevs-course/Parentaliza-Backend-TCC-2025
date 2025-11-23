using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Editar;

public class EditarConteudoCommand : IRequest<CommandResponse<EditarConteudoCommandResponse>>
{
    public Guid Id { get; private set; }
    public string? Titulo { get; private set; }
    public string? Categoria { get; private set; }
    public DateTime DataPublicacao { get; private set; }
    public string? Descricao { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();
    public EditarConteudoCommand(Guid id, string? titulo, string? categoria, DateTime dataPublicacao, string? descricao)
    {
        Id = id;
        Titulo = titulo;
        Categoria = categoria;
        DataPublicacao = dataPublicacao;
        Descricao = descricao;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<EditarConteudoCommand>();

        validacoes.RuleFor(Conteudo => Conteudo.Titulo)
                  .NotEmpty().WithMessage("O título é obrigatório.")
                  .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(Conteudo => Conteudo.Categoria)
                  .NotEmpty().WithMessage("A categoria é obrigatória.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                  .Custom((categoria, contexto) =>
                  {
                      var categoriasValidas = new List<string> { "Nutrição", "Pós-parto", "Direitos trabalhistas" };
                      if (categoria == null || !categoriasValidas.Contains(categoria))
                      {
                          contexto.AddFailure("A categoria informada é inválida. As categorias válidas são: Nutrição, Pós-parto, Direitos trabalhistas.");
                      }
                  });

        validacoes.RuleFor(Conteudo => Conteudo.DataPublicacao)
                  .LessThanOrEqualTo(DateTime.Now)
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                  .ChildRules(data =>
                  {
                      data.RuleFor(d => d)
                   .NotEmpty().WithMessage("A data de publicação é obrigatória.");
                  });

        validacoes.RuleFor(Conteudo => Conteudo.Descricao)
                  .ChildRules(descricao =>
                  {
                      descricao.RuleFor(d => d)
                        .MaximumLength(2000).WithMessage("A descrição deve ter no máximo 2000 caracteres.");
                  })
                  .NotEmpty().WithMessage("A descrição é obrigatória.")
                  .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}
