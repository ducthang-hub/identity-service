using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Models.DomainClasses
{
    public class ClientScope
    {
        public Guid ClientIndex { get; set; }
        public Client Client { get; set; }
        public Guid ApiScopeIndex { get; set; }
        public ApiScope ApiScope { get; set; }
    }
}
