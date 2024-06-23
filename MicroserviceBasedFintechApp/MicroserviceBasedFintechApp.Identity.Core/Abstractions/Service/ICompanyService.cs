using MicroserviceBasedFintechApp.Identity.Core.Contracts.Requests;
using MicroserviceBasedFintechApp.Identity.Core.Contracts.Responses;

namespace MicroserviceBasedFintechApp.Identity.Core.Abstractions.Service
{
    public interface ICompanyService
    {
        Task<CreateCompanyResponse> CreateCompany(CreateCompanyRequest request);
    }
}
