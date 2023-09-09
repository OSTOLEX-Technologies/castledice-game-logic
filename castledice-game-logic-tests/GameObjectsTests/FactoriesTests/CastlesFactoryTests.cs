using castledice_game_logic.GameObjects.Configs;
using castledice_game_logic.GameObjects.Factories;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests.FactoriesTests;

public class CastlesFactoryTests
{
    [Theory]
    [InlineData(3, 1)]
    [InlineData(5, 2)]
    [InlineData(1, 4)]
    // In this test durability (both free and standard) of the castle is checked by passing player with amount of action points
    // that is definitely bigger than castle durability into GetCaptureCost method.
    public void GetCastle_ShouldReturnCastle_CreatedAccordingToConfig(int castleDurability, int freeDurability)
    {
        var config = new CastleConfig() { Durability = castleDurability, FreeDurability = freeDurability};
        var factory = new CastlesFactory(config);
        
        var castle = factory.GetCastle(GetPlayer());
        
        Assert.Equal(castleDurability, castle.GetCaptureCost(GetPlayer(actionPoints: castleDurability + 1)));
        castle.Free();
        Assert.Equal(freeDurability, castle.GetCaptureCost(GetPlayer(actionPoints: freeDurability + 1)));
    }

    [Fact]
    public void GetCastle_ShouldCreateCastle_WithGivenOwner()
    {
        var owner = GetPlayer();
        var factory = new CastlesFactory(GetConfig());
        
        var castle = factory.GetCastle(owner);
        
        Assert.Same(owner, castle.GetOwner());
    }
    
    private CastleConfig GetConfig(int durability = 3, int freeDurability = 1)
    {
        return new CastleConfig() { Durability = durability, FreeDurability = freeDurability};
    }
}