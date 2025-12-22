using HappiSteps.Domain.Admissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappiSteps.Infrastructure.Persistence.Configurations;

internal sealed class AdmissionConfiguration
    : IEntityTypeConfiguration<Admission>
{
    public void Configure(EntityTypeBuilder<Admission> builder)
    {
        builder.ToTable("Admissions");

        builder.HasKey(a => a.AdmissionId);

        builder.Property(a => a.ChildId)
               .IsRequired();

        builder.Property(a => a.OrganisationId)
               .IsRequired();

        builder.Property(a => a.AdmissionDate)
               .IsRequired();

        builder.Property(a => a.Status)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(a => a.LeavingDate);
    }
}
