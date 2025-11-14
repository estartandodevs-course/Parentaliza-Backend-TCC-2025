using Parentaliza.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class CriarEventoAgendaDtos
{

    [Required(ErrorMessage = "O título do evento é obrigatório")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "O evento deve ter entre 3 e 200 caracteres")]
    public string Evento { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tipo do evento é obrigatório")]
    [EnumDataType(typeof(TipoEvento), ErrorMessage = "Tipo de evento inválido")]
    public string Especialidade { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "O endereço não pode exceder 500 caracteres")]
    public string? Localizacao { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data do evento é obrigatório")]
    [DataType(DataType.DateTime)]
    public DateTime Data { get; set; } 

    [Required(ErrorMessage = "A hora do evento é obrigatório")]
    [DataType(DataType.DateTime)]
    public DateTime Hora { get; set; } 

    [StringLength(1000, ErrorMessage = "A descrição não pode exceder 1000 caracteres")]
    public string? Anotacao { get; set; } = string.Empty;

}
public class AtualizarEventoAgendaDtos
{
    [StringLength(200, MinimumLength = 3)]
    public string? Titulo { get; set; }

    [StringLength(1000)]
    public string? Descricao { get; set; }

    [EnumDataType(typeof(TipoEvento))]
    public TipoEvento? Tipo { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? DataHora { get; set; }

    [StringLength(500)]
    public string? Endereco { get; set; }

    public Guid? BebeId { get; set; }

    [StringLength(2000)]
    public string? Observacoes { get; set; }
}

public class MarcarEventoRealizadoDtos
{
    [Required(ErrorMessage = "A data de realização é obrigatória")]
    [DataType(DataType.DateTime)]
    public DateTime DataRealizacao { get; set; }

    [StringLength(2000, ErrorMessage = "As observações não podem exceder 2000 caracteres")]
    public string? ObservacoesRealizacao { get; set; }

    public List<string>? AnexosUrls { get; set; }
}

public class CancelarEventoDtos
{
    [Required(ErrorMessage = "O motivo do cancelamento é obrigatório")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "O motivo deve ter entre 3 e 500 caracteres")]
    public string MotivoCancelamento { get; set; } = string.Empty;
}

public class EventoResumoDtos
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? BebeNome { get; set; }
    public bool EventoHoje { get; set; }
    public int HorasAteEvento { get; set; }
}

public class FiltrarEventosDtos
{

    [EnumDataType(typeof(TipoEvento))]
    public TipoEvento? Tipo { get; set; }

    [EnumDataType(typeof(StatusEvento))]
    public StatusEvento? Status { get; set; }

    public Guid? BebeId { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DataInicio { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DataFim { get; set; }

    [StringLength(100)]
    public string? TextoBusca { get; set; }

    public string OrdenarPor { get; set; } = "DataAsc";

    [Range(1, int.MaxValue)]
    public int Pagina { get; set; } = 1;

    [Range(1, 100)]
    public int ItensPorPagina { get; set; } = 20;
}

public class EventosPaginadosDtos
{
    public int TotalItens { get; set; }
    public int TotalPaginas { get; set; }
    public int PaginaAtual { get; set; }
    public int ItensPorPagina { get; set; }
    public List<EventoResumoDtos> Eventos { get; set; } = new();

    public int EventosHoje { get; set; }
    public int EventosPendentes { get; set; }
    public int EventosRealizados { get; set; }
}

public class CalendarioMensalDtos
{
    public int Ano { get; set; }
    public int Mes { get; set; }
    public string NomeMes { get; set; } = string.Empty;
    public Dictionary<int, List<EventoResumoDtos>> EventosPorDia { get; set; } = new();
    public int TotalEventos { get; set; }
}