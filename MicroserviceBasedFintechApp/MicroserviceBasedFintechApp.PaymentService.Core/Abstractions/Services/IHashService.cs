namespace MicroserviceBasedFintechApp.PaymentService.Core.Abstractions.Services
{
    public interface IHashService
    {
        string Hash(string text);
    }
}
