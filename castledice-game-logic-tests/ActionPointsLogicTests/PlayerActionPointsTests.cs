using System.Diagnostics;
using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic_tests;

public class PlayerActionPointsTests
{
    [Fact]
    public void DecreaseActionPoints_ShouldDecreaseActionPointsAmount_ByGivenNumber()
    {
        var actionPoints = new PlayerActionPoints();
        int expectedActionPoints = 3;
        actionPoints.Amount = 5;
        actionPoints.DecreaseActionPoints(2);

        var actualActionPoints = actionPoints.Amount;
        
        Assert.Equal(expectedActionPoints, actualActionPoints);
    }

    [Fact]
    public void IncreaseActionPoints_ShouldIncreaseActionPointsAmount_ByGivenNumber()
    {
        var actionPoints = new PlayerActionPoints();
        int expectedActionPoints = 3;
        actionPoints.Amount = 0;
        actionPoints.IncreaseActionPoints(3);

        var actualActionPoints = actionPoints.Amount;
        
        Assert.Equal(expectedActionPoints, actualActionPoints);
    }
        
}