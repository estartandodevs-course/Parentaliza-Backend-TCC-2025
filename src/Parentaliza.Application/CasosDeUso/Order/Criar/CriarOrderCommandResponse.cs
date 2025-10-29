using Parentaliza.Domain.Entidades;

namespace Parentaliza.Application.CasosDeUso.Order.Criar;

public class CriarOrderCommandResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public OrderEntity? Order { get; set; }
}

