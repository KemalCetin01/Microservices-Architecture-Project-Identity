using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using MS.Services.Core.ExceptionHandling.Wrapper;
using System.Net;

namespace MS.Services.Identity.API.CustomProviders;
public class ApiVersioningErrorResponseProvider : DefaultErrorResponseProvider
{
    public override IActionResult CreateResponse(ErrorResponseContext context)
    {
        var errorResponse = new ExceptionResponse
        {
            Message = context.Message,
            StatusCode = context.ErrorCode
        };
        var response = new ObjectResult(errorResponse)
        {
            StatusCode = (int)HttpStatusCode.BadRequest
        };

        return response;
    }
}
