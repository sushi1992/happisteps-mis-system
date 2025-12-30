using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using HappiSteps.Application.Common.Interfaces;

namespace HappiSteps.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly ITokenIssuer _tokenIssuer;

    public AuthController(ITokenIssuer tokenIssuer)
    {
        _tokenIssuer = tokenIssuer;
    }

    // DEV ONLY – bypasses real auth
    [HttpGet("dev-login")]
    [AllowAnonymous]
    public IActionResult DevLogin(
        Guid userId,
        Guid organisationId)
    {
        var token = _tokenIssuer.IssueToken(
            userId,
            organisationId,
            ["Admin"]);

        return Ok(new { token });
    }

    // SESSION / IDENTITY ENDPOINT (MIS backbone)
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var userId =
            User.FindFirstValue(ClaimTypes.NameIdentifier) ??
            User.FindFirstValue(JwtRegisteredClaimNames.Sub);

        var organisationId =
            User.FindFirstValue("organisationId");

        return Ok(new
        {
            UserId = userId,
            OrganisationId = organisationId,
            Roles = User.FindAll(ClaimTypes.Role)
                        .Select(r => r.Value)
                        .ToArray()
        });
    }
}
