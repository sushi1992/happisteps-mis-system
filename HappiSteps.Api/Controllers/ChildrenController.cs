using HappiSteps.Contracts.Children;
using HappiSteps.Domain.Children;
using HappiSteps.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HappiSteps.Api.Controllers;

[ApiController]
[Route("api/children")]
public class ChildrenController : ControllerBase
{
    private readonly HappiStepsDbContext _db;

    public ChildrenController(HappiStepsDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateChildRequest request)
    {
        var child = new Child(
            request.OrganisationId,
            request.FirstName,
            request.LastName,
            request.DateOfBirth
        );

        _db.Children.Add(child);
        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = child.ChildId },
            child.ChildId
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var child = await _db.Children.FindAsync(id);

        if (child is null)
        {
            return NotFound();
        }

        return Ok(child);
    }
}
