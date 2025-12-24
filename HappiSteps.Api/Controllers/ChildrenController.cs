using HappiSteps.Application.Children.CreateChild;
using HappiSteps.Application.Children.GetChildById;
using HappiSteps.Contracts.Children;
using HappiSteps.ReadModel.Children.GetChildrenForOrganisation;
using HappiSteps.ReadModel.Children.GetChildDetails;
using Microsoft.AspNetCore.Mvc;

namespace HappiSteps.Api.Controllers;

[ApiController]
[Route("api/children")]
public class ChildrenController : ControllerBase
{
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

    [HttpGet]
    public async Task<IActionResult> GetChildren(
        [FromServices] GetChildrenForOrganisationHandler handler)
    {
        var result = await handler.Handle(
            new GetChildrenForOrganisationQuery());

        return Ok(result);
    }

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
}
