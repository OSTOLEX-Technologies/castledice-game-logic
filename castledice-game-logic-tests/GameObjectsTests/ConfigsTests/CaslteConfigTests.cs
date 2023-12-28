using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic_tests.ConfigsTests;

public class CaslteConfigTests
{
    [Theory]
    [InlineData(3, 1, 1)]
    [InlineData(4, 2, 2)]
    public void Properties_ShouldReturnValues_GivenInConstructor(int maxDurability, int maxFreeDurability, int captureHitCost)
    {
        var castleConfig = new CastleConfig(maxDurability, maxFreeDurability, captureHitCost);
        
        Assert.Equal(maxDurability, castleConfig.MaxDurability);
        Assert.Equal(maxFreeDurability, castleConfig.MaxFreeDurability);
        Assert.Equal(captureHitCost, castleConfig.CaptureHitCost);
    }
}