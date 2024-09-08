using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Models.DomainClasses
{
    public class ApiScopeResource
    {
        public Guid ApiResourceIndex { get; set; }
        public ApiResource ApiResource { get; set; }
        public Guid ApiScopeIndex { get; set; }
        public ApiScope ApiScope { get; set; }
    }
}
