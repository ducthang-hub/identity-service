using System.Net;

namespace IdentityServer.Utilities.DTO;

public class ActionResponse
{
    public HttpStatusCode Status { get; set; }
    public string ErrorMessage { get; set; }
}