namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Obter;

public class ObterConteudoCommandResponse
{
    public Guid Id { get; private set; }
    public string? Titulo { get; private set; }
    public string? Categoria { get; private set; }
    public DateTime DataPublicacao { get; private set; }
    public string? Descricao { get; private set; }

    public ObterConteudoCommandResponse(Guid id, string? titulo, string? categoria, DateTime dataPublicacao, string? descricao)
    {
        Id = id;
        Titulo = titulo;
        Categoria = categoria;
        DataPublicacao = dataPublicacao;
        Descricao = descricao;
    }
}
