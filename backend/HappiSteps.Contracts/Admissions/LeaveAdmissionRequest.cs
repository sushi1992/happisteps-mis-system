namespace HappiSteps.Contracts.Admissions;

public sealed record LeaveAdmissionRequest(
    Guid OrganisationId,
    DateOnly LeavingDate
);
