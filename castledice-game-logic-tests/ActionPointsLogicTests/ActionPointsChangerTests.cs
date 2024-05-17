using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic_tests.ActionPointsLogicTests;
using static ObjectCreationUtility;

public class ActionPointsChangerTests
{
    [Fact]
    public void ChangeActionPoints_ShouldReturnActionWithPlayer_GivenInConstructor()
    {
        var player = GetPlayer();
        var changer = new ActionPointsChanger(player);

        var action = changer.ChangeActionPoints(3);
        
        Assert.Same(player, action.Player);
    }

    [Fact]
    public void ChangeActionPoints_ShouldReturnAction_WithGivenAmount()
    {
        var player = GetPlayer();
        int expectedAmount = 6;
        var changer = new ActionPointsChanger(player);

        var action = changer.ChangeActionPoints(expectedAmount);
        
        Assert.Equal(expectedAmount, action.Amount);
    }
}