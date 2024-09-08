using IdentityServer.Models.Context;
using IdentityServer.Models.DomainClasses;
using IdentityServer.Repository.Interfaces;

namespace IdentityServer.Repository.Implements
{
    public class ApiScopeResourceRepository : GenericRepository<ApiScopeResource>, IApiScopeResourceRepository
    {
        public ApiScopeResourceRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
