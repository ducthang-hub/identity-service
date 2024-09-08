using IdentityServer.Repository.Interfaces;

namespace IdentityServer.Repository
{
    public interface IUnitOfRepository
    {
        public IClientRepository Client { get; }
        public IClientGrantTypeRepository ClientGrantType { get; }
        public IClientScopeRepository ClientScope { get; }
        public IApiResourceRepository ApiResource { get; }
        public IApiScopeRepository ApiScope { get; }
        public IApiScopeResourceRepository ApiScopeResource { get; }
        public IClientSecretRepository ClientSecret { get; }
        public IUserRepository User { get; set; }
        public void SaveChanges();
        public void Dispose();
        public void Migrate();
    }
}
