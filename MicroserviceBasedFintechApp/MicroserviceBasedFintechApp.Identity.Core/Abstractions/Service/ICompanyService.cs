using MicroserviceBasedFintechApp.Identity.Core.Contracts.Requests;
using MicroserviceBasedFintechApp.Identity.Core.Contracts.Response;

namespace MicroserviceBasedFintechApp.Identity.Core.Abstractions.Service
{
    public interface ICompanyService
    {
        Task<CreateCompanyResponse> CreateCompany(CreateCompanyRequest request);
    }
}
