using castledice_game_logic.GameObjects.Configs;
using castledice_game_logic.GameObjects.Factories;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests.FactoriesTests;

public class CastlesFactoryTests
{
    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(1)]
    // In this test durability of the castle is checked by passing player with amount of action points
    // that is definitely bigger than castle durability into GetCaptureCost method.
    public void GetCastle_ShouldReturnCastle_CreatedAccordingToConfig(int castleDurability)
    {
        var config = new CastleConfig() { Durability = castleDurability };
        var factory = new CastlesFactory(config);
        var capturer = GetPlayer(castleDurability + 1);
        
        var castle = factory.GetCastle(GetPlayer());
        
        Assert.Equal(castleDurability, castle.GetCaptureCost(capturer));
    }

    [Fact]
    public void GetCastle_ShouldCreateCastle_WithGivenOwner()
    {
        var owner = GetPlayer();
        var factory = new CastlesFactory(GetConfig());
        
        var castle = factory.GetCastle(owner);
        
        Assert.Same(owner, castle.GetOwner());
    }
    
    private CastleConfig GetConfig(int durability = 3)
    {
        return new CastleConfig() { Durability = durability };
    }
}