namespace MicroserviceBasedFintechApp.Identity.Core.Contracts.Response
{
    public class CreateCompanyResponse
    {
        public int Id { get; set; }
        public Guid ApiKey { get; set; }
        public Guid Secret { get; set; }
    }
}
