using IdentityServer.Models.Context;
using IdentityServer.Models.DomainClasses;
using IdentityServer.Repository.Interfaces;

namespace IdentityServer.Repository.Implements
{
    public class ClientApiScopeRepository : GenericRepository<ClientApiScope>, IClientApiScopeRepository
    {
        public ClientApiScopeRepository(DatabaseContext context) : base(context) { }

    }
}
