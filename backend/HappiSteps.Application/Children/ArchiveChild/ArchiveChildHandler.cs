using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Domain.Children;
using HappiSteps.Domain.Common;

namespace HappiSteps.Application.Children.ArchiveChild;

public sealed class ArchiveChildHandler
{
    private readonly IChildRepository _children;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditLogger _audit;

    public ArchiveChildHandler(
        IChildRepository children,
        IUnitOfWork unitOfWork,
        IAuditLogger audit)
    {
        _children = children;
        _unitOfWork = unitOfWork;
        _audit = audit;
    }

    public async Task Handle(
        ArchiveChildCommand command,
        CancellationToken cancellationToken = default)
    {
        var child = await _children.GetTrackedByIdAsync(
            command.ChildId,
            cancellationToken);

        if (child is null)
            throw new InvalidOperationException("Child not found.");

        child.Archive();

        await _audit.LogAsync(
            action: "ChildArchived",
            entityType: "Child",
            entityId: child.ChildId,
            metadata: new
            {
                PreviousStatus = "Left"
            },
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
