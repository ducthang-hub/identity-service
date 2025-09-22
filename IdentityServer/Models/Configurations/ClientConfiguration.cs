using IdentityServer.Models.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Models.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable(nameof(Client));

            builder.HasKey(i => i.Index);
            builder.Property(i => i.Index).IsRequired();

            builder.Property(i => i.ClientSecrets)
                .HasColumnType("jsonb");
            
            builder.Property(i => i.ClientGrantTypes)
                .HasColumnType("jsonb");
        }
    }
}
