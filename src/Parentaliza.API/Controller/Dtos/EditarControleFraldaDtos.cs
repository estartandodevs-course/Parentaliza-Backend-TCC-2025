using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class EditarControleFraldaDtos
{
    [Required(ErrorMessage = "O ID do bebê é obrigatório.")]
    public Guid BebeNascidoId { get; set; }

    [Required(ErrorMessage = "A hora da troca é obrigatória.")]
    [DataType(DataType.DateTime, ErrorMessage = "O tipo de data/hora é inválido.")]
    public DateTime HoraTroca { get; set; }

    [MaxLength(50, ErrorMessage = "O tipo de fralda não pode exceder 50 caracteres.")]
    public string? TipoFralda { get; set; }

    [MaxLength(500, ErrorMessage = "As observações não podem exceder 500 caracteres.")]
    public string? Observacoes { get; set; }
}

