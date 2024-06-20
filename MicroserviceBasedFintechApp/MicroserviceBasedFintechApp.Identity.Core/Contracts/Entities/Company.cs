using MicroserviceBasedFintechApp.Identity.Core.Contracts.Entities.Base;

namespace MicroserviceBasedFintechApp.Identity.Core.Contracts.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public Guid ApiKey { get; set; }
        public string HashedSecret { get; set; }
    }
}
