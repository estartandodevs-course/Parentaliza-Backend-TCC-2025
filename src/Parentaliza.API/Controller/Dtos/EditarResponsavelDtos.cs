using System.ComponentModel.DataAnnotations;
using Parentaliza.Domain.Enums;

namespace Parentaliza.API.Controller.Dtos;

public class EditarResponsavelDtos
{
    [Required(ErrorMessage = "O nome do responsável é obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "O email deve ser válido")]
    [MaxLength(255, ErrorMessage = "O email não pode exceder 255 caracteres")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O tipo de responsável é obrigatório")]
    public TiposEnum TipoResponsavel { get; set; }

    [MaxLength(100, ErrorMessage = "A senha não pode exceder 100 caracteres")]
    public string? Senha { get; set; }

    [MaxLength(50, ErrorMessage = "A fase de nascimento não pode exceder 50 caracteres")]
    public string? FaseNascimento { get; set; }
}

