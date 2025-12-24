using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Domain.Children;
using HappiSteps.Domain.Common;

namespace HappiSteps.Application.Children.ArchiveChild;

public sealed class ArchiveChildHandler
{
    private readonly IChildRepository _children;
    private readonly IUnitOfWork _unitOfWork;

    public ArchiveChildHandler(
        IChildRepository children,
        IUnitOfWork unitOfWork)
    {
        _children = children;
        _unitOfWork = unitOfWork;
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

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
