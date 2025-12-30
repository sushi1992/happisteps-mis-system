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

    private readonly IStaffRepository _staffRepository;

    public MicrosoftAuthController(
        IMicrosoftTokenValidator validator,
        ITokenIssuer tokenIssuer,
        IStaffRepository staffRepository)
    {
        _validator = validator;
        _tokenIssuer = tokenIssuer;
        _staffRepository = staffRepository;
    }

    [HttpPost("exchange")]
    [AllowAnonymous]
    public async Task<IActionResult> Exchange([FromBody] MicrosoftLoginRequest request)
    {
        // 1. Validate Microsoft login
        var msUser = await _validator.ValidateCode(request.IdToken);

        // 2. Look up staff member by Microsoft Object ID
        var staff = await _staffRepository
            .GetByMicrosoftObjectIdAsync(msUser.MicrosoftObjectId);

        if (staff is null || !staff.IsActive)
            return Unauthorized("User not provisioned");

        // 3. Issue JWT based on StaffMember
        var token = _tokenIssuer.IssueToken(
            staff.StaffMemberId,
            staff.OrganisationId,
            new[] { staff.Role });

        return Ok(new { token });
    }
}
