using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Utilities.Helpers;

public static class ResponseHelper
{
    public static ObjectResult ToResponse(HttpStatusCode status, string errorMessage = "", object data = null)
    {
        return new ObjectResult(new
        {
            StatusCode = status,
            ErrorMessage = errorMessage,
            Data = data
        });
    }
}