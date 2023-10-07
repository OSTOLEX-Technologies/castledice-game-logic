using castledice_game_logic.GameConfiguration;

namespace castledice_game_logic_tests.GameConfigurationTests;

public class RandomConfigTests
{
    [Theory]
    [InlineData(1, 6, 100)]
    [InlineData(2, 18, 1000)]
    public void Properties_ShouldReturnValues_GivenInConstructor(int minActionPointsRoll, int maxActionPointsRoll, int probabilityPrecision)
    {
        var randomConfig = new RandomConfig(minActionPointsRoll, maxActionPointsRoll, probabilityPrecision);
        
        Assert.Equal(minActionPointsRoll, randomConfig.MinActionPointsRoll);
        Assert.Equal(maxActionPointsRoll, randomConfig.MaxActionPointsRoll);
        Assert.Equal(probabilityPrecision, randomConfig.ProbabilityPrecision);
    }
}