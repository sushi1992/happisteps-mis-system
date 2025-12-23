using HappiSteps.Application.Common.Interfaces;

namespace HappiSteps.Api.Auth;

public sealed class OrganisationContext : IOrganisationContext
{
    public Guid OrganisationId { get; }

    public OrganisationContext(IHttpContextAccessor accessor)
    {
        var claim = accessor.HttpContext?
            .User?
            .FindFirst("organisationId")?.Value;

        OrganisationId = claim is null
            ? Guid.Empty
            : Guid.Parse(claim);
    }
}
