using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Parentaliza.Application.Mediator;
using System.Net;

namespace Parentaliza.Application.CasosDeUso.EventoAgendaCasoDeUso.Criar;

public class CriarEventoAgendaCommand : IRequest<CommandResponse<CriarEventoAgendaCommandResponse>>
{
    public string? Evento { get; set; }
    public string? Especialidade { get; set; }
    public string? Localizacao { get; set; }
    public DateTime Data { get; set; }
    public DateTime Horario { get; set; }
    public string? Anotacao { get; set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();
    public CriarEventoAgendaCommand(string? evento, string? especialidade, string? localizacao, DateTime data, DateTime horario, string? anotacao)
    {
        Evento = evento;
        Especialidade = especialidade;
        Localizacao = localizacao;
        Data = data;
        Horario = horario;
        Anotacao = anotacao;
    }
    public bool Validar()
    {
        var validacoes = new InlineValidator<CriarEventoAgendaCommand>();

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Evento)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O título do evento é obrigatório.")
            .MaximumLength(200)
            .WithMessage("O evento deve ter no máximo 200 caracteres.")
            .MinimumLength(3)
            .WithMessage("O evento deve ter no mínimo 3 caracteres.");

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Especialidade)
           .NotEmpty()
           .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
           .WithMessage("A especialidade do evento é obrigatória.");

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Localizacao)
            .MaximumLength(500)
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("A localização não pode exceder 500 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Localizacao));

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Data)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("A data do evento é obrigatória.");

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Horario)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O horário do evento é obrigatório.");

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Anotacao)
            .MaximumLength(1000)
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("A anotação não pode exceder 1000 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Anotacao));

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}
