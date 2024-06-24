namespace MicroserviceBasedFintechApp.Identity.Core.Contracts.Requests
{
    public class AuthenticateCompanyRequest
    {
        public Guid ApiKey { get; set; }
        public string HashedSecret { get; set; }
    }
}
