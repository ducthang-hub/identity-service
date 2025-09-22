namespace IdentityServer.Models.DomainClasses
{
    public class ClientApiScope
    {
        public Guid ClientIndex { get; set; }
        public Client Client { get; set; }
        public Guid ApiScopeIndex { get; set; }
        public ApiScope ApiScope { get; set; }
    }
}
