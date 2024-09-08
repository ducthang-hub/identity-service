using IdentityServer.Repository;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.ResourcesValidation
{
    public class ResourceStore : IResourceStore
    {
        private readonly IUnitOfRepository _uor;
        public ResourceStore(IUnitOfRepository uor)
        {
            _uor = uor;
        }
        public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            try
            {
                var apiResourceData = await _uor.ApiResource.Where(i => apiResourceNames.Contains(i.Name)).ToListAsync();
                if (!apiResourceData.Any())
                {
                    return new List<ApiResource>();
                }

                var apiResource = apiResourceData.Select(i => new ApiResource {
                    Name = i.Name,
                    ApiSecrets = new List<Secret> { new Secret(i.Secret.ToString().Sha256()) }
                }).ToList();

                return apiResource;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ApiResource>(); 
            }
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            try
            {
                var apiResourceData = await (
                    from ar in _uor.ApiResource.GetAll()
                    join sr in _uor.ApiScopeResource.GetAll()
                        on ar.Index equals sr.ApiResourceIndex
                    join sp in _uor.ApiScope.GetAll()
                        on sr.ApiScopeIndex equals sp.Index
                    where scopeNames.Contains(sp.Name)
                    select new
                    {
                        ResourceName = ar.Name,
                        ResourceSecret = ar.Secret,
                    }
                )
                .ToListAsync();

                var apiResources = apiResourceData.Select(i => new ApiResource
                {
                    Name = i.ResourceName,
                    ApiSecrets = new List<Secret>
                    {
                        new Secret(i.ResourceSecret.ToString().Sha256())
                    }
                }).ToList();

                return apiResources;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ApiResource>();
            }
        }

        public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            try
            {
                var apiScopeData = await _uor.ApiScope.Where(i => scopeNames.Contains(i.Name)).ToListAsync();
                var apiScopes = apiScopeData.Select(i => new ApiScope
                {
                    Name = i.Name,
                    DisplayName = i.DisplayName
                }).ToList();

                return apiScopes;
            }
            catch(Exception ex )
            {
                Console.WriteLine(ex.Message);
                return new List<ApiScope>();
            }
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return new List<IdentityResource>();
        }

        public async Task<IdentityServer4.Models.Resources> GetAllResourcesAsync()
        {
            return new IdentityServer4.Models.Resources();
        }
    }
}
