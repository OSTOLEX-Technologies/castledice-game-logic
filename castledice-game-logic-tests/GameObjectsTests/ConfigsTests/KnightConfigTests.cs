using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic_tests.ConfigsTests;

public class KnightConfigTests
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(3, 4)]
    public void Properties_ShouldReturnValues_GivenInConstructor(int placementCost, int health)
    {
        var knightsConfig = new KnightConfig(placementCost, health);
        
        Assert.Equal(placementCost, knightsConfig.PlacementCost);
        Assert.Equal(health, knightsConfig.Health);
    }
}