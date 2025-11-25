using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class EditarControleMamadeiraDtos
{
    [Required(ErrorMessage = "O ID do bebê é obrigatório.")]
    public Guid BebeNascidoId { get; set; }

    [Required(ErrorMessage = "A data é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "O tipo de data é inválido.")]
    public DateTime Data { get; set; }

    [Required(ErrorMessage = "A hora é obrigatória.")]
    public TimeSpan Hora { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "A quantidade de leite não pode ser negativa.")]
    public decimal? QuantidadeLeite { get; set; }

    [MaxLength(500, ErrorMessage = "A anotação não pode exceder 500 caracteres.")]
    public string? Anotacao { get; set; }
}

