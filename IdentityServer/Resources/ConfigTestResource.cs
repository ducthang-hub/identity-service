using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer.Resources
{
    public class ConfigTestResource
    {
        public static List<TestUser> Users => new List<TestUser>
        {
            new TestUser
            {
                Username = "student-web",
                Password = "@Bcd1234",
                SubjectId = Guid.NewGuid().ToString(),
            },
            new TestUser
            {
                Username = "student-mobile",
                Password = "@Bcd1234",
                SubjectId = Guid.NewGuid().ToString(),
            },
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource
            {
                Name = Common.StudentApiResource,
                ApiSecrets = new List<Secret>
                {
                    new Secret
                    {
                        Value = "secret"
                    }
                },
                Scopes = new List<string>{ Common.StudentScope }
            },
            new ApiResource
            {
                Name = Common.TeacherApiResource,
                ApiSecrets = new List<Secret>
                {
                    new Secret
                    {
                        Value = "secret"
                    }
                },
                Scopes = new List<string>{ Common.TeacherScope }
            },
            new ApiResource
            {
                Name = Common.AdminApiResource,
                ApiSecrets = new List<Secret>
                {
                    new Secret
                    {
                        Value = "secret"
                    }
                },
                Scopes = new List<string>{ Common.AdminScope}
            },
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope
            {
                Name = Common.StudentScope,
                DisplayName = "EWB Student Scope"
            },
            new ApiScope
            {
                Name = Common.TeacherScope,
                DisplayName = "EWB Teacher Scope"
            },
            new ApiScope
            {
                Name = Common.AdminScope,
                DisplayName = "EWB Admin Scope"
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResource(
                name: "openid",
                userClaims: new[] { "sub" },
                displayName: "Your user identifier"),

            new IdentityResource(
                name: "profile",
                userClaims: new[] { "username", "email", "website" },
                displayName: "Your profile data")
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            //new Client
            //{
            //    ClientId = Common.EwbStudentWebClient,
            //    AllowedScopes = {"openid", "profile", "offline_access", Common.StudentScope},
            //    ClientSecrets =
            //    {
            //        new Secret("secret".Sha256())
            //    },
            //    AllowedGrantTypes = new string[]
            //    {
            //        GrantType.ResourceOwnerPassword,
            //        GrantType.ClientCredentials,
            //        //GrantType.AuthorizationCode
            //    },
            //    AllowOfflineAccess = true,
            //    AbsoluteRefreshTokenLifetime = Common.RefreshTokenLifeTime,
            //    AccessTokenLifetime = Common.AccessTokenLifeTime,
            //    RefreshTokenExpiration = TokenExpiration.Absolute,
            //    RefreshTokenUsage = TokenUsage.ReUse,
            //},
            //new Client
            //{
            //    ClientId = Common.EwbStudentMobileClient,
            //    AllowedScopes = {"openid", "profile", "offline_access", Common.StudentScope},
            //    ClientSecrets =
            //    {
            //        new Secret("secret".Sha256())
            //    },
            //    AllowedGrantTypes= new string[]
            //    {
            //        GrantType.ResourceOwnerPassword,
            //    },
            //    AllowOfflineAccess = true,
            //    AbsoluteRefreshTokenLifetime = Common.RefreshTokenLifeTime,
            //    AccessTokenLifetime = Common.AccessTokenLifeTime,
            //    RefreshTokenExpiration = TokenExpiration.Absolute,
            //    RefreshTokenUsage = TokenUsage.ReUse,
            //},
            new Client
            {
                ClientId = Common.EwbTeacherClient,
                AllowedScopes = {"openid", "profile", "offline_access", Common.TeacherScope},
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedGrantTypes= new string[]
                {
                    GrantType.ResourceOwnerPassword,
                    //GrantType.AuthorizationCode
                },
                AllowOfflineAccess = true,
                AbsoluteRefreshTokenLifetime = Common.RefreshTokenLifeTime,
                AccessTokenLifetime = Common.AccessTokenLifeTime,
                RefreshTokenExpiration = TokenExpiration.Absolute,
                RefreshTokenUsage = TokenUsage.ReUse,
            },
            new Client
            {
                ClientId = Common.EwbAdminClient,
                AllowedScopes = {"openid", "profile", Common.AdminScope},
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedGrantTypes= new string[]
                {
                    GrantType.ResourceOwnerPassword,
                },
                AccessTokenLifetime = Common.AccessTokenLifeTime,
                //AbsoluteRefreshTokenLifetime = Common.RefreshTokenLifeTime,
                //RefreshTokenUsage = TokenUsage.ReUse,
                //RefreshTokenExpiration = TokenExpiration.Absolute,
                //AllowOfflineAccess = true,
            }
        };
    }
}
