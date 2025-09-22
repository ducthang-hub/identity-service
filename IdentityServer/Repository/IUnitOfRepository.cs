using IdentityServer.Repository.Interfaces;

namespace IdentityServer.Repository
{
    public interface IUnitOfRepository
    {
        public IClientRepository Client { get; }
        // public IClientGrantTypeRepository ClientGrantType { get; }
        public IClientApiScopeRepository ClientApiScope { get; }
        // public IClientSecretRepository ClientSecret { get; }
        public IApiResourceRepository ApiResource { get; }
        public IApiScopeRepository ApiScope { get; }
        public IApiScopeResourceRepository ApiScopeResource { get; }
        public IUserRepository User { get; set; }
        public void SaveChanges();
        public void Dispose();
        public void Migrate();
    }
}
