namespace Parentaliza.Domain.Entidades;

public class Conteudo : Entity
{
    public string? Titulo { get; private set; }
    public string? Categoria { get; private set; }
    public DateTime DataPublicacao { get; private set; }
    public string? Descricao { get; private set; }
    public Conteudo() { }
    public Conteudo(string? titulo, string? categoria, DateTime dataPublicacao, string? descricao)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título é obrigatório.", nameof(titulo));

        if (string.IsNullOrWhiteSpace(categoria))
            throw new ArgumentException("A categoria é obrigatória.", nameof(categoria));

        if (dataPublicacao > DateTime.UtcNow)
            throw new ArgumentException("A data de publicação não pode ser no futuro.", nameof(dataPublicacao));

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição é obrigatória.", nameof(descricao));

        Titulo = titulo;
        Categoria = categoria;
        DataPublicacao = dataPublicacao;
        Descricao = descricao;
    }
}