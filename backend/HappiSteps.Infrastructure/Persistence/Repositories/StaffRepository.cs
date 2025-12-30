using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Domain.Staff;
using Microsoft.EntityFrameworkCore;

namespace HappiSteps.Infrastructure.Persistence.Repositories;

public sealed class StaffRepository : IStaffRepository
{
    private readonly HappiStepsDbContext _context;

    public StaffRepository(HappiStepsDbContext context)
    {
        _context = context;
    }

    public async Task<StaffMember?> GetByMicrosoftObjectIdAsync(string objectId)
    {
        return await _context.StaffMembers
            .SingleOrDefaultAsync(s => s.MicrosoftObjectId == objectId);
    }

    public async Task Add(StaffMember staff)
    {
        await _context.StaffMembers.AddAsync(staff);
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
