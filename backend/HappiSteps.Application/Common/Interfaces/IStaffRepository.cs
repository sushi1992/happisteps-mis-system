using HappiSteps.Domain.Staff;

namespace HappiSteps.Application.Common.Interfaces;

public interface IStaffRepository
{
    Task<StaffMember?> GetByMicrosoftObjectIdAsync(string objectId);
    Task Add(StaffMember staff);
    Task SaveChanges();
}
