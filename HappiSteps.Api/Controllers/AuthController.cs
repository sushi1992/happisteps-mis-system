using HappiSteps.Api.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HappiSteps.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    [HttpPost("dev-login")]
    public IActionResult DevLogin(
        Guid userId,
        Guid organisationId)
    {
        var token = DevTokenIssuer.IssueToken(
            userId,
            organisationId);

        return Ok(new { token });
    }
}
