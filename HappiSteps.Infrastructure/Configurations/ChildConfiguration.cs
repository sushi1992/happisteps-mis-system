using HappiSteps.Domain.Children;
using HappiSteps.Domain.Identifiers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappiSteps.Infrastructure.Persistence.Configurations;

internal class ChildConfiguration : IEntityTypeConfiguration<Child>
{
    public void Configure(EntityTypeBuilder<Child> builder)
    {
        builder.ToTable("Children");

        builder.HasKey(c => c.ChildId);

        builder.Property(c => c.FirstName).IsRequired();
        builder.Property(c => c.LastName).IsRequired();
        builder.Property(c => c.DateOfBirth).IsRequired();
        builder.Property(c => c.Status).IsRequired();

        // Map private backing field
        builder
            .HasMany<ChildIdentifier>("_identifiers")
            .WithOne()
            .HasForeignKey("ChildId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation("_identifiers")
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
