using IdentityServer.Models.Context;
using IdentityServer.Models.DomainClasses;
using IdentityServer.Repository.Interfaces;

namespace IdentityServer.Repository.Implements
{
    public class UserRepository : GenericRepository<User>, IUserRepository 
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
