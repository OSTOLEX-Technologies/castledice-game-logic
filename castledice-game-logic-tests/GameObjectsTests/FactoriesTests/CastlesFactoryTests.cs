using castledice_game_logic.GameObjects.Configs;
using castledice_game_logic.GameObjects.Factories.Castles;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests.GameObjectsTests.FactoriesTests;

public class CastlesFactoryTests
{
    [Fact]
    public void ConfigProperty_ShouldReturnConfig_GivenInConstructor()
    {
        var expectedConfig = new CastleConfig(3, 3, 3);
        var factory = new CastlesFactory(expectedConfig);
        
        Assert.Same(expectedConfig, factory.Config);
    }

[Theory]
    [InlineData(3, 1, 1)]
    [InlineData(5, 2, 3)]
    [InlineData(2, 4, 2)]
    public void GetCastle_ShouldReturnCastle_CreatedAccordingToConfig(int maxDurability, int maxFreeDurability, int captureHitCost)
    {
        var config = new CastleConfig(maxDurability, maxFreeDurability, captureHitCost);
        var factory = new CastlesFactory(config);
        
        var castle = factory.GetCastle(GetPlayer());
        
        Assert.Equal(maxDurability, castle.GetDurability());
        Assert.Equal(maxDurability, castle.GetMaxDurability());
        Assert.Equal(captureHitCost, castle.GetCaptureHitCost(GetPlayer()));
        Assert.Equal(maxFreeDurability, castle.GetMaxFreeDurability());
    }

    [Fact]
    public void GetCastle_ShouldCreateCastle_WithGivenOwner()
    {
        var owner = GetPlayer();
        var factory = new CastlesFactory(GetConfig());
        
        var castle = factory.GetCastle(owner);
        
        Assert.Same(owner, castle.GetOwner());
    }
    
    private static CastleConfig GetConfig(int durability = 3, int freeDurability = 1, int captureHitCost = 1)
    {
        return new CastleConfig(durability, freeDurability, captureHitCost);
    }
}