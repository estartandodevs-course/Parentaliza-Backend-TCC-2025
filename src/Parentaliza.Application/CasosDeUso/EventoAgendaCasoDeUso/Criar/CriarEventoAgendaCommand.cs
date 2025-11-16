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
    public DateTime Data { get; set; } = DateTime.Now;
    public DateTime Hora { get; set; } = DateTime.Now;
    public string? Anotacao { get; set; }

    public ValidationResult ResultadoDasValidacoes { get; private set; } = new ValidationResult();
    public CriarEventoAgendaCommand(string? evento, string? especialidade, string? localizacao, DateTime data, DateTime hora, string? anotacao)
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
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O nome do fornecedor deve ser informado.");

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Especialidade)
           .NotEmpty()
           .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
           .WithMessage("O documento do fornecedor precisa ser informado");

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Localizacao)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O tipo do fornecedor precisa ser 1 (Pessoa Física) ou 2 (Pessoa Jurídica)");

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Data)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O tipo do fornecedor precisa ser 1 (Pessoa Física) ou 2 (Pessoa Jurídica)");

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Hora)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O tipo do fornecedor precisa ser 1 (Pessoa Física) ou 2 (Pessoa Jurídica)");

        validacoes.RuleFor(EventoAgenda => EventoAgenda.Anotacao)
            .NotEmpty()
            .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
            .WithMessage("O tipo do fornecedor precisa ser 1 (Pessoa Física) ou 2 (Pessoa Jurídica)");

        ResultadoDasValidacoes = validacoes.Validate(this);
        return ResultadoDasValidacoes.IsValid;
    }
}
