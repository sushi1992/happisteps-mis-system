using HappiSteps.ReadModel.Dashboard.GetOrganisationDashboardStats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappiSteps.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/dashboard")]
public sealed class DashboardController : ControllerBase
{
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats(
        [FromServices] GetOrganisationDashboardStatsHandler handler)
    {
        var result = await handler.Handle(
            new GetOrganisationDashboardStatsQuery());

        return Ok(result);
    }
}
