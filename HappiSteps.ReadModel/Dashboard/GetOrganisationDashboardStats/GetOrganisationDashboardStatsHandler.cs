using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Contracts.Dashboard;
using HappiSteps.Domain.Admissions;
using HappiSteps.Domain.Children;
using HappiSteps.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HappiSteps.ReadModel.Dashboard.GetOrganisationDashboardStats;

public sealed class GetOrganisationDashboardStatsHandler
{
    private readonly HappiStepsDbContext _dbContext;
    private readonly IOrganisationContext _organisation;

    public GetOrganisationDashboardStatsHandler(
        HappiStepsDbContext dbContext,
        IOrganisationContext organisation)
    {
        _dbContext = dbContext;
        _organisation = organisation;
    }

    public async Task<OrganisationDashboardStats> Handle(
        GetOrganisationDashboardStatsQuery _,
        CancellationToken cancellationToken = default)
    {
        var orgId = _organisation.OrganisationId;

        var totalChildren = await _dbContext.Children
            .CountAsync(c => c.OrganisationId == orgId, cancellationToken);

        var archivedChildren = await _dbContext.Children
            .CountAsync(
                c => c.OrganisationId == orgId &&
                     c.Status == ChildStatus.Archived,
                cancellationToken);

        var onRollChildren = await _dbContext.Admissions
            .CountAsync(
                a => a.OrganisationId == orgId &&
                     a.Status == AdmissionStatus.OnRoll,
                cancellationToken);

        var startOfYear = new DateOnly(DateTime.UtcNow.Year, 1, 1);

        var admissionsThisYear = await _dbContext.Admissions
            .CountAsync(
                a => a.OrganisationId == orgId &&
                     a.AdmissionDate >= startOfYear,
                cancellationToken);

        return new OrganisationDashboardStats(
            totalChildren,
            onRollChildren,
            archivedChildren,
            admissionsThisYear);
    }
}
