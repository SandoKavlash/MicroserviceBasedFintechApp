namespace MicroserviceBasedFintechApp.Identity.Persistence.Contracts.Configs
{
    public class RabbitMqConfig
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string AuthenticationExchange { get; set; }

        public string AuthenticationResponseQueue { get; set; }
        public string AuthenticationResponseQueueRoutingKey { get; set; }

        public string AuthenticationRequestQueue { get; set; }
        public string AuthenticationRequestQueueRoutingKey { get; set; }
    }
}
