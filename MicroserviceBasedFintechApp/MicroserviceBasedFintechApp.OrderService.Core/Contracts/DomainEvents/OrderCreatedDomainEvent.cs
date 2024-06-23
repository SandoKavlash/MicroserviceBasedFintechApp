using MediatR;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.DomainEvents.Base;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Enums;

namespace MicroserviceBasedFintechApp.OrderService.Core.Contracts.DomainEvents
{
    public record OrderCreatedDomainEvent(Guid ApiKey, string HashedSecret, decimal Amount, Currency Currency, Guid IdempotencyKey) : DomainEvent;
}
