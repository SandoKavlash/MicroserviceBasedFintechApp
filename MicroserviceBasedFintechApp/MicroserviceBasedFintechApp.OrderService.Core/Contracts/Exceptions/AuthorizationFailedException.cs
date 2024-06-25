using MicroserviceBasedFintechApp.OrderService.Core.Contracts.Exceptions.Base;
namespace MicroserviceBasedFintechApp.OrderService.Core.Contracts.Exceptions
{
    public class AuthorizationFailedException : BaseException
    {
        public AuthorizationFailedException() : base(403, "User Auth failed")
        {

        }
    }
}
