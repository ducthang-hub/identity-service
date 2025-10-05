using IdentityServer.Models.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Models.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Index);
            builder.Property(x => x.Index).IsRequired();

            builder.HasData(new User()
            {
                Index = "cb3dc6a4-4564-4312-82d0-326ff5ba6c39",
                Provider = 0,
                IsActive = true,
                IsDeleted = false,
                UserName = "marist",
                EmailConfirmed = true,
                PasswordHash = "marist0103_pw",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });
        }
    }
}
