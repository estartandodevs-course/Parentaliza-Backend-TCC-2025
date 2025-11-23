using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Criar;

public class CriarEventoAgendaCommand : IRequest<CommandResponse<CriarEventoAgendaCommandResponse>>
{
    public string? Evento { get; private set; }
    public string? Especialidade { get; private set; }
    public string? Localizacao { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan Hora { get; private set; }
    public string? Anotacao { get; private set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();
    public CriarEventoAgendaCommand(string? evento, string? especialidade, string? localizacao, DateTime data, TimeSpan hora, string? anotacao)
    {
        Evento = evento;
        Especialidade = especialidade;
        Localizacao = localizacao;
        Data = data;
        Hora = hora;
        Anotacao = anotacao;
    }
    public bool Validar()
    {
        var validacoes = new InlineValidator<CriarEventoAgendaCommand>();

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Evento)
            .NotEmpty().WithMessage("O Evento ou consulta precisa ser informado")
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Especialidade)
           .NotEmpty().WithMessage("A especialidade do evento ou consulta precisa ser informado")
           .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Localizacao)
            .NotEmpty().WithMessage("A Localização do evento ou consulta deve ser informado")
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Data)
            .NotEmpty().WithMessage("A Data do evento ou consulta deve ser informada")
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());


        validacoes.RuleFor(EventoAgenda => EventoAgenda.Hora)
            .NotEmpty().WithMessage("A Hora do evento ou consulta deve ser informada")
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Anotacao)
            .NotEmpty().WithMessage("Uma breve descrição do evento ou consulta deve ser informada")
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString());

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}