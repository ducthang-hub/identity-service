using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Models.DomainClasses
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Index { get; set; }
        public string ClientId { get; set; }
        public List<string> ClientSecrets { get; set; } = new();
        public List<string> ClientGrantTypes { get; set; } = new();
        public IEnumerable<ClientApiScope> ClientApiScopes { get; set; }
    }
}
