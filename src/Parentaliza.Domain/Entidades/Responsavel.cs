using Parentaliza.Domain.Enums;
namespace Parentaliza.Domain.Entidades;
public class Responsavel : Entity
{
    public string? Nome { get; private set; }
    public string? Email { get; private set; }
    public TipoResponsavel TipoResponsavel { get; private set; }
    public string? Senha { get; private set; }
    public string? FaseNascimento { get; private set; }
    public Responsavel() { }
    public Responsavel(string? nome, string? email, int tipoResponsavel, string? senha, string? faseNascimento)
    {
        if (string.IsNullOrWhiteSpace(nome)) 
            throw new ArgumentException("O nome é obrigatório.", nameof(nome));
        
        if (string.IsNullOrWhiteSpace(email)) 
            throw new ArgumentException("O email é obrigatório.", nameof(email));
        
        if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            throw new ArgumentException("O email deve ter um formato válido.", nameof(email));
        
        if (string.IsNullOrWhiteSpace(senha)) 
            throw new ArgumentException("A senha é obrigatória.", nameof(senha));
        
        if (!Enum.IsDefined(typeof(TipoResponsavel), tipoResponsavel))
            throw new ArgumentException("O tipo de responsável é inválido.", nameof(tipoResponsavel));
        
        Nome = nome;
        Email = email;
        TipoResponsavel = (TipoResponsavel)tipoResponsavel;
        Senha = senha;
        FaseNascimento = faseNascimento;
    }
}