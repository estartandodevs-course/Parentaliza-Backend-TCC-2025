using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class EditarBebeGestacaoDtos
{
    [Required(ErrorMessage = "O ID do responsável é obrigatório")]
    public Guid ResponsavelId { get; set; }

    [Required(ErrorMessage = "O nome do bebê é obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A Data de Previsão é obrigatória")]
    [DataType(DataType.Date)]
    public DateTime DataPrevista { get; set; }

    [Required(ErrorMessage = "Os dias de gestação são obrigatórios")]
    [Range(0, 294, ErrorMessage = "Os dias de gestação devem estar entre 0 e 294 dias (42 semanas)")]
    public int DiasDeGestacao { get; set; }

    [Required(ErrorMessage = "O Peso estimado é obrigatório")]
    [Range(0.1, 20.0, ErrorMessage = "O peso deve estar entre 0.1 e 20.0 kg")]
    public decimal PesoEstimado { get; set; }

    [Required(ErrorMessage = "O comprimento estimado é obrigatório")]
    [Range(1.0, 100.0, ErrorMessage = "O comprimento deve estar entre 1.0 e 100.0 cm")]
    public decimal ComprimentoEstimado { get; set; }
}