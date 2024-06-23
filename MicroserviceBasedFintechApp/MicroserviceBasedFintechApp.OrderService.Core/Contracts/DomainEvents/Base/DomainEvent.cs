using MediatR;

namespace MicroserviceBasedFintechApp.OrderService.Core.Contracts.DomainEvents.Base
{
    public abstract record DomainEvent : INotification;
}
