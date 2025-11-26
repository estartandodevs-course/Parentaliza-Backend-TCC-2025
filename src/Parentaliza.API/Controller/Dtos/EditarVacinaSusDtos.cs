using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class EditarVacinaSusDtos
{
    [Required(ErrorMessage = "O nome da vacina é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O nome da vacina não pode exceder 200 caracteres.")]
    public string? NomeVacina { get; set; }

    [MaxLength(1000, ErrorMessage = "A descrição não pode exceder 1000 caracteres.")]
    public string? Descricao { get; set; }

    [MaxLength(100, ErrorMessage = "A categoria de faixa etária não pode exceder 100 caracteres.")]
    public string? CategoriaFaixaEtaria { get; set; }

    [Range(0, 120, ErrorMessage = "A idade mínima deve estar entre 0 e 120 meses.")]
    public int? IdadeMinMesesVacina { get; set; }

    [Range(0, 120, ErrorMessage = "A idade máxima deve estar entre 0 e 120 meses.")]
    public int? IdadeMaxMesesVacina { get; set; }
}