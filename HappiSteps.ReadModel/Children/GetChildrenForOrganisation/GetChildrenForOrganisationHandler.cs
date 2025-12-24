using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Contracts.Children;
using HappiSteps.Domain.Admissions;
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
        return await _dbContext.Children
            .AsNoTracking()
            .Where(c => c.OrganisationId == _organisation.OrganisationId)
            .Select(c => new
            {
                Child = c,
                OnRollAdmission = _dbContext.Admissions
                    .Where(a =>
                        a.ChildId == c.ChildId &&
                        a.Status == AdmissionStatus.OnRoll)
                    .OrderByDescending(a => a.AdmissionDate)
                    .FirstOrDefault()
            })
            .Select(x => new ChildListItem(
                x.Child.ChildId,
                x.Child.FirstName,
                x.Child.LastName,
                x.Child.DateOfBirth,
                x.Child.Status.ToString(),
                x.OnRollAdmission != null
                    ? x.OnRollAdmission.AdmissionDate
                    : null
            ))
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToListAsync(cancellationToken);
    }
}
