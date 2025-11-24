using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Editar;

public class EditarEventoAgendaCommand : IRequest<CommandResponse<EditarEventoAgendaCommandResponse>>
{
    public Guid Id { get; private set; }
    public Guid ResponsavelId { get; private set; }
    public string? Evento { get; private set; }
    public string? Especialidade { get; private set; }
    public string? Localizacao { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Hora { get; private set; }
    public string? Anotacao { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();

    public EditarEventoAgendaCommand(Guid id, Guid responsavelId, string? evento, string? especialidade, string? localizacao, DateTime data, TimeSpan hora, string? anotacao)
    {
        Id = id;
        ResponsavelId = responsavelId;
        Evento = evento;
        Especialidade = especialidade;
        Localizacao = localizacao;
        Data = data;
        Hora = hora;
        Anotacao = anotacao;
    }

    public bool Validar()
    {
        var validacoes = new InlineValidator<EditarEventoAgendaCommand>();

        validacoes.RuleFor(EventoAgenda => EventoAgenda.ResponsavelId)
            .NotEqual(Guid.Empty).WithMessage("O ID do responsável é obrigatório")
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Evento)
            .NotEmpty().WithMessage("O evento ou consulta deve ser informado.")
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Especialidade)
           .NotEmpty().WithMessage("A especialidade do evento ou consulta deve ser informada.")
           .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Localizacao)
            .NotEmpty().WithMessage("A localização do evento ou consulta deve ser informada.")
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Data)
            .NotEqual(default(DateTime)).WithMessage("A data do evento ou consulta deve ser informada.")
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Anotacao)
            .MaximumLength(1000)
            .When(e => !string.IsNullOrEmpty(e.Anotacao))
            .WithMessage("A descrição não pode exceder 1000 caracteres.")
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}
