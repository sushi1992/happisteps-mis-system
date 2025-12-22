namespace HappiSteps.Contracts.Admissions;

public sealed record ConfirmAdmissionRequest(
    DateOnly OnRollDate,
    string Upn
);
