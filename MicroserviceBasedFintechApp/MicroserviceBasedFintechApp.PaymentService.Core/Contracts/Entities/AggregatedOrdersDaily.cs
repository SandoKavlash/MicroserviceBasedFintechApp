using MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities.Base;

namespace MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Entities
{
    public class AggregatedOrdersDaily: BaseEntity
    {
        public Guid ApiKey { get; set; }
        public DateTime DateAggregationUTC { get; set; }
        public decimal Amount { get; set; }
    }
}
