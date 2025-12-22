using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Domain.Children;
using HappiSteps.Domain.Common;
using HappiSteps.Domain.Identifiers;

namespace HappiSteps.Application.Admissions.ConfirmAdmission;

public sealed class ConfirmAdmissionHandler
{
    private readonly IAdmissionRepository _admissions;
    private readonly IChildRepository _children;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmAdmissionHandler(
        IAdmissionRepository admissions,
        IChildRepository children,
        IUnitOfWork unitOfWork)
    {
        _admissions = admissions;
        _children = children;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        ConfirmAdmissionCommand command,
        CancellationToken cancellationToken = default)
    {
        // 1️⃣ Load admission (tracked)
        var admission = await _admissions.GetByIdAsync(
            command.AdmissionId,
            cancellationToken);

        if (admission is null)
            throw new InvalidOperationException("Admission not found.");

        // 2️⃣ Load child (tracked)
        var child = await _children.GetTrackedByIdAsync(
            admission.ChildId,
            cancellationToken);

        if (child is null)
            throw new InvalidOperationException("Child not found.");

        // 3️⃣ Confirm admission
        admission.ConfirmAdmission(command.OnRollDate);

        // 4️⃣ Assign UPN if not already assigned
        if (!child.Identifiers.Any(i => i.Type == IdentifierType.UPN))
        {
            child.AssignUpn(command.Upn);
        }

        // 5️⃣ Update child status
        child.ChangeStatus(ChildStatus.OnRoll);

        // 6️⃣ Commit as one unit
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
