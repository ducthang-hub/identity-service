using IdentityServer.Models.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Models.Configurations
{
    public class ClientApiScopeConfiguration : IEntityTypeConfiguration<ClientApiScope>
    {
        public void Configure(EntityTypeBuilder<ClientApiScope> builder)
        {
            builder.ToTable(nameof(ClientApiScope));
            
            builder.HasKey(i => new { i.ClientIndex, i.ApiScopeIndex });
            
            builder.Property(i => i.ClientIndex).IsRequired();
            
            builder.Property(i => i.ApiScopeIndex).IsRequired();
            
            builder.HasOne<Client>(i => i.Client)
                .WithMany(i => i.ClientApiScopes)
                .HasForeignKey(i => i.ClientIndex)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne<ApiScope>(i => i.ApiScope)
                .WithMany(i => i.ClientApiScopes)
                .HasForeignKey(i => i.ApiScopeIndex)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
