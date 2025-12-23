namespace HappiSteps.Application.Admissions.ConfirmAdmission;

public sealed record ConfirmAdmissionCommand(
    Guid AdmissionId,
    Guid OrganisationId,
    DateOnly OnRollDate,
    string Upn
);
