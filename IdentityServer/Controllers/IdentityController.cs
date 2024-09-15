using IdentityModel.Client;
using IdentityServer.Data.Dtos.Login;
using IdentityServer.Data.Requests.Identity;
using IdentityServer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using IdentityServer.Data.Requests;
using IdentityServer.Features.Commands.Authenticate.Register;
using IdentityServer.Features.Commands.Authenticate.Register.DataObjects;
using IdentityServer.Helpers;
using IdentityServer.Utilities.Helpers;
using MediatR;
using Newtonsoft.Json;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUnitOfRepository _uor;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        public IdentityController(
                IUnitOfRepository uor,
                HttpClient httpClient,
                IConfiguration configuration,
                IMediator mediator
            )
        {
            _uor = uor;
            _httpClient = httpClient;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost("error")]
        public IActionResult Error()
        {
            throw new InvalidOperationException("this is a forced crash.");
        }
        
        [HttpPost("write-text")]
        public IActionResult WriteText([FromBody] WriteTextRequest request)
        {
            try
            {
                var directory = DirectoryHelper.GetParentFolder();
                if (string.IsNullOrEmpty(directory))
                {
                    return BadRequest("Directory not found");
                }
                
                var folderPath = Path.Combine(directory, "Text");
                var filePath = Path.Combine(folderPath, "text.txt");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                
                if(!System.IO.File.Exists(filePath))
                {
                    System.IO.File.Create(filePath).Dispose();
                }
                
                var oldText = System.IO.File.ReadAllText(filePath);
                var textObject = JsonConvert.DeserializeObject<List<WriteTextRequest>>(oldText) ?? new List<WriteTextRequest>();

                request.CreatedDate = DateTime.Now;
                textObject.Add(request);
                
                var newTextObject = JsonConvert.SerializeObject(textObject);
                System.IO.File.WriteAllText(filePath, newTextObject);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("get-text")]
        public IActionResult GetText()
        {
            var directory = DirectoryHelper.GetParentFolder();

            if (string.IsNullOrEmpty(directory))
            {
                return BadRequest("Directory not found");
            }
            
            var path = Path.Combine(directory, "Text/text.txt");
            var text = System.IO.File.ReadAllText(path);
            var textObject = JsonConvert.DeserializeObject<List<WriteTextRequest>>(text);
            return Ok(textObject);
        }
        
        [HttpGet("health-check")]
        public IActionResult HealthCheck()
        {
            return Ok("Identity Server is running");
        }
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken, [FromHeader] RegisterHeaderRequest headerRequest)
        {
            var response = await _mediator.Send(new RegisterCommand(request, headerRequest), cancellationToken);
            return ResponseHelper.ToResponse(response.Status, response.ErrorMessage, response.Data);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            const string funcName = $"{nameof(IdentityController)} - {nameof(Login)}";
            try
            {
                var user = await _uor.User
                    .Where(i => i.UserName == request.UserName && i.PasswordHash == request.Password)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user is null)
                {
                    return NotFound("User not found");
                }
                
                var origin = request.Https ? _configuration["Application:UrlHttps"] : _configuration["Application:UrlHttp"];
                
                Console.WriteLine($"{funcName} client origin {origin}");
                
                var tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = $"{origin}/connect/token",
                    ClientId = request.ClientId,
                    Scope = "offline_access " + request.ApiScope,
                    ClientSecret = request.ClientSecret,
                    UserName = request.UserName,
                    Password = request.Password,
                }, cancellationToken: cancellationToken);
                
                if (!string.IsNullOrEmpty(tokenResponse.Error))
                {
                    Console.WriteLine($"{funcName} Token Response Error: {tokenResponse.Error}");
                    return BadRequest($"ErrorDescription: {tokenResponse.ErrorDescription}\nErrorType: {tokenResponse.ErrorType.ToString()}\nRawJson: {tokenResponse.Raw}");
                }
                
                if (tokenResponse.HttpStatusCode != HttpStatusCode.OK) return BadRequest(tokenResponse);
                var output = new TokenDto
                {
                    AccessToken = tokenResponse.AccessToken,
                    RefreshToken = tokenResponse.RefreshToken,
                };
                
                return Ok(output);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
