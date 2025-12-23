using HappiSteps.Contracts.Admissions;
using HappiSteps.Domain.Admissions;
using HappiSteps.Domain.Identifiers;
using HappiSteps.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HappiSteps.ReadModel.Admissions.GetOnRollRegister;

public sealed class GetOnRollRegisterHandler
{
    private readonly HappiStepsDbContext _dbContext;

    public GetOnRollRegisterHandler(HappiStepsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<OnRollRegisterItem>> Handle(
        GetOnRollRegisterQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Admissions
            .AsNoTracking()
            .Where(a =>
                a.OrganisationId == query.OrganisationId &&
                a.Status == AdmissionStatus.OnRoll)
            .Join(
                _dbContext.Children.AsNoTracking(),
                admission => admission.ChildId,
                child => child.ChildId,
                (admission, child) => new { admission, child })
            .Select(x => new OnRollRegisterItem(
                x.child.ChildId,
                x.child.FirstName,
                x.child.LastName,
                x.child.DateOfBirth,
                x.child.Identifiers
                    .Where(i => i.Type == IdentifierType.UPN)
                    .Select(i => i.Value)
                    .FirstOrDefault(),
                x.admission.AdmissionDate
            ))
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToListAsync(cancellationToken);
    }
}
