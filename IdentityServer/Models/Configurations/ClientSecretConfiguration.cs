using IdentityServer.Models.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Models.Configurations
{
    public class ClientSecretConfiguration : IEntityTypeConfiguration<ClientSecret>
    {
        public void Configure(EntityTypeBuilder<ClientSecret> builder)
        {
            builder.ToTable(nameof(ClientSecret));

            builder.HasKey(i => i.Index);
            builder.Property(i => i.Index).IsRequired();

            builder.Property(i => i.Secret).IsRequired();   

            builder.Property(i => i.ClientIndex).IsRequired();  

            builder.HasOne(i => i.Client)
                .WithMany(i => i.ClientSecrets)
                .HasForeignKey(i => i.ClientIndex)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
