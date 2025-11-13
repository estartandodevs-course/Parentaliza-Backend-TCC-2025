using Parentaliza.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parentaliza.API.Controller.Dtos;

public class CriarEventoAgendaDto
{

    [Required(ErrorMessage = "O título do evento é obrigatório")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 200 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "A descrição não pode exceder 1000 caracteres")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O tipo do evento é obrigatório")]
    [EnumDataType(typeof(TipoEvento), ErrorMessage = "Tipo de evento inválido")]
    public TipoEvento Tipo { get; set; }

    [Required(ErrorMessage = "A data e hora do evento são obrigatórias")]
    [DataType(DataType.DateTime)]
    public DateTime DataHora { get; set; }

    [StringLength(500, ErrorMessage = "O endereço não pode exceder 500 caracteres")]
    public string? Endereco { get; set; }

    public Guid? BebeId { get; set; }

    public bool EventoRecorrente { get; set; } = false;

    [StringLength(2000, ErrorMessage = "As observações não podem exceder 2000 caracteres")]
    public string? Observacoes { get; set; }

    [Required(ErrorMessage = "O ID do usuário responsável é obrigatório")]
    public Guid UsuarioId { get; set; }
}

public class EventoAgendaResponseDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public DateTime DataHora { get; set; }
    public string? Endereco { get; set; }
    public DateTime DataCriacao { get; set; }

    public Guid? BebeId { get; set; }
    public string? BebeNome { get; set; }

    public bool EventoPassado { get; set; }
    public bool EventoHoje { get; set; }
    public int DiasAteEvento { get; set; }
    public int HorasAteEvento { get; set; }
}


public class AtualizarEventoAgendaDto
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

public class MarcarEventoRealizadoDto
{
    [Required(ErrorMessage = "A data de realização é obrigatória")]
    [DataType(DataType.DateTime)]
    public DateTime DataRealizacao { get; set; }

    [StringLength(2000, ErrorMessage = "As observações não podem exceder 2000 caracteres")]
    public string? ObservacoesRealizacao { get; set; }

    public List<string>? AnexosUrls { get; set; }
}

public class CancelarEventoDto
{
    [Required(ErrorMessage = "O motivo do cancelamento é obrigatório")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "O motivo deve ter entre 3 e 500 caracteres")]
    public string MotivoCancelamento { get; set; } = string.Empty;
}

public class EventoResumoDto
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? BebeNome { get; set; }
    public bool EventoHoje { get; set; }
    public int HorasAteEvento { get; set; }
}

public class FiltrarEventosDto
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

public class EventosPaginadosDto
{
    public int TotalItens { get; set; }
    public int TotalPaginas { get; set; }
    public int PaginaAtual { get; set; }
    public int ItensPorPagina { get; set; }
    public List<EventoResumoDto> Eventos { get; set; } = new();

    public int EventosHoje { get; set; }
    public int EventosPendentes { get; set; }
    public int EventosRealizados { get; set; }
}

public class CalendarioMensalDto
{
    public int Ano { get; set; }
    public int Mes { get; set; }
    public string NomeMes { get; set; } = string.Empty;
    public Dictionary<int, List<EventoResumoDto>> EventosPorDia { get; set; } = new();
    public int TotalEventos { get; set; }
}