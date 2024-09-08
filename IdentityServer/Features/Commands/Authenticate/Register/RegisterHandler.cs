using System.Net;
using IdentityServer.Enums;
using IdentityServer.Features.Commands.Authenticate.Register.DataObjects;
using IdentityServer.Models.DomainClasses;
using IdentityServer.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Features.Commands.Authenticate.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IUnitOfRepository _uor;

    public RegisterHandler(IUnitOfRepository uor)
    {
        _uor = uor;
    }
    public async Task<RegisterResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var funcName = $"{nameof(RegisterHandler)} ";

        var response = new RegisterResponse
        {
            Status = HttpStatusCode.OK
        };
        var request = command.Request;
        var header = command.HeaderRequest;
        
        try
        {
            var client = await _uor.Client.Where(i => i.ClientId == header.ClientId).FirstOrDefaultAsync(cancellationToken);

            if (client is null)
            {
                Console.WriteLine($"{funcName} Client not found");
                response.ErrorMessage = "Client not found";
                response.Status = HttpStatusCode.NotFound;
            }

            var userExisted = await _uor.User.Where(i => i.Email == request.UserName).AnyAsync(cancellationToken);

            if (userExisted)
            {
                Console.WriteLine($"{funcName} User email {request.UserName} already exist");
                response.ErrorMessage = "User already exist";
                response.Status = HttpStatusCode.BadRequest;
                return response;
            }

            var newUser = new User
            {
                UserName = request.UserName,
                PasswordHash = request.Password,
                Provider = Provider.Default,
                Email = request.UserName,
                NormalizedEmail = request.UserName.ToUpper(),
                NormalizedUserName = request.UserName.ToUpper(),
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow
            };

            await _uor.User.Add(newUser);
            _uor.SaveChanges();

            response.Data = newUser;
            return response;  
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{funcName} Has error: {ex.Message}");
            response.Status = HttpStatusCode.InternalServerError;
            response.ErrorMessage = ex.Message;
            return response;
        }
    }
}