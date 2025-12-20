using HappiSteps.Domain.Children;
using HappiSteps.Contracts.Children;
using HappiSteps.Application.Common.Interfaces;

namespace HappiSteps.Application.Children.CreateChild;

public class CreateChildHandler
{
    private readonly IChildRepository _children;

    public CreateChildHandler(IChildRepository children)
    {
        _children = children;
    }

    public async Task<ChildResponse> Handle(CreateChildCommand command)
    {
        var child = Child.Create(
            command.OrganisationId,
            command.FirstName,
            command.LastName,
            command.DateOfBirth
        );

        await _children.AddAsync(child);

        return new ChildResponse(
            child.ChildId,
            child.OrganisationId,
            child.FirstName,
            child.LastName,
            child.DateOfBirth,
            child.Status.ToString(),
            []
        );
    }
}
