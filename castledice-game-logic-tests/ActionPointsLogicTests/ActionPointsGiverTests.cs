using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.Math;
using Moq;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class ActionPointsGiverTests
{
    [Fact]
    public void GiveActionPoints_ShouldReturnActionWithPlayer_GivenInConstructor()
    {
        var player = GetPlayer();
        var giver = new ActionPointsGiver(player);

        var action = giver.GiveActionPoints(3);
        
        Assert.Same(player, action.Player);
    }

    [Fact]
    public void GiveActionPoints_ShouldIncreasePlayerActionPoints_ByGivenAmount()
    {
        var player = GetPlayer();
        int expectedAmount = 6;
        var giver = new ActionPointsGiver(player);

        var action = giver.GiveActionPoints(expectedAmount);
        
        Assert.Equal(expectedAmount, action.Amount);
    }
}