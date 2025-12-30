using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Contracts.Auth;


namespace HappiSteps.Api.Controllers;

[ApiController]
[Route("api/auth/microsoft")]
public sealed class MicrosoftAuthController : ControllerBase
{
    private readonly IMicrosoftTokenValidator _validator;
    private readonly ITokenIssuer _tokenIssuer;

    public MicrosoftAuthController(
        IMicrosoftTokenValidator validator,
        ITokenIssuer tokenIssuer)
    {
        _validator = validator;
        _tokenIssuer = tokenIssuer;
    }

    [HttpPost("exchange")]
    [AllowAnonymous]
    public async Task<IActionResult> Exchange([FromBody] MicrosoftLoginRequest request)
    {
        var msUser = await _validator.ValidateCode(request.IdToken);

        // TEMP: derive IDs deterministically
        var userId = Guid.NewGuid();
        var organisationId = Guid.NewGuid();
        var roles = new[] { "Admin" };

        var token = _tokenIssuer.IssueToken(
            userId,
            organisationId,
            roles);

        return Ok(new { token });
    }
}
