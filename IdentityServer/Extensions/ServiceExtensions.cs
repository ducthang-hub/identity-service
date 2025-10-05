using System.Reflection;
using IdentityServer.HostedService;
using IdentityServer.Models.Context;
using IdentityServer.Models.DomainClasses;
using IdentityServer.Repository;
using IdentityServer.ResourcesValidation;
using IdentityServer.Services;
using IdentityServer.Services.DatabaseService;
using IdentityServer.SettingOptions;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Polly;
using Polly.Retry;
using static System.Net.WebRequestMethods;

namespace IdentityServer.Extensions
{
    public class ServiceExtensions
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _config;
        public ServiceExtensions(
                IServiceCollection services,
                IConfiguration config
            )
        {
            _services = services;
            _config = config;
        }

        public void PollyConfiguration()
        {
            // _services.AddResiliencePipeline<HttpResponseMessage>("http-request-pipeline", pipelineBuilder =>
            // {
            //     pipelineBuilder
            //         .AddRetry(new RetryStrategyOptions<HttpResponseMessage>
            //         {
            //             ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
            //                 .Handle<HttpRequestException>()
            //                 .HandleResult(r => !r.IsSuccessStatusCode),
            //             Delay = TimeSpan.FromSeconds(1),
            //             MaxRetryAttempts = 3,
            //             BackoffType = DelayBackoffType.Constant
            //         })
            //         .AddTimeout(TimeSpan.FromSeconds(10));
            // });
        }

        public void SettingOptionsConfiguration()
        {
            _services.AddOptions<ApplicationSettings>()
                .BindConfiguration(ApplicationSettings.ConfigurationSection);
            
            _services.AddOptions<IdentityServerSettings>()
                .BindConfiguration(IdentityServerSettings.ConfigurationSection);
        }

        public void HttpClientConfiguration()
        {
            _services.AddHttpClient<IdentityServerService>();
        }
        
        public void IdentityServerConfiguration()
        {
            _services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<DatabaseContext>()
               .AddDefaultTokenProviders();

            var identityBuilder = _services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            });

            //.AddInMemoryClients(ConfigTestResource.Clients)
            //.AddInMemoryApiScopes(ConfigTestResource.ApiScopes)
            //.AddInMemoryApiResources(ConfigTestResource.ApiResources)
            //.AddInMemoryIdentityResources(ConfigTestResource.IdentityResources)
            //.AddTestUsers(ConfigTestResource.Users);

            identityBuilder
                .AddClientStore<ClientStore>()
                .AddResourceStore<ResourceStore>()
                .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            _services.AddTransient<IProfileService, ProfileService>();
            _services.AddTransient<IResourceOwnerPasswordValidator, CustomResourceOwnerPasswordValidator>();
        }

        public void AuthenticationConfiguration()
        {
            _services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = _config["Application:UrlHttps"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    // ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                };
                options.RequireHttpsMetadata = false;
                options.IncludeErrorDetails = true;
            });
            
        }

        public void AuthorizationConfiguration()
        {
            _services.AddAuthorization(options =>
            {
                options.AddPolicy(Common.StudentScope, policy =>
                {
                    policy.RequireClaim("client_id", new string[] { Common.EwbStudentWebClient, Common.EwbStudentMobileClient });
                });

                options.AddPolicy(Common.TeacherScope, policy =>
                {
                    policy.RequireClaim("client_id", Common.EwbTeacherClient);
                });

                options.AddPolicy(Common.AdminScope, policy =>
                {
                    policy.RequireClaim("client_id", Common.EwbAdminClient);
                });
            });
        }

        public void DatabaseConfiguration()
        {
            var connectionString = _config["Database:ConnectionString"];
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.EnableDynamicJson();
            var dataSource = dataSourceBuilder.Build();
            _services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(dataSource, o =>
            {
                o.EnableRetryOnFailure();
            }));
        }

        public void DIConfiguration()
        {
            _services.AddTransient<IUnitOfRepository, UnitOfRepository>();
            _services.AddTransient<IDatabaseService, DatabaseService>();
        }

        public void HostedServiceConfiguration()
        {
            _services.AddHostedService<MigrationHostedService>();
        }

        public void MediatorConfiguration()
        {
            _services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
