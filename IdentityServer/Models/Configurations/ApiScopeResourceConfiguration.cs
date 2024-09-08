using IdentityServer.Models.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Models.Configurations
{
    public class ApiScopeResourceConfiguration : IEntityTypeConfiguration<ApiScopeResource>
    {
        public void Configure(EntityTypeBuilder<ApiScopeResource> builder)
        {
            builder.ToTable(nameof(ApiScopeResource));

            builder.HasKey(i => new {i.ApiScopeIndex, i.ApiResourceIndex});

            builder.Property(i => i.ApiScopeIndex).IsRequired();    

            builder.Property(i => i.ApiResourceIndex).IsRequired();

            builder.HasOne(i => i.ApiScope)
                .WithMany(i => i.ApiScopeResources)
                .HasForeignKey(i => i.ApiScopeIndex)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.ApiResource)
                .WithMany(i => i.ApiScopeResources)
                .HasForeignKey(i => i.ApiResourceIndex)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
