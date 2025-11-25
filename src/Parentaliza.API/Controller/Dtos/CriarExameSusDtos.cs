using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class CriarExameSusDtos
{
    [Required(ErrorMessage = "O nome do exame é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O nome do exame não pode exceder 200 caracteres.")]
    public string? NomeExame { get; set; }

    [MaxLength(1000, ErrorMessage = "A descrição não pode exceder 1000 caracteres.")]
    public string? Descricao { get; set; }

    [MaxLength(100, ErrorMessage = "A categoria de faixa etária não pode exceder 100 caracteres.")]
    public string? CategoriaFaixaEtaria { get; set; }

    [Range(0, 120, ErrorMessage = "A idade mínima deve estar entre 0 e 120 meses.")]
    public int? IdadeMinMesesExame { get; set; }

    [Range(0, 120, ErrorMessage = "A idade máxima deve estar entre 0 e 120 meses.")]
    public int? IdadeMaxMesesExame { get; set; }
}

