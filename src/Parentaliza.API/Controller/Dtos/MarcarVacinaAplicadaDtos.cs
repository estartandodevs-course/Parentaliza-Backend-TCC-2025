using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class MarcarVacinaAplicadaDtos
{
    [Required(ErrorMessage = "A data de aplicação é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "O tipo de data é inválido.")]
    public DateTime DataAplicacao { get; set; }

    [MaxLength(50, ErrorMessage = "O lote não pode exceder 50 caracteres.")]
    public string? Lote { get; set; }

    [MaxLength(100, ErrorMessage = "O local de aplicação não pode exceder 100 caracteres.")]
    public string? LocalAplicacao { get; set; }

    [MaxLength(500, ErrorMessage = "As observações não podem exceder 500 caracteres.")]
    public string? Observacoes { get; set; }
}