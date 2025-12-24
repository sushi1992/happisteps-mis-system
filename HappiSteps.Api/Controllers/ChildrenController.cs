using HappiSteps.Application.Children.CreateChild;
using HappiSteps.Application.Children.GetChildById;
using HappiSteps.Contracts.Children;
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

        return CreatedAtAction(nameof(GetById), new { id = child.ChildId }, child);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        [FromServices] GetChildByIdHandler handler)
    {
        var child = await handler.Handle(new GetChildByIdQuery(id));

        if (child is null)
            return NotFound();

        return Ok(child);
    }
}
