using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Contracts.Admissions;
using HappiSteps.Contracts.Children;
using HappiSteps.Domain.Admissions;
using HappiSteps.Domain.Identifiers;
using HappiSteps.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HappiSteps.ReadModel.Children.GetChildDetails;

public sealed class GetChildDetailsHandler
{
    private readonly HappiStepsDbContext _dbContext;
    private readonly IOrganisationContext _organisation;

    public GetChildDetailsHandler(
        HappiStepsDbContext dbContext,
        IOrganisationContext organisation)
    {
        _dbContext = dbContext;
        _organisation = organisation;
    }

    public async Task<ChildDetailsResponse?> Handle(
        GetChildDetailsQuery query,
        CancellationToken cancellationToken = default)
    {
        var child = await _dbContext.Children
            .AsNoTracking()
            .Include(c => c.Identifiers)
            .FirstOrDefaultAsync(
                c =>
                    c.ChildId == query.ChildId &&
                    c.OrganisationId == _organisation.OrganisationId,
                cancellationToken);

        if (child is null)
            return null;

        var admissions = await _dbContext.Admissions
            .AsNoTracking()
            .Where(a =>
                a.ChildId == query.ChildId &&
                a.OrganisationId == _organisation.OrganisationId)
            .OrderByDescending(a => a.AdmissionDate)
            .Select(a => new AdmissionHistoryItem(
                a.AdmissionId,
                a.OrganisationId,
                a.AdmissionDate,
                a.LeavingDate,
                a.Status.ToString()
            ))
            .ToListAsync(cancellationToken);

        return new ChildDetailsResponse(
            child.ChildId,
            child.FirstName,
            child.LastName,
            child.DateOfBirth,
            child.Status.ToString(),
            child.Identifiers
                .Select(i => new ChildIdentifierResponse(
                    i.Type.ToString(),
                    i.Value,
                    i.AssignedAt))
                .ToList(),
            admissions
        );
    }
}
