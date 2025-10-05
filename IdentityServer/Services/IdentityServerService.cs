using IdentityModel.Client;
using IdentityServer.Repository;
using IdentityServer.SettingOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IdentityServer.Services;

public sealed class IdentityServerService(
    IUnitOfRepository uor,
    IServiceProvider serviceProvider,
    HttpClient httpClient)
{
    public async Task<TokenResponse> GetToken(string username, string password, CancellationToken cancellationToken)
    {
        try
        {

            var identityServerSettings = serviceProvider.GetRequiredService<IOptions<IdentityServerSettings>>().Value;
            var applicationSettings = serviceProvider.GetRequiredService<IOptions<ApplicationSettings>>().Value;

            var client = await uor.Client.Where(i => i.ClientId == identityServerSettings.ClientId)
                .FirstOrDefaultAsync(cancellationToken);

            var apiScopes = await (
                from clientApiScope in uor.ClientApiScope.GetAll()
                join apiScope in uor.ApiScope.GetAll()
                    on clientApiScope.ApiScopeIndex equals apiScope.Index
                where clientApiScope.ClientIndex == client.Index
                select apiScope.Name
            ).ToArrayAsync(cancellationToken);

            var tokenRequest = new PasswordTokenRequest
            {
                Address = $"{applicationSettings.UrlHttp}/connect/token",
                ClientId = client?.ClientId,
                ClientSecret = client?.ClientSecrets.FirstOrDefault(),
                Scope = "offline_access " + apiScopes.FirstOrDefault(),
                // Scope = apiScopes.FirstOrDefault(),
                UserName = username,
                Password = password,
            };

            var tokenResponse =
                await httpClient.RequestPasswordTokenAsync(tokenRequest, cancellationToken: cancellationToken);
            return tokenResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Throw ex");
            throw;
        }
    }
}