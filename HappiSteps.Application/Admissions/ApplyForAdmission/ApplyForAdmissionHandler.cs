using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Domain.Admissions;
using HappiSteps.Domain.Common;

namespace HappiSteps.Application.Admissions.ApplyForAdmission;

public sealed class ApplyForAdmissionHandler
{
    private readonly IAdmissionRepository _admissions;
    private readonly IUnitOfWork _unitOfWork;

    public ApplyForAdmissionHandler(IAdmissionRepository admissions, IUnitOfWork unitOfWork)
    {
        _admissions = admissions;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        ApplyForAdmissionCommand command,
        CancellationToken cancellationToken = default)
    {
        var hasActiveAdmission =
            await _admissions.HasActiveAdmissionAsync(
                command.ChildId,
                command.OrganisationId,
                cancellationToken);

        if (hasActiveAdmission)
        {
            throw new InvalidOperationException(
                "Child already has an active admission for this organisation.");
        }

        var admission = Admission.Apply(
            command.ChildId,
            command.OrganisationId,
            command.AdmissionDate);

        await _admissions.AddAsync(admission, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return admission.AdmissionId;
    }
}
