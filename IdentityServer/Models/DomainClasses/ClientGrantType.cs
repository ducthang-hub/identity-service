using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Models.DomainClasses
{
    public class ClientGrantType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Index { get; set; }
        public string GrantType { get; set; }
        public Guid ClientIndex { get; set; }
        public Client Client { get; set; }
    }
}
