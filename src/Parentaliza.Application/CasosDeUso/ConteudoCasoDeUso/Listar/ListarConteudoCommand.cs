using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.ConteudoCasoDeUso.Listar;

public class ListarConteudoCommand : IRequest<CommandResponse<List<ListarConteudoCommandResponse>>> { }