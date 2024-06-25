namespace MicroserviceBasedFintechApp.PaymentService.Core.Contracts.Models
{
    public class PaymentModel
    {
        public int OrderId { get; set; }
        public string CardNumber { get; set; }
        public DateTime CardExpirationDate { get; set; }
        public Guid ApiKey { get; set; }
        public Guid Secret { get; set; }
    }
}
