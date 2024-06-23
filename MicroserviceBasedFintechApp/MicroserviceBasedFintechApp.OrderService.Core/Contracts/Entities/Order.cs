using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities.Base;
using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Enums;


namespace MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities
{
    public class Order : BaseEntity
    {
        public decimal Amount { get; set; }
        public int? CompanyId { get; set; }
        public Currency Currency { get; set; }
        public Guid IdempotencyKey { get; set; }
        public Status? Status { get; set; }
        public Guid ApiKey { get; set; }
        public string SecretHashed { get; set; }
        public bool? Authenticated { get; set; }
    }
}
 