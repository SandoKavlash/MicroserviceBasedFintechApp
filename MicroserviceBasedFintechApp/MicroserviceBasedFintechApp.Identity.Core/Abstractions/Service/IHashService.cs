namespace MicroserviceBasedFintechApp.Identity.Core.Abstractions.Service
{
    public interface IHashService
    {
        string Hash(string text);
    }
}
