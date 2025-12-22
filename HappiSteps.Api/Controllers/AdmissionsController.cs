using HappiSteps.Application.Admissions.ApplyForAdmission;
using HappiSteps.Application.Admissions.ConfirmAdmission;
using HappiSteps.Contracts.Admissions;
using Microsoft.AspNetCore.Mvc;

namespace HappiSteps.Api.Controllers;

[ApiController]
[Route("api/children/{childId:guid}/admissions")]
public sealed class AdmissionsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> ApplyForAdmission(
        Guid childId,
        ApplyForAdmissionRequest request,
        [FromServices] ApplyForAdmissionHandler handler)
    {
        var admissionId = await handler.Handle(
            new ApplyForAdmissionCommand(
                childId,
                request.OrganisationId,
                request.AdmissionDate));

        return Ok(new { AdmissionId = admissionId });
    }

    [HttpPost("{admissionId:guid}/confirm")]
    public async Task<IActionResult> ConfirmAdmission(
        Guid admissionId,
        ConfirmAdmissionRequest request,
        [FromServices] ConfirmAdmissionHandler handler)
    {
        await handler.Handle(
            new ConfirmAdmissionCommand(
                admissionId,
                request.OnRollDate,
                request.Upn));

        return NoContent();
    }
}
