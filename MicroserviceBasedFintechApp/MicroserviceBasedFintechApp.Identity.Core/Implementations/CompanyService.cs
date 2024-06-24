using MicroserviceBasedFintechApp.Identity.Core.Abstractions.Repository;
using MicroserviceBasedFintechApp.Identity.Core.Abstractions.Service;
using MicroserviceBasedFintechApp.Identity.Core.Contracts.Entities;
using MicroserviceBasedFintechApp.Identity.Core.Contracts.Models;
using MicroserviceBasedFintechApp.Identity.Core.Contracts.Requests;
using MicroserviceBasedFintechApp.Identity.Core.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceBasedFintechApp.Identity.Core.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly IGenericRepository<Company> _companyRepo;
        private readonly IHashService _hashService;
        public CompanyService(
            IGenericRepository<Company> companyRepo,
            IHashService hashService)
        {
            _companyRepo = companyRepo;
            _hashService = hashService;
        }
        public async Task<CreateCompanyResponse> CreateCompany(CreateCompanyRequest request)
        {
            Guid apiKey = Guid.NewGuid();
            Guid secret  = Guid.NewGuid();
            Company company = new Company()
            {
                ApiKey = apiKey,
                Name = request.CompanyName,
                HashedSecret = _hashService.Hash(secret.ToString()),
                UpdateDateAtUtc = DateTime.UtcNow,
                CreationDateAtUtc = DateTime.UtcNow
            };
            await _companyRepo.InsertAsync(company);

            await _companyRepo.SaveChangesAsync();


            return new CreateCompanyResponse()
            {
                Secret = secret,
                ApiKey = apiKey,
                Id = company.Id
            };
        }

        public async Task<AuthenticateCompanyResponse> IsAuthenticatedCompany(AuthenticateCompanyRequest request)
        {
            Company? company = _companyRepo.GetQueryable().SingleOrDefault(c => c.ApiKey == request.ApiKey && c.HashedSecret == request.HashedSecret);
            if (company == null) return new AuthenticateCompanyResponse() { CompanyId = -1 };

            return new AuthenticateCompanyResponse { CompanyId = company.Id };
        }
    }
}
