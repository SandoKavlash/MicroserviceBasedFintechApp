using MicroserviceBasedFintechApp.Identity.Core.Contracts.Enums;

namespace MicroserviceBasedFintechApp.Identity.Core.Contracts.Models
{
    public class Order
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
