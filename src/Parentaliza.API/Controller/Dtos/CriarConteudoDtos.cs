using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class CriarConteudoDtos
{
    [Required(ErrorMessage = "O título do conteúdo é obrigatório")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 200 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "A categoria do conteúdo é obrigatória")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "A categoria deve ter entre 3 e 100 caracteres")]
    public string Categoria { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de publicação é obrigatória")]
    [DataType(DataType.DateTime, ErrorMessage = "A data de publicação deve ser uma data válida")]
    public DateTime DataPublicacao { get; set; }

    [Required(ErrorMessage = "A descrição do conteúdo é obrigatória")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "A descrição deve ter entre 10 e 2000 caracteres")]
    public string Descricao { get; set; } = string.Empty;
}
