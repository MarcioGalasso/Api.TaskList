using Microsoft.Extensions.Configuration;
using SharedKernel.Domain.Entities;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Services.Interface;
using SharedKernel.Domain.Validation;

namespace SharedKernel.Domain.Services
{
    public class ApplicationUserService : QueryService<ApplicationUser>, IApplicationUserService
    {
        public ApplicationUserService(IQueryRepository<ApplicationUser> repository, IConfiguration configuration,
            IRestService restService) : base(repository, configuration, restService)
        {
            
        }
    }
}
