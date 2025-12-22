namespace HappiSteps.Contracts.Admissions;

public sealed record ApplyForAdmissionRequest(
    Guid OrganisationId,
    DateOnly AdmissionDate
);
