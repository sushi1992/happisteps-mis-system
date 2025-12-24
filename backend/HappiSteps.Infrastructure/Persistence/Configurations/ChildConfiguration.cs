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
        builder.HasMany(c => c.Identifiers)
               .WithOne()
               .HasForeignKey(ci => ci.ChildId)
               .OnDelete(DeleteBehavior.Cascade);

        // Tell EF the navigation is backed by the field
        builder.Navigation(c => c.Identifiers)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
