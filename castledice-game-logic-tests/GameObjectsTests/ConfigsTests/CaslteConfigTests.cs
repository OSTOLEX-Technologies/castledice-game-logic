using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic_tests.ConfigsTests;

public class CaslteConfigTests
{
    [Theory]
    [InlineData(3, 1, 1)]
    [InlineData(4, 2, 2)]
    public void Properties_ShouldReturnValues_GivenInConstructor(int durability, int freeDurability, int captureHitCost)
    {
        var castleConfig = new CastleConfig(durability, freeDurability, captureHitCost);
        
        Assert.Equal(durability, castleConfig.Durability);
        Assert.Equal(freeDurability, castleConfig.FreeDurability);
        Assert.Equal(captureHitCost, castleConfig.CaptureHitCost);
    }
}