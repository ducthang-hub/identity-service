namespace IdentityServer.Data.Requests.Identity
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Https { get; set; }
    }
}
