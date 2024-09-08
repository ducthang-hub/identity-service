using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [Authorize(Common.StudentScope)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize]
        [HttpGet("StudentWebResource")]
        public string GetStudentWebResource()
        {
            var result = "reach web student resource";
            return result;
        }

        [Authorize(Common.StudentScope)]
        [HttpGet("StudentMobileResource")]
        public string GetMobileStudentResource()
        {
            var result = "reach mobile student resource";
            return result;
        }

        [Authorize(Common.TeacherScope)]
        [HttpGet("TeacherResource")]
        public string GetTeacherResource()
        {
            var result = "reach teacher resource";
            return result;
        }

        [Authorize(Common.AdminScope)]
        [HttpGet("AdminResource")]
        public string GetAdminResource()
        {
            var result = "reach admin resource";
            return result;
        }
    }
}