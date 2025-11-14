using Parentaliza.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;
public class CriarBebeNascidoDtos
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O evento deve ter entre 3 e 50 caracteres")]
    public string? Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Data de Nascimento é obrigatória")]
    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O Sexo é obrigatório")]
    [EnumDataType(typeof(SexoEnum), ErrorMessage = "Sexo inválido")]
    public SexoEnum Sexo { get; set; }

    [Required(ErrorMessage = "O Tipo sanguineo é obrigatório")]
    public TipoSanguineoEnum TipoSanguineo { get; set; }

    [Required(ErrorMessage = "A Idade em meses é obrigatória")]
    [Range(0, 11, ErrorMessage = "A idade em meses deve estar entre 0 e 11")]
    public int? IdadeMeses { get; set; } 

    [Required(ErrorMessage = "O Peso é obrigatório")]
    [Range(0.1, 20.0, ErrorMessage = "O peso deve estar entre 0.1 e 20.0 kg")]
    public decimal Peso { get; set; }

    [Required(ErrorMessage = "A Altura é obrigatória")]
    [Range(10.0, 100.0, ErrorMessage = "A altura deve estar entre 10.0 e 100.0 cm")]
    public decimal Altura { get; set; }
}