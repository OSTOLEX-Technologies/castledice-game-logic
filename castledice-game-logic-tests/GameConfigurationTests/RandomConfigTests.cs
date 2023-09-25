using castledice_game_logic.GameConfiguration;

namespace castledice_game_logic_tests.GameConfigurationTests;

public class RandomConfigTests
{
    [Fact]
    public void Properties_ShouldReturnValues_GivenInConstructor()
    {
        int minActionPointsRoll = 1;
        int maxActionPointsRoll = 6;
        int probabilityPrecision = 100;
        
        var randomConfig = new RandomConfig(minActionPointsRoll, maxActionPointsRoll, probabilityPrecision);
        
        Assert.Equal(minActionPointsRoll, randomConfig.MinActionPointsRoll);
        Assert.Equal(maxActionPointsRoll, randomConfig.MaxActionPointsRoll);
        Assert.Equal(probabilityPrecision, randomConfig.ProbabilityPrecision);
    }
}