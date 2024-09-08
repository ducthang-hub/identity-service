using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Features.Commands.Authenticate.Register.DataObjects;

public class RegisterHeaderRequest
{
    [FromHeader(Name = "client-id")]
    public string ClientId { get; set; } = Common.EwbStudentWebClient;

    [FromHeader(Name = "origin")]
    public string Origin { get; set; }
}