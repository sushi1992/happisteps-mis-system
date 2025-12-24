namespace HappiSteps.Contracts.Admissions;

public sealed record AdmissionHistoryItem(
    Guid AdmissionId,
    Guid OrganisationId,
    DateOnly AdmissionDate,
    DateOnly? LeavingDate,
    string Status
);
