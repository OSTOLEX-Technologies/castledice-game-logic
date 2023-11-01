using castledice_game_logic.GameConfiguration;

namespace castledice_game_logic_tests.GameConfigurationTests;

public class RandomConfigTests
{
    [Theory]
    [InlineData(1, 6, 100)]
    [InlineData(2, 18, 1000)]
    public void Properties_ShouldReturnValues_GivenInConstructor(int minInclusive, int maxExclusive, int precision)
    {
        var randomConfig = new RandomConfig(minInclusive, maxExclusive, precision);
        
        Assert.Equal(minInclusive, randomConfig.MinInclusive);
        Assert.Equal(maxExclusive, randomConfig.MaxExclusive);
        Assert.Equal(precision, randomConfig.Precision);
    }
}