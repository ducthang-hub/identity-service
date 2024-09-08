using IdentityServer.Models.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;

namespace IdentityServer.Models.Configurations
{
    public class ApiResourceConfiguration : IEntityTypeConfiguration<ApiResource>
    {
        public void Configure(EntityTypeBuilder<ApiResource> builder)
        {
            builder.ToTable(nameof(ApiResource));

            builder.HasKey(i => i.Index);
            builder.Property(i => i.Index).IsRequired();

            builder.Property(i => i.Name).IsRequired(); 

            builder.Property(i => i.Secret).IsRequired();

            //builder.HasData(
            //    new ApiResource
            //    {
            //        Index = Guid.NewGuid(),
            //        Name = "student-api-resource",
            //        Secret = Guid.NewGuid()
            //    },
            //    new ApiResource
            //    {
            //        Index = Guid.NewGuid(),
            //        Name = "teacher-api-resource",
            //        Secret = Guid.NewGuid()
            //    }
            //);
        }
    }
}
