namespace MicroserviceBasedFintechApp.OrderService.Core.Contracts.Requests
{
    public class GetOrderRequest
    {
        public int OrderId { get; set; }
        public Guid ApiKey { get; set; }
        public Guid Secret { get; set; }
    }
}
