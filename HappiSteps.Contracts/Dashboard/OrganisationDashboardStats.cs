namespace HappiSteps.Contracts.Dashboard;

public sealed record OrganisationDashboardStats(
    int TotalChildren,
    int OnRollChildren,
    int ArchivedChildren,
    int AdmissionsThisYear
);
