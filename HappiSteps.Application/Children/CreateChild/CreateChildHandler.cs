using HappiSteps.Domain.Children;
using HappiSteps.Domain.Common;
using HappiSteps.Contracts.Children;
using HappiSteps.Application.Common.Interfaces;

namespace HappiSteps.Application.Children.CreateChild;

public class CreateChildHandler
{
    private readonly IChildRepository _children;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditLogger _audit;

    public CreateChildHandler(IChildRepository children, IUnitOfWork unitOfWork, IAuditLogger audit)
    {
        _children = children;
        _unitOfWork = unitOfWork;
        _audit = audit;
    }

    public async Task<ChildResponse> Handle(CreateChildCommand command,
        CancellationToken cancellationToken = default)
    {
        var child = Child.Create(
            command.OrganisationId,
            command.FirstName,
            command.LastName,
            command.DateOfBirth
        );

        await _children.AddAsync(child, cancellationToken);
        await _audit.LogAsync(
            action: "ChildCreated",
            entityType: "Child",
            entityId: child.ChildId,
            metadata: new
            {
                child.FirstName,
                child.LastName,
                child.DateOfBirth
            },
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
