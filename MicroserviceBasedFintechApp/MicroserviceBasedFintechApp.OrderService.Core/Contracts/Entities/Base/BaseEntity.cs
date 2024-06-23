namespace MicroserviceBasedFintechApp.OrderService.Core.Contracts.Entities.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime UpdateDateAtUtc { get; set; }
        public DateTime CreationDateAtUtc { get; set; }
    }
}
