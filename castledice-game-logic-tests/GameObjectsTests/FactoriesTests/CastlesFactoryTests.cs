using castledice_game_logic.GameObjects.Configs;
using castledice_game_logic.GameObjects.Factories;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests.FactoriesTests;

public class CastlesFactoryTests
{
    [Theory]
    [InlineData(3, 1, 1)]
    [InlineData(5, 2, 3)]
    [InlineData(2, 4, 2)]
    // In this test durability (both free and standard) of the castle is checked by passing player with amount of action points
    // that is definitely bigger than castle durability into GetCaptureCost method.
    public void GetCastle_ShouldReturnCastle_CreatedAccordingToConfig(int durability, int freeDurability, int captureCost)
    {
        var config = new CastleConfig(durability, freeDurability, captureCost);
        var factory = new CastlesFactory(config);
        
        var castle = factory.GetCastle(GetPlayer());
        
        Assert.Equal(durability, castle.GetDurability());
        Assert.Equal(captureCost, castle.GetCaptureCost(GetPlayer()));
        castle.Free();
        Assert.Equal(freeDurability, castle.GetDurability());
    }

    [Fact]
    public void GetCastle_ShouldCreateCastle_WithGivenOwner()
    {
        var owner = GetPlayer();
        var factory = new CastlesFactory(GetConfig());
        
        var castle = factory.GetCastle(owner);
        
        Assert.Same(owner, castle.GetOwner());
    }
    
    private static CastleConfig GetConfig(int durability = 3, int freeDurability = 1, int captureCost = 1)
    {
        return new CastleConfig(durability, freeDurability, captureCost);
    }
}