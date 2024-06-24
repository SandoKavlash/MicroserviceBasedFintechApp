using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities.Base;

namespace MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities
{
    public class TransactionalOutBox : BaseEntity
    {
        public Guid ApiKey { get; set; }
        public Guid IdempotencyKey { get; set; }
    }
}
