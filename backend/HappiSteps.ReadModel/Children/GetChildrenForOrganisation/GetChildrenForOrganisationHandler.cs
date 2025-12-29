using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Contracts.Children;
using HappiSteps.Domain.Admissions;
using HappiSteps.Domain.Children;
using HappiSteps.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HappiSteps.ReadModel.Children.GetChildrenForOrganisation;

public sealed class GetChildrenForOrganisationHandler
{
    private readonly HappiStepsDbContext _dbContext;
    private readonly IOrganisationContext _organisation;

    public GetChildrenForOrganisationHandler(
        HappiStepsDbContext dbContext,
        IOrganisationContext organisation)
    {
        _dbContext = dbContext;
        _organisation = organisation;
    }

    public async Task<IReadOnlyList<ChildListItem>> Handle(
        GetChildrenForOrganisationQuery _,
        CancellationToken cancellationToken = default)
    {
        // -----------------------------
        // Phase 1: Database query only
        // -----------------------------
        var rows = await _dbContext.Children
            .AsNoTracking()
            .Where(c =>
                c.OrganisationId == _organisation.OrganisationId &&
                c.Status != ChildStatus.Archived)
            .Select(c => new
            {
                c.ChildId,
                c.FirstName,
                c.LastName,
                c.DateOfBirth,
                c.Status,

                OnRollAdmissionDate = _dbContext.Admissions
                    .Where(a =>
                        a.ChildId == c.ChildId &&
                        a.Status == AdmissionStatus.OnRoll)
                    .OrderByDescending(a => a.AdmissionDate)
                    .Select(a => (DateOnly?)a.AdmissionDate)
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        // -----------------------------
        // Phase 2: Shape + order in memory
        // -----------------------------
        return rows
            .Select(x => new ChildListItem(
                x.ChildId,
                x.FirstName,
                x.LastName,
                x.DateOfBirth,
                x.Status.ToString(),
                x.OnRollAdmissionDate
            ))
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToList();
    }
}
