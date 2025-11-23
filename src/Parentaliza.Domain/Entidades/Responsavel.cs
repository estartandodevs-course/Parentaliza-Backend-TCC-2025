using Parentaliza.Domain.Enums;
namespace Parentaliza.Domain.Entidades;
public class Responsavel : Entity
{
    public Guid? ResponsavelId { get; private set; }
    public string? Nome { get; private set; }
    public string? Email { get; private set; }
    public TiposEnum TipoResponsavel { get; private set; }
    public string? Senha { get; private set; }
    public string? FaseNascimento { get; private set; }
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