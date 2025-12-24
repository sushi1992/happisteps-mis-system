using HappiSteps.Application.Admissions.ApplyForAdmission;
using HappiSteps.Application.Admissions.ConfirmAdmission;
using HappiSteps.Application.Admissions.LeaveAdmission;
using HappiSteps.Contracts.Admissions;
using HappiSteps.ReadModel.Admissions.GetAdmissionHistoryForChild;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HappiSteps.Contracts.Auth;

namespace HappiSteps.Api.Controllers;

[ApiController]
[Route("api/children/{childId:guid}/admissions")]
public sealed class AdmissionsController : ControllerBase
{
    [Authorize(Roles = Roles.Admin)]
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

    [Authorize(Roles = Roles.Admin)]
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
    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{admissionId:guid}/leave")]
    public async Task<IActionResult> Leave(
        Guid admissionId,
        LeaveAdmissionRequest request,
        [FromServices] LeaveAdmissionHandler handler)
    {
        await handler.Handle(
            new LeaveAdmissionCommand(admissionId, request.OrganisationId, request.LeavingDate));

        return NoContent();
    }

    [HttpGet("{childId:guid}/admissions/history")]
    public async Task<IActionResult> GetAdmissionHistory(
    Guid childId,
    [FromServices] GetAdmissionHistoryForChildHandler handler)
    {
        var result = await handler.Handle(
            new GetAdmissionHistoryForChildQuery(childId));

        return Ok(result);
    }
}
