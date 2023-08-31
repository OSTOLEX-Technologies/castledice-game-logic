using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class GiveActionPointsApplierTests
{
    [Fact]
    public void ApplyAction_ShouldIncreasePlayerActionPoints_ByAmountFromTheAction()
    {
        int initialAmount = 2;
        int increase = 3;
        int expectedAmount = 5;
        var player = GetPlayer(actionPoints: 2);
        var action = new GiveActionPointsAction(player, increase);
        var applier = new GiveActionPointsApplier();
        
        applier.ApplyAction(action);
        int actualAmount = player.ActionPoints.Amount;
        
        Assert.Equal(expectedAmount, actualAmount);
    }
}