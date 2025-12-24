using HappiSteps.ReadModel.Admissions.GetOnRollRegister;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace HappiSteps.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/organisations/{organisationId:guid}/registers")]
public sealed class RegistersController : ControllerBase
{
    [HttpGet("on-roll")]
    public async Task<IActionResult> GetOnRollRegister(
        [FromServices] GetOnRollRegisterHandler handler)
    {
        var result = await handler.Handle(
            new GetOnRollRegisterQuery());

        return Ok(result);
    }
}
