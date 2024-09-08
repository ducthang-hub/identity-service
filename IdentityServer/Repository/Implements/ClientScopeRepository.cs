using IdentityServer.Models.Context;
using IdentityServer.Models.DomainClasses;
using IdentityServer.Repository.Interfaces;

namespace IdentityServer.Repository.Implements
{
    public class ClientScopeRepository : GenericRepository<ClientScope>, IClientScopeRepository
    {
        public ClientScopeRepository(DatabaseContext context) : base(context) { }

    }
}
