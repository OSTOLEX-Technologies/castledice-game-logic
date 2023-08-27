using castledice_game_logic.GameObjects;
using CastleGO = castledice_game_logic.GameObjects.Castle;
using static castledice_game_logic_tests.ObjectCreationUtility;
namespace castledice_game_logic_tests;

public class CastleTests
{
    [Fact]
    public void Castle_ShouldImplement_ICapturable()
    {
        var castle = new CastleGO(GetPlayer());
        
        Assert.True(castle is ICapturable);
    }

    [Fact]
    public void Castle_ShouldImplement_IUpgradeable()
    {
        var castle = new CastleGO(GetPlayer());
        
        Assert.True(castle is IUpgradeable);
    }
    
    [Fact]
    public void Castle_ShouldImplement_IPlayerOwned()
    {
        var castle = new CastleGO(GetPlayer());
        
        Assert.True(castle is IPlayerOwned);
    }

    [Fact]
    public void GetOwner_ShouldReturnPlayer_ThatWasGivenInConstructor()
    {
        var player = GetPlayer();
        var castle = new CastleGO(player);

        var owner = castle.GetOwner();
        
        Assert.Same(player, owner);
    }

    [Fact]
    public void CanBeUpgraded_ShouldReturnTrue_IfCastleIsNotMaxLevel()
    {
        var player = GetPlayer();
        var castle = new CastleGO(player);
        
        Assert.True(castle.CanBeUpgraded());
    }
}