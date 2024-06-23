using MicroserviceBasedFintechApp.Identity.Core.Abstractions.Repository;
using MicroserviceBasedFintechApp.Identity.Core.Abstractions.Service;
using MicroserviceBasedFintechApp.Identity.Core.Contracts.Entities;
using MicroserviceBasedFintechApp.Identity.Core.Contracts.Requests;
using MicroserviceBasedFintechApp.Identity.Core.Contracts.Responses;

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
    }
}
