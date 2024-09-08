using IdentityServer.Models.Context;
using IdentityServer.Models.DomainClasses;
using IdentityServer.Repository.Interfaces;

namespace IdentityServer.Repository.Implements
{
    public class ApiResourceRepository : GenericRepository<ApiResource>, IApiResourceRepository
    {
        public ApiResourceRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
