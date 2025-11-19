using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class CriarBebeGestacaoDtos
{
    [Required(ErrorMessage = "O nome do bebê é obrigatório")]
    [MaxLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres")]
    public string? Nome { get; private set; }

    [Required(ErrorMessage = "A Data de Previsão é obrigatória")]
    [DataType(DataType.Date)]
    public DateTime DataPrevista { get; private set; }

    [Required(ErrorMessage = "Os dias de gestaçao é obrigatório")]
    [Range(1, 300, ErrorMessage = "Os dias de gestação não deve exceder 300 caracteres")]
    public int DiasDeGestacao { get; private set; }

    [Required(ErrorMessage = "O Peso estimado é obrigatório")]
    [Range(0.1, 20.0, ErrorMessage = "O peso deve estar entre 0.1 e 10.0 kg")]
    public decimal PesoEstimado { get; private set; }

    [Required(ErrorMessage = "O comprimento estimado é obrigatório")]
    [Range(1.0, 100.0, ErrorMessage = "O comprimento deve estar entre 1.0 e 100.0 cm")]
    public decimal ComprimentoEstimado { get; private set; }
}