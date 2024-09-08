using IdentityServer.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models.DomainClasses
{
    // User table inherit from IdentityUser and the Dbcontext inherit from IdentitytDbContext => user table is named as AspNetUsers
    public class User : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Index { get; set; }
        public Provider Provider { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
