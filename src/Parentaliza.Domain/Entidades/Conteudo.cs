namespace Parentaliza.Domain.Entidades;

public class Conteudo
{
    public string? Titulo { get; set; }
    public string? Categoria { get; set; }
    public DateTime DataPublicacao { get; set; }
    public string? Descricao { get; set; }
    public Conteudo() { }
    public Conteudo(string? titulo, string? categoria, DateTime dataPublicacao, string? descricao)
    {
        Titulo = titulo;
        Categoria = categoria;
        DataPublicacao = dataPublicacao;
        Descricao = descricao;
    }
}
