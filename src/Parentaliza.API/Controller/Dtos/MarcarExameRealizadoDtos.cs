using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class MarcarExameRealizadoDtos
{
    [Required(ErrorMessage = "A data de realização é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "O tipo de data é inválido.")]
    public DateTime DataRealizacao { get; set; }

    [MaxLength(500, ErrorMessage = "As observações não podem exceder 500 caracteres.")]
    public string? Observacoes { get; set; }
}

