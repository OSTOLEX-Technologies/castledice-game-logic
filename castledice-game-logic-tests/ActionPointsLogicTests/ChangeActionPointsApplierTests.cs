using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic_tests.ActionPointsLogicTests;
using static ObjectCreationUtility;

public class ChangeActionPointsApplierTests
{
    [Fact]
    public void ApplyAction_ShouldChangePlayerActionPoints_ByAmountFromTheAction()
    {
        int initialAmount = 3;
        var rnd = new Random();
        int change = rnd.Next(0, 3) - rnd.Next(0, 3);
        int expectedAmount = initialAmount + change;
        var player = GetPlayer(actionPoints: initialAmount);
        var action = new ChangeActionPointsAction(player, change);
        var applier = new ChangeActionPointsApplier();
        
        applier.ApplyAction(action);
        int actualAmount = player.ActionPoints.Amount;
        
        Assert.Equal(expectedAmount, actualAmount);
    }
}