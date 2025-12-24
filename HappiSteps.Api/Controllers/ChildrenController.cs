using HappiSteps.Application.Children.CreateChild;
using HappiSteps.Contracts.Children;
using HappiSteps.ReadModel.Children.GetChildrenForOrganisation;
using HappiSteps.ReadModel.Children.GetChildDetails;
using HappiSteps.Application.Children.ArchiveChild;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HappiSteps.Contracts.Auth;

namespace HappiSteps.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/children")]
public class ChildrenController : ControllerBase
{
    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateChildRequest request,
        [FromServices] CreateChildHandler handler)
    {
        var child = await handler.Handle(new CreateChildCommand(
            request.OrganisationId,
            request.FirstName,
            request.LastName,
            request.DateOfBirth
        ));

        return CreatedAtAction(nameof(GetChildDetails), new { id = child.ChildId }, child);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetChildren(
        [FromServices] GetChildrenForOrganisationHandler handler)
    {
        var result = await handler.Handle(
            new GetChildrenForOrganisationQuery());

        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetChildDetails(
        Guid id,
        [FromServices] GetChildDetailsHandler handler)
    {
        var result = await handler.Handle(
            new GetChildDetailsQuery(id));

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{id:guid}/archive")]
    public async Task<IActionResult> ArchiveChild(
        Guid id,
        [FromServices] ArchiveChildHandler handler)
    {
        await handler.Handle(
            new ArchiveChildCommand(id));

        return NoContent();
    }
}
