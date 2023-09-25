using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic_tests.GameConfigurationTests;

public class UnitsConfigTests
{
    [Fact]
    public void Properties_ShouldReturnObjects_GivenInConstructor()
    {
        var knightsConfig = new KnightConfig();
        
        var unitsConfig = new UnitsConfig(knightsConfig);
        
        Assert.Equal(knightsConfig, unitsConfig.KnightConfig);
    }
}