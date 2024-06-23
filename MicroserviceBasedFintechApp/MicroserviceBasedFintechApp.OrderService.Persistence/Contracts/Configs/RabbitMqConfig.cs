namespace MicroserviceBasedFintechApp.OrderService.Persistence.Contracts.Configs
{
    public class RabbitMqConfig
    {
        public string Host { get;set; }
        public string UserName { get;set; }
        public string Password { get;set; }
        public int Port { get; set; }
        public string AuthenticationExchange { get; set; }
        public string AuthenticationResponseQueue { get; set; }
    }
}
