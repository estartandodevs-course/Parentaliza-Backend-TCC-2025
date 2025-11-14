using FluentValidation;
using FluentValidation.Results;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Criar;

public class CriarConteudoCommand
{
    public string? Titulo { get; set; }
    public string? Categoria { get; set; }
    public DateTime DataPublicacao { get; set; }
    public string? Descricao { get; set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();
    public CriarConteudoCommand(string? titulo, string? categoria, DateTime dataPublicacao, string? descricao)
    {
        Titulo = titulo;
        Categoria = categoria;
        DataPublicacao = dataPublicacao;
        Descricao = descricao;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<CriarConteudoCommand>();

        validacoes.RuleFor(Conteudo => Conteudo.Titulo)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres.");
        validacoes.RuleFor(Conteudo => Conteudo.Categoria).NotEmpty().WithMessage("A categoria é obrigatória.");

        validacoes.RuleFor(Conteudo => Conteudo.DataPublicacao)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de publicação não pode ser no futuro.");

        validacoes.RuleFor(Conteudo => Conteudo.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.");

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}