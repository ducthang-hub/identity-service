using MediatR;

namespace IdentityServer.Features.Commands.Authenticate.Register.DataObjects;

public class RegisterRequest : IRequest<RegisterResponse>
{
    public string UserName{ get; set; }
    public string Password { get; set; }
}