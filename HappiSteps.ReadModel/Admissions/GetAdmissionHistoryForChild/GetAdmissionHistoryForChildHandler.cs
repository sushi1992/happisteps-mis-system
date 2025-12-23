using HappiSteps.Contracts.Admissions;
using HappiSteps.Domain.Admissions;
using HappiSteps.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HappiSteps.ReadModel.Admissions.GetAdmissionHistoryForChild;

public sealed class GetAdmissionHistoryForChildHandler
{
    private readonly HappiStepsDbContext _dbContext;

    public GetAdmissionHistoryForChildHandler(
        HappiStepsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<AdmissionHistoryItem>> Handle(
        GetAdmissionHistoryForChildQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Admissions
            .AsNoTracking()
            .Where(a => a.ChildId == query.ChildId)
            .OrderByDescending(a => a.AdmissionDate)
            .Select(a => new AdmissionHistoryItem(
                a.AdmissionId,
                a.OrganisationId,
                a.AdmissionDate,
                a.LeavingDate,
                a.Status.ToString()
            ))
            .ToListAsync(cancellationToken);
    }
}
