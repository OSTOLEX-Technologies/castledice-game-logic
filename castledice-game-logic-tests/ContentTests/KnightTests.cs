using castledice_game_logic.GameObjects;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class KnightTests
{
    [Fact]
    public void Knight_ShouldImplement_IRemovable()
    {
        var knight = new Knight(GetPlayer());
        
        Assert.True(knight is IRemovable);
    }

    [Fact]
    public void Knight_ShouldImplement_IPlayerOwned()
    {
        var knight = new Knight(GetPlayer());
        
        Assert.True(knight is IPlayerOwned);
    }

    [Fact]
    public void Knight_ShouldImplement_IUpgradeable()
    {
        var knight = new Knight(GetPlayer());
        
        Assert.True(knight is IUpgradeable);
    }

    [Fact]
    public void GetOwner_ShouldReturnPlayer_GivenInConstructor()
    {
        var player = GetPlayer();
        var knight = new Knight(player);

        var owner = knight.GetOwner();
        
        Assert.Same(player, owner);
    }
}