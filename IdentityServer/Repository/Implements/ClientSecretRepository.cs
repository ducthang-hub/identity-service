using IdentityServer.Models.Context;
using IdentityServer.Models.DomainClasses;
using IdentityServer.Repository.Interfaces;

namespace IdentityServer.Repository.Implements
{
    public class ClientSecretRepository : GenericRepository<ClientSecret>, IClientSecretRepository
    {
        public ClientSecretRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
