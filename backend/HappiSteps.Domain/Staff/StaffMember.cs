namespace HappiSteps.Domain.Staff;

public sealed class StaffMember
{
    public Guid StaffMemberId { get; init; }
    public Guid OrganisationId { get; init; }

    public string Email { get; private set; } = null!;
    public string Role { get; private set; } = null!; // "Admin", "Staff"
    public string DisplayName { get; private set; } = null!;
    public bool IsActive { get; private set; }

    // Optional but very useful
    public string? MicrosoftObjectId { get; private set; }

    private StaffMember() { }

    public StaffMember(
           Guid organisationId,
           string microsoftObjectId,
           string email,
           string displayName,
           string role)
    {
        StaffMemberId = Guid.NewGuid();
        OrganisationId = organisationId;
        MicrosoftObjectId = microsoftObjectId;
        Email = email;
        DisplayName = displayName;
        Role = role;
        IsActive = true;
    }

    public void AttachMicrosoftIdentity(string objectId)
    {
        MicrosoftObjectId = objectId;
    }

    public void Disable()
    {
        IsActive = false;
    }
}
