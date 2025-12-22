using HappiSteps.Domain.Children;
using HappiSteps.Domain.Common;
using HappiSteps.Contracts.Children;
using HappiSteps.Application.Common.Interfaces;

namespace HappiSteps.Application.Children.CreateChild;

public class CreateChildHandler
{
    private readonly IChildRepository _children;
    private readonly IUnitOfWork _unitOfWork;

    public CreateChildHandler(IChildRepository children, IUnitOfWork unitOfWork)
    {
        _children = children;
        _unitOfWork = unitOfWork;
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
        await _unitOfWork.SaveChangesAsync();

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
