using IdentityServer.Models.Context;
using IdentityServer.Models.DomainClasses;
using IdentityServer.Repository.Interfaces;

namespace IdentityServer.Repository.Implements
{
    public class ApiScopeRepository : GenericRepository<ApiScope>, IApiScopeRepository
    {
        public ApiScopeRepository(DatabaseContext context) : base(context) { }
    }
}
