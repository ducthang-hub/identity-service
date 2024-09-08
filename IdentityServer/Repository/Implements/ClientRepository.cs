using IdentityServer.Models.Context;
using IdentityServer.Models.DomainClasses;
using IdentityServer.Repository.Interfaces;

namespace IdentityServer.Repository.Implements
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
