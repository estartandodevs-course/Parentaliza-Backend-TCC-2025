using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class CriarControleLeiteMaternoDtos
{
    [Required(ErrorMessage = "O ID do bebê é obrigatório.")]
    public Guid BebeNascidoId { get; set; }

    [Required(ErrorMessage = "O cronômetro é obrigatório.")]
    [DataType(DataType.DateTime, ErrorMessage = "O tipo de data/hora é inválido.")]
    public DateTime Cronometro { get; set; }

    [MaxLength(50, ErrorMessage = "O lado direito não pode exceder 50 caracteres.")]
    public string? LadoDireito { get; set; }

    [MaxLength(50, ErrorMessage = "O lado esquerdo não pode exceder 50 caracteres.")]
    public string? LadoEsquerdo { get; set; }
}

