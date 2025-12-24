using HappiSteps.Domain.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappiSteps.Infrastructure.Persistence.Configurations;

internal sealed class AuditLogEntryConfiguration
    : IEntityTypeConfiguration<AuditLogEntry>
{
    public void Configure(EntityTypeBuilder<AuditLogEntry> builder)
    {
        builder.ToTable("AuditLogEntries");

        builder.HasKey(x => x.AuditLogEntryId);

        builder.Property(x => x.Action).IsRequired();
        builder.Property(x => x.EntityType).IsRequired();

        builder.Property(x => x.MetadataJson)
               .HasColumnType("TEXT");

        builder.Property(x => x.OccurredAtUtc).IsRequired();
    }
}
