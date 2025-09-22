using IdentityServer.Models.Configurations;
using IdentityServer.Models.DomainClasses;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Models.Context
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        private const string DefaultSchema = "identitydb";

        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        // public DbSet<ClientGrantType> ClientGrantTypes { get; set; }
        public DbSet<ClientApiScope> ClientApiScopes { get; set; }
        // public DbSet<ClientSecret> ClientSecrets { get; set; }
        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<ApiScope> ApiScopes { get; set; }
        public DbSet<ApiScopeResource> ApiScopeResources { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(DefaultSchema);
            modelBuilder.ApplyConfiguration(new ApiResourceConfiguration())
                .ApplyConfiguration(new ApiScopeConfiguration())
                .ApplyConfiguration(new ApiScopeResourceConfiguration())
                .ApplyConfiguration(new ClientConfiguration())
                .ApplyConfiguration(new ClientApiScopeConfiguration())
                // .ApplyConfiguration(new ClientGrantTypeConfiguration())
                // .ApplyConfiguration(new ClientSecretConfiguration())
                .ApplyConfiguration(new UserConfiguration());
        }
    }
}
