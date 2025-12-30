using HappiSteps.Domain.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappiSteps.Infrastructure.Persistence.Configurations;

public sealed class StaffMemberConfiguration
    : IEntityTypeConfiguration<StaffMember>
{
    public void Configure(EntityTypeBuilder<StaffMember> builder)
    {
        builder.ToTable("StaffMembers");

        builder.HasKey(x => x.StaffMemberId);

        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(256);

        builder.HasIndex(x => new { x.OrganisationId, x.Email })
               .IsUnique();

        builder.Property(x => x.Role)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.IsActive)
               .IsRequired();

        builder.Property(x => x.MicrosoftObjectId)
               .HasMaxLength(100);
    }
}
