namespace HappiSteps.Application.Admissions.ConfirmAdmission;

public sealed record ConfirmAdmissionCommand(
    Guid AdmissionId,
    DateOnly OnRollDate,
    string Upn
);
