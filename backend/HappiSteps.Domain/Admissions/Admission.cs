namespace HappiSteps.Domain.Admissions;

public class Admission
{
    private Admission() { } // EF

    public Guid AdmissionId { get; private set; }
    public Guid ChildId { get; private set; }
    public Guid OrganisationId { get; private set; }

    public DateOnly AdmissionDate { get; private set; }
    public DateOnly? LeavingDate { get; private set; }

    public AdmissionStatus Status { get; private set; }

    internal Admission(
        Guid childId,
        Guid organisationId,
        DateOnly admissionDate)
    {
        AdmissionId = Guid.NewGuid();
        ChildId = childId;
        OrganisationId = organisationId;
        AdmissionDate = admissionDate;
        Status = AdmissionStatus.Applied;
    }

    public static Admission Apply(
        Guid childId,
        Guid organisationId,
        DateOnly admissionDate)
    {
        if (childId == Guid.Empty)
            throw new ArgumentException("ChildId is required");

        if (organisationId == Guid.Empty)
            throw new ArgumentException("OrganisationId is required");

        return new Admission(
            childId,
            organisationId,
            admissionDate);
    }

    public void ConfirmAdmission(DateOnly onRollDate)
    {
        if (Status != AdmissionStatus.Applied)
            throw new InvalidOperationException("Only applied admissions can be confirmed.");

        AdmissionDate = onRollDate;
        Status = AdmissionStatus.OnRoll;
    }

    public void Leave(DateOnly leavingDate)
    {
        if (Status != AdmissionStatus.OnRoll)
            throw new InvalidOperationException("Only on-roll admissions can be closed.");
        if (leavingDate < AdmissionDate)
            throw new InvalidOperationException(
                "Leaving date cannot be before admission date.");

        LeavingDate = leavingDate;
        Status = AdmissionStatus.Left;
    }
}
