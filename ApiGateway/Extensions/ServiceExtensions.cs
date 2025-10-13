using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateway.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomAuthentication(this IServiceCollection services)
    {
        var key = "This is Secret Key don't share with anyone"u8.ToArray();
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer("Bearer", x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;

            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });
    }
}