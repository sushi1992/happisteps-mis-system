using HappiSteps.Domain.Children;
using Xunit;

public class ChildTests
{
    [Fact]
    public void Cannot_assign_upn_twice()
    {
        var child = new Child(
            Guid.NewGuid(),
            "Test",
            "Child",
            new DateOnly(2020, 1, 1)
        );

        child.AssignUpn("2014123456789");

        Assert.Throws<InvalidOperationException>(() =>
            child.AssignUpn("2014123456790"));
    }
}
