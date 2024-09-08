using IdentityServer.Repository;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;

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
                var clientData = await (
                    from cl in _uor.Client.GetAll()
                    join sp in _uor.ClientScope.GetAll().Include(i => i.ApiScope)
                        on cl.Index equals sp.ClientIndex
                    join sc in _uor.ClientSecret.GetAll()
                        on cl.Index equals sc.ClientIndex
                    join gt in _uor.ClientGrantType.GetAll()
                        on cl.Index equals gt.ClientIndex
                    select new
                    {
                        Client = cl,
                        ApiScope = sp,
                        ClientSecret = sc,
                        ClientGrantType = gt
                    }
                )
                .ToListAsync();

                if(!clientData.Any())
                {
                    return new Client();
                }

                var client = clientData.Select(i => i.Client).FirstOrDefault();
                var apiScope = clientData.Select(i => i.ApiScope).Distinct().ToList();
                var clientSecret = clientData.Select(i => i.ClientSecret).Distinct().ToList();  
                var grantType = clientData.Select(i => i.ClientGrantType).Distinct().ToList();

                var returnClient = new Client
                {
                    ClientId = client?.ClientId ?? clientId,
                    AllowedScopes = apiScope?.Select(i => i.ApiScope.Name).ToList() ?? new List<string>(),
                    ClientSecrets = clientSecret?.Select(i => new Secret(i.Secret.Sha256())).ToList() ?? null,
                    AllowedGrantTypes = grantType?.Select(i => i.GrantType).ToList() ?? new List<string>(),
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
