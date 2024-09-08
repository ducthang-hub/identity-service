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
        public List<ClientScope> ClientScopes { get; set; }
        public List<ClientSecret> ClientSecrets { get; set; }
        public List<ClientGrantType> ClientGrantTypes { get; set; }
    }
}
