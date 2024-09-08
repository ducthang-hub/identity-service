using IdentityServer.Repository;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.ResourcesValidation
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUnitOfRepository _uor;
        public CustomResourceOwnerPasswordValidator(
                IUnitOfRepository uor
            )
        {
            _uor = uor; 
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //todo: create hash password service
                var user = await _uor.User
                    .Where(i => i.UserName == context.UserName && i.PasswordHash == context.Password)
                    .FirstOrDefaultAsync();

                if( user == null )
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                }

                context.Result = new GrantValidationResult(subject: user.Index, GrantType.ResourceOwnerPassword);
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
