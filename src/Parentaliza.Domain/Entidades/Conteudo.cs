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
        Titulo = titulo;
        Categoria = categoria;
        DataPublicacao = dataPublicacao;
        Descricao = descricao;
    }
}
