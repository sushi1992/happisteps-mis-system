namespace HappiSteps.Application.Common.Interfaces;

public interface ITokenIssuer
{
    string IssueToken(
        Guid userId,
        Guid organisationId,
        IReadOnlyCollection<string> roles);
}
