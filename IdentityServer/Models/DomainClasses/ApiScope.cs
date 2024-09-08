using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Models.DomainClasses
{
    public class ApiScope
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Index { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<ApiScopeResource> ApiScopeResources { get; set; }
        public List<ClientScope> ClientScopes { set; get; }
    }
}
