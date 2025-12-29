using System.Security.Claims;
using HappiSteps.Api.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace HappiSteps.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly DevTokenIssuer _tokenIssuer;

    public AuthController(DevTokenIssuer tokenIssuer)
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
            organisationId);

        return Ok(new { token });
    }

    // REAL LOGIN (stubbed for now)
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginRequest request)
    {
        // TODO: validate credentials, load user, resolve roles

        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var organisationId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        var token = _tokenIssuer.IssueToken(
            userId,
            organisationId);

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
