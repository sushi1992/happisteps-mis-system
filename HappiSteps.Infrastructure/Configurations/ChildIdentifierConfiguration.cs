using HappiSteps.Domain.Identifiers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappiSteps.Infrastructure.Persistence.Configurations;

internal class ChildIdentifierConfiguration : IEntityTypeConfiguration<ChildIdentifier>
{
    public void Configure(EntityTypeBuilder<ChildIdentifier> builder)
    {
        builder.ToTable("ChildIdentifiers");

        builder.HasKey("ChildId", nameof(ChildIdentifier.Type));

        builder.Property(i => i.Type)
               .HasConversion<string>();

        builder.Property(i => i.Value)
               .IsRequired();

        builder.Property(i => i.AssignedAt)
               .IsRequired();
    }
}
