using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Domain.Children;
using HappiSteps.Domain.Common;

namespace HappiSteps.Application.Admissions.LeaveAdmission;

public sealed class LeaveAdmissionHandler
{
    private readonly IAdmissionRepository _admissions;
    private readonly IChildRepository _children;
    private readonly IUnitOfWork _unitOfWork;

    public LeaveAdmissionHandler(
        IAdmissionRepository admissions,
        IChildRepository children,
        IUnitOfWork unitOfWork)
    {
        _admissions = admissions;
        _children = children;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        LeaveAdmissionCommand command,
        CancellationToken cancellationToken = default)
    {
        var admission = await _admissions.GetByIdAsync(
            command.AdmissionId,
            cancellationToken);

        if (admission is null)
            throw new InvalidOperationException("Admission not found.");

        // We are going to mutate the child, so tracked fetch.
        var child = await _children.GetTrackedByIdAsync(
            admission.ChildId,
            cancellationToken);

        if (child is null)
            throw new InvalidOperationException("Child not found.");

        admission.Leave(command.LeavingDate);
        child.ChangeStatus(ChildStatus.Left);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
