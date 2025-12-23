namespace HappiSteps.Contracts.Admissions;

public sealed record ConfirmAdmissionRequest(
    Guid OrganisationId,
    DateOnly OnRollDate,
    string Upn
);
