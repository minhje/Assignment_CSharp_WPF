using Business.Helpers;

namespace Business.Tests.Helpers;

public class IdGenerator_Tests
{
    [Fact]
    public void GenerateUniqueId_ShouldReturnGuidAsString()
    {
        // Act
        var result = IdGenerator.GenerateUniqueId();

        // Assert
        Assert.NotNull(result);
        Assert.True(Guid.TryParse(result, out _));
    }
}
