using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic_tests.GameConfigurationTests;

public class PlaceablesConfigTests
{
    [Fact]
    public void Properties_ShouldReturnObjects_GivenInConstructor()
    {
        var knightsConfig = new KnightConfig(1, 2);
        
        var unitsConfig = new PlaceablesConfig(knightsConfig);
        
        Assert.Equal(knightsConfig, unitsConfig.KnightConfig);
    }
}