namespace HappiSteps.Application.Admissions.ApplyForAdmission;

public sealed record ApplyForAdmissionCommand(
    Guid ChildId,
    Guid OrganisationId,
    DateOnly AdmissionDate
);
