using MediatR;
using Parentaliza.Application.Mediator;

namespace Parentaliza.Application.CasosDeUso.VacinaSusCasoDeUso.Listar;

public class ListarVacinaSusCommand : IRequest<CommandResponse<List<ListarVacinaSusCommandResponse>>> { }