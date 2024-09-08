using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models.DomainClasses
{
    public class ApiResource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Index { get; set; }
        public string Name { get; set; }
        public Guid Secret { get; set; }
        public List<ApiScopeResource> ApiScopeResources { get; set; }
    }
}
