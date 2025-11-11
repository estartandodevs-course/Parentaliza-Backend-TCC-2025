using Parentaliza.Domain.Enums;
namespace Parentaliza.Domain.Entidades;
public class Responsavel : Entity
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public TiposEnum TipoResponsavel { get; set; }
    public string? Senha { get; set; }
    public string? FaseNascimento { get; set; }
    public Responsavel() { }
    public Responsavel(string? nome, string? email, int tipoResponsavel, string? senha, string? faseNascimento)
    {
        Nome = nome;
        Email = email;
        TipoResponsavel = (TiposEnum)tipoResponsavel;
        Senha = senha;
        FaseNascimento = faseNascimento;
    }
}