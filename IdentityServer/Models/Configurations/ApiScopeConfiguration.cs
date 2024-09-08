using IdentityServer.Models.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Models.Configurations
{
    public class ApiScopeConfiguration : IEntityTypeConfiguration<ApiScope>
    {
        public void Configure(EntityTypeBuilder<ApiScope> builder)
        {
            builder.ToTable(nameof(ApiScope));

            builder.HasKey(i => i.Index);
            builder.Property(i => i.Index).IsRequired();

            builder.Property(i => i.Name).IsRequired();

            builder.Property(i => i.DisplayName).IsRequired();

            //builder.HasData(
            //    new ApiScope
            //    {
            //        Index = Guid.NewGuid(),
            //        Name = "student-scope",
            //        DisplayName  = "Student Api Scope"
            //    },
            //    new ApiScope
            //    {
            //        Index = Guid.NewGuid(),
            //        Name = "teacher-scope",
            //        DisplayName = "Teacher Api Scope"
            //    }
            //);
        }
    }
}
