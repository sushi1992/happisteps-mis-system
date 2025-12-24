using HappiSteps.Contracts.Admissions;

namespace HappiSteps.Contracts.Children;

public sealed record ChildDetailsResponse(
    Guid ChildId,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Status,
    IReadOnlyCollection<ChildIdentifierResponse> Identifiers,
    IReadOnlyCollection<AdmissionHistoryItem> Admissions
);
