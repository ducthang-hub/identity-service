using IdentityServer.Models.Context;
using IdentityServer.Repository.Implements;
using IdentityServer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Repository
{
    public class UnitOfRepository : IUnitOfRepository
    {
        private readonly DatabaseContext _context;
        public UnitOfRepository(DatabaseContext context)
        {
            _context = context;
            Client = new ClientRepository(context);
            ClientGrantType = new ClientGrantTypeRepository(context);
            ClientScope = new ClientScopeRepository(context);
            ClientSecret = new ClientSecretRepository(context);
            ApiResource = new ApiResourceRepository(context);
            ApiScope = new ApiScopeRepository(context);
            ApiScopeResource = new ApiScopeResourceRepository(context);
            User = new UserRepository(context);
        }

        public IClientRepository Client {get;}
        public IClientGrantTypeRepository ClientGrantType {get;}
        public IClientScopeRepository ClientScope {get;}
        public IApiResourceRepository ApiResource {get;}
        public IApiScopeRepository ApiScope {get;}
        public IApiScopeResourceRepository ApiScopeResource {get;}
        public IClientSecretRepository ClientSecret { get; }
        public IUserRepository User { get; set; }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Migrate()
        {
            _context.Database.Migrate();
        }
    }
}
