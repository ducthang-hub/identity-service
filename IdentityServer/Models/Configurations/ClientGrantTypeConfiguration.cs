using IdentityServer.Models.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Models.Configurations
{
    public class ClientGrantTypeConfiguration : IEntityTypeConfiguration<ClientGrantType>
    {
        public void Configure(EntityTypeBuilder<ClientGrantType> builder)
        {
            builder.ToTable(nameof(ClientGrantType));

            builder.HasKey(i => i.Index);
            builder.Property(i => i.Index).IsRequired();

            builder.Property(i=>i.ClientIndex).IsRequired();    

            builder.HasOne(i => i.Client)
                .WithMany(i => i.ClientGrantTypes)
                .HasForeignKey(i => i.ClientIndex)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
