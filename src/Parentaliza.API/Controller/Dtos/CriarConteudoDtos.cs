using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class CriarConteudoDtos
{
    [Required(ErrorMessage = "O título do conteúdo é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 100 caracteres")]
    public string? Titulo { get; set; }

    [Required(ErrorMessage = "A categoria do conteúdo é obrigatória")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "A categoria deve ter entre 3 e 100 caracteres")]
    public string? Categoria { get; set; }

    [Required(ErrorMessage = "A data de publicação é obrigatória")]
    [DataType(DataType.Date, ErrorMessage = "A data de publicação deve ser uma data válida")]
    public DateTime DataPublicacao { get; set; }

    [Required(ErrorMessage = "A descrição do conteúdo é obrigatória")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "A descrição não deve exceder 2000 caracteres")]
    public string? Descricao { get; set; }
}