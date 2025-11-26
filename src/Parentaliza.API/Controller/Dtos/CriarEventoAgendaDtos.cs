using Parentaliza.API.Controller.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class CriarEventoAgendaDtos
{
    [Required(ErrorMessage = "O ID do responsável é obrigatório")]
    public Guid ResponsavelId { get; set; }

    [Required(ErrorMessage = "O título do evento é obrigatório")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O evento deve ter entre 3 e 100 caracteres")]
    public string? Evento { get; set; }

    [Required(ErrorMessage = "A especialidade do evento é obrigatória")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "A especialidade não deve exceder 100 caracteres")]
    public string? Especialidade { get; set; }

    [Required(ErrorMessage = "A localização do evento é obrigatória")]
    [StringLength(500, ErrorMessage = "A localização não pode exceder 500 caracteres")]
    public string? Localizacao { get; set; }

    [Required(ErrorMessage = "A data do evento é obrigatória")]
    [DataType(DataType.Date, ErrorMessage = "Data inválida")]
    [DataHoraFutura(nameof(Hora), ErrorMessage = "A data e hora do evento não podem ser no passado")]
    public DateTime Data { get; set; }

    [Required(ErrorMessage = "O horário do evento é obrigatório")]
    [DataType(DataType.Time, ErrorMessage = "Horário inválido")]
    public TimeSpan Hora { get; set; }

    [StringLength(1000, ErrorMessage = "A descrição não pode exceder 1000 caracteres")]
    public string? Anotacao { get; set; }
}