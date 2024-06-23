using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Enums;

namespace MicroserviceBasedFintechApp.OrderService.Core.Contracts.Requests
{
    public class OrderCreateRequest
    {
        public Guid ApiKey { get; set; }
        public Guid Secret { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public Guid IdempotencyKey { get; set; }
    }
}
