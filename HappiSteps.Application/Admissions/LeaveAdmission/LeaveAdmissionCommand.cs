namespace HappiSteps.Application.Admissions.LeaveAdmission;

public sealed record LeaveAdmissionCommand(
    Guid AdmissionId,
    DateOnly LeavingDate
);
