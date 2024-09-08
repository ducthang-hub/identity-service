using IdentityServer.Constants;
using IdentityServer.Models.DomainClasses;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer.ResourcesValidation
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
        private readonly UserManager<User> _userManager;
        public ProfileService(
                IUserClaimsPrincipalFactory<User> claimFactory,
                UserManager<User> userManager
            )
        {
            _claimsFactory = claimFactory;
            _userManager = userManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var sub = context.Subject.GetSubjectId();
                var user = await _userManager.FindByIdAsync(sub);

                var principal = await _claimsFactory.CreateAsync(user);

                var claims = principal.Claims.ToList();
                claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

                claims.Add(new Claim(CustomClaimTypes.UserId, user.Index ?? string.Empty));
                claims.Add(new Claim(CustomClaimTypes.Email, user.Email ?? string.Empty));
                claims.Add(new Claim(CustomClaimTypes.Phone, user.PhoneNumber ?? string.Empty));
                claims.Add(new Claim(CustomClaimTypes.Provider, user.Provider.ToString()));
                claims.Add(new Claim(CustomClaimTypes.SecurityStamp, user.SecurityStamp ?? string.Empty));

                context.IssuedClaims = claims;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(subId);
            context.IsActive = user != null;
        }
    }
}
