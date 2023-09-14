using castledice_game_logic.TurnsLogic;
using Moq;

namespace castledice_game_logic_tests;
using static ObjectCreationUtility;

public class ActionPointsConditionTests
{
    [Fact]
    public void ShouldSwitchTurn_ShouldReturnFalse_IfCurrentPlayerHasActionPoints()
    {
        var player = GetPlayer(actionPoints: 1); 
        var switchCondition = new ActionPointsCondition();
        
        Assert.False(switchCondition.ShouldSwitchTurn(player));
    }

    [Fact]
    public void ShouldSwitchTurn_ShouldReturnTrue_IfCurrentPlayerHasNoActionPoints()
    {
        var player = GetPlayer(actionPoints: 0);
        var switchCondition = new ActionPointsCondition();
        
        Assert.True(switchCondition.ShouldSwitchTurn(player));
    }
}