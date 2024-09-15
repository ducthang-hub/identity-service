namespace IdentityServer.Data.Requests.Identity
{
    public class LoginRequest
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ApiScope { get; set; }
        public bool Https { get; set; }
    }
}
