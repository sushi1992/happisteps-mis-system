using HappiSteps.ReadModel.Admissions.GetOnRollRegister;
using Microsoft.AspNetCore.Mvc;

namespace HappiSteps.Api.Controllers;

[ApiController]
[Route("api/organisations/{organisationId:guid}/registers")]
public sealed class RegistersController : ControllerBase
{
    [HttpGet("on-roll")]
    public async Task<IActionResult> GetOnRollRegister(
        Guid organisationId,
        [FromServices] GetOnRollRegisterHandler handler)
    {
        var result = await handler.Handle(
            new GetOnRollRegisterQuery(organisationId));

        return Ok(result);
    }
}
