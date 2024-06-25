using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Exceptions.Base;

namespace MicroserviceBasedFintechApp.OrderService.Core.Contracts.Exceptions
{
    public class OrderNotFoundException : BaseException
    {
        public OrderNotFoundException() : base(404,"Order not found")
        {

        }
    }
}
