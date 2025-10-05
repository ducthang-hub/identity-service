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
using IdentityServer.Services;
using IdentityServer.Utilities.Helpers;
using MediatR;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;

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
        private readonly IdentityServerService _identityServer;
        public IdentityController(
                IUnitOfRepository uor,
                HttpClient httpClient,
                IConfiguration configuration,
                IMediator mediator,
                IdentityServerService identityServer
            )
        {
            _uor = uor;
            _httpClient = httpClient;
            _configuration = configuration;
            _mediator = mediator;
            _identityServer = identityServer;
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

                var pipeline = new ResiliencePipelineBuilder<TokenResponse>()
                    .AddRetry(new RetryStrategyOptions<TokenResponse>
                    {
                        ShouldHandle = new PredicateBuilder<TokenResponse>()
                            .Handle<Exception>()
                            .HandleResult(r => r.IsError),
                        Delay = TimeSpan.FromSeconds(1),
                        MaxRetryAttempts = 3,
                        BackoffType = DelayBackoffType.Constant,
                        OnRetry = agrs =>
                        {
                            Console.WriteLine($"Attempt {agrs.AttemptNumber} - {agrs.Outcome}");
                            return ValueTask.CompletedTask;
                        }
                    })
                    .AddTimeout(TimeSpan.FromSeconds(10))
                    .Build();

                var tokenResponse = await pipeline.ExecuteAsync(async token =>
                {
                    var tokenResponse =
                        await _identityServer.GetToken(user.UserName!, user.PasswordHash!, token);
                    return tokenResponse;
                }, cancellationToken);
                
                if (!string.IsNullOrEmpty(tokenResponse.Error) || tokenResponse.HttpStatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine($"{funcName} Token Response Error: {tokenResponse.Error} - {tokenResponse.ErrorDescription}");
                    return BadRequest();
                }
                
                return Ok(new TokenDto
                {
                    AccessToken = tokenResponse.AccessToken,
                    RefreshToken = tokenResponse.RefreshToken,
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
