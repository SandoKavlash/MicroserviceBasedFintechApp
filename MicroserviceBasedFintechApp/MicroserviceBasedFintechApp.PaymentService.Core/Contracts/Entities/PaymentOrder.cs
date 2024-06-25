using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities.Base;
using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Enums;

namespace MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities
{
    public class PaymentOrder : BaseEntity
    {
        public decimal Amount { get; set; }
        public int? CompanyId { get; set; }
        public Currency Currency { get; set; }
        public Guid IdempotencyKey { get; set; }
        public Status? Status { get; set; }
        public Guid ApiKey { get; set; }
        public string SecretHashed { get; set; }
        public bool? Authenticated { get; set; }
        public bool OrderServiceNotifier { get; set; }
        public bool IsPaid { get; set; }
    }
}
