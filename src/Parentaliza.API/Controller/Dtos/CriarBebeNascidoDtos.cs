using Parentaliza.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;
public class CriarBebeNascidoDtos
{
    [Required(ErrorMessage = "O nome do bebê é obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres")]
    public string? Nome { get; private set; }

    [Required(ErrorMessage = "A Data de Nascimento é obrigatória")]
    [DataType(DataType.Date, ErrorMessage = "O tipo de data e inválido")]
    public DateTime DataNascimento { get; private set; }

    [Required(ErrorMessage = "O Sexo é obrigatório")]
    [EnumDataType(typeof(SexoEnum), ErrorMessage = "Sexo inválido")]
    public SexoEnum Sexo { get; private set; }

    [Required(ErrorMessage = "O Tipo sanguineo é obrigatório")]
    [EnumDataType(typeof(TipoSanguineoEnum), ErrorMessage = "Tipo sanguíneo inválido")]
    public TipoSanguineoEnum TipoSanguineo { get; private set; }

    [Required(ErrorMessage = "A Idade é obrigatória")]
    public int IdadeMeses { get; private set; }

    [Required(ErrorMessage = "O Peso é obrigatório")]
    [Range(0.1, 20.0, ErrorMessage = "O peso deve estar entre 0.1 e 20.0 kg")]
    public decimal Peso { get; private set; }

    [Required(ErrorMessage = "A Altura é obrigatória")]
    [Range(10.0, 100.0, ErrorMessage = "A altura deve estar entre 10.0 e 100.0 cm")]
    public decimal Altura { get; private set; }
}