using IdentityServer.Models.DomainClasses;
using IdentityServer.Utilities.DTO;

namespace IdentityServer.Features.Commands.Authenticate.Register;

public class RegisterResponse : ActionResponse
{
    public User Data { get; set; }
}