using HappiSteps.Domain.Children;
using Xunit;

public class ChildTests
{
    [Fact]
    public void Cannot_assign_upn_twice()
    {
        // Arrange
        var child = Child.Create(
            Guid.NewGuid(),
            "Test",
            "Child",
            new DateOnly(2020, 1, 1)
        );

        // Act
        child.AssignUpn("2014123456789");

        // Assert
        Assert.Throws<InvalidOperationException>(() =>
            child.AssignUpn("2014123456790"));
    }
}
