using IdentityServer.Features.Commands.Authenticate.Register.DataObjects;
using MediatR;

namespace IdentityServer.Features.Commands.Authenticate.Register;

public class RegisterCommand : IRequest<RegisterResponse>
{
    public RegisterCommand(RegisterRequest request, RegisterHeaderRequest headerRequest)
    {
        Request = request;
        HeaderRequest = headerRequest;
    }

    public RegisterRequest Request { get; set; }
    public RegisterHeaderRequest HeaderRequest { get; set; }
}