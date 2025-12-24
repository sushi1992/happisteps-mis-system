using HappiSteps.Domain.Admissions;

namespace HappiSteps.Application.Common.Interfaces;

public interface IAdmissionRepository
{
    Task AddAsync(Admission admission, CancellationToken ct = default);

    // Read-only is fine here for now
    Task<Admission?> GetByIdAsync(Guid admissionId, CancellationToken ct = default);
    Task<bool> HasActiveAdmissionAsync(
        Guid childId,
        Guid organisationId,
        CancellationToken cancellationToken = default);
}
