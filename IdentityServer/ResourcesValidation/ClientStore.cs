using IdentityServer.Repository;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using Client = IdentityServer4.Models.Client;

namespace IdentityServer.ResourcesValidation
{
    public class ClientStore : IClientStore
    {
        private readonly IUnitOfRepository _uor;
        public ClientStore(IUnitOfRepository uor)
        {
            _uor = uor;            
        }
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            try
            {
                var client = await _uor.Client.Where(i => i.ClientId == clientId).FirstOrDefaultAsync();

                if(client == null)
                {
                    return new Client();
                }

                var apiScopes = await (
                        from apiScope in _uor.ApiScope.GetAll()
                        join clientApiScope in _uor.ClientApiScope.GetAll()
                            on apiScope.Index equals clientApiScope.ApiScopeIndex
                        where clientApiScope.ClientIndex == client.Index
                        select apiScope.Name
                    )
                    .ToArrayAsync();

                var returnClient = new Client
                {
                    ClientId = clientId,
                    AllowedScopes = apiScopes,
                    ClientSecrets = client.ClientSecrets.Select(i => new Secret(i.Sha256())).ToList(),
                    AllowedGrantTypes =  GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AbsoluteRefreshTokenLifetime = Common.RefreshTokenLifeTime,
                    AccessTokenLifetime = Common.AccessTokenLifeTime,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RefreshTokenUsage = TokenUsage.ReUse,
                };

                return returnClient;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Client();
            }
        }
    }
}
