using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Domain.Admissions;
using Microsoft.EntityFrameworkCore;

namespace HappiSteps.Infrastructure.Persistence.Repositories;

public sealed class AdmissionRepository : IAdmissionRepository
{
    private readonly HappiStepsDbContext _dbContext;

    public AdmissionRepository(HappiStepsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Admission admission,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Admissions.AddAsync(admission, cancellationToken);
    }

    public async Task<Admission?> GetByIdAsync(
        Guid admissionId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Admissions
            .FirstOrDefaultAsync(a => a.AdmissionId == admissionId, cancellationToken);
    }

    public async Task<bool> HasActiveAdmissionAsync(
        Guid childId,
        Guid organisationId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Admissions.AnyAsync(
            a =>
                a.ChildId == childId &&
                a.OrganisationId == organisationId &&
                a.Status != AdmissionStatus.Left,
            cancellationToken);
    }
}
