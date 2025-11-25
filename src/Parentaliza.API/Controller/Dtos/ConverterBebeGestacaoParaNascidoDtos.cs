using Parentaliza.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class ConverterBebeGestacaoParaNascidoDtos
{
    [Required(ErrorMessage = "A Data de Nascimento é obrigatória")]
    [DataType(DataType.Date, ErrorMessage = "O tipo de data é inválido")]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O Sexo é obrigatório")]
    [EnumDataType(typeof(Sexo), ErrorMessage = "Sexo inválido")]
    public Sexo Sexo { get; set; }

    [Required(ErrorMessage = "O Tipo sanguíneo é obrigatório")]
    [EnumDataType(typeof(TipoSanguineo), ErrorMessage = "Tipo sanguíneo inválido")]
    public TipoSanguineo TipoSanguineo { get; set; }

    [Required(ErrorMessage = "A Idade em meses é obrigatória")]
    [Range(0, 120, ErrorMessage = "A idade deve estar entre 0 e 120 meses")]
    public int IdadeMeses { get; set; }

    [Required(ErrorMessage = "O Peso é obrigatório")]
    [Range(0.1, 20.0, ErrorMessage = "O peso deve estar entre 0.1 e 20.0 kg")]
    public decimal Peso { get; set; }

    [Required(ErrorMessage = "A Altura é obrigatória")]
    [Range(10.0, 100.0, ErrorMessage = "A altura deve estar entre 10.0 e 100.0 cm")]
    public decimal Altura { get; set; }

    [Required(ErrorMessage = "É necessário informar se deseja excluir o registro de gestação após a conversão")]
    public bool ExcluirBebeGestacao { get; set; } = true;
}

