using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ExameSusCasoDeUso.Listar;

public class ListarExameSusCommand : IRequest<CommandResponse<List<ListarExameSusCommandResponse>>>
{
}

