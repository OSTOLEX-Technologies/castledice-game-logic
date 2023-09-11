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
    public void DecreaseActionPoints_ShouldThrowInvalidOperationException_IfDecreasingBelowZero()
    {
        var actionPoints = new PlayerActionPoints();
        actionPoints.Amount = 3;

        Assert.Throws<InvalidOperationException>(() => actionPoints.DecreaseActionPoints(5));
    }

    [Fact]
    public void AmountProperty_ShouldThrowArgumentException_IfTryingToSetAmountLessThanZero()
    {
        var actionPoints = new PlayerActionPoints();

        Assert.Throws<ArgumentException>(() => actionPoints.Amount = -1);
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
    
    [Fact]
    public void ActionPointsDecreased_ShouldBeInvoked_WhenDecreasingActionPoints()
    {
        var actionPoints = new PlayerActionPoints();
        actionPoints.Amount = 5;
        int decreaseAmount = 2;
        int decreaseAmountFromEvent = 0;
        actionPoints.ActionPointsDecreased += (sender, amount) => decreaseAmountFromEvent = amount;
        
        actionPoints.DecreaseActionPoints(decreaseAmount);
        
        Assert.Equal(decreaseAmount, decreaseAmountFromEvent);
    }
    
    [Fact]
    public void ActionPointsIncreased_ShouldBeInvoked_WhenIncreasingActionPoints()
    {
        var actionPoints = new PlayerActionPoints();
        int increaseAmount = 2;
        int increaseAmountFromEvent = 0;
        actionPoints.ActionPointsIncreased += (sender, amount) => increaseAmountFromEvent = amount;
        
        actionPoints.IncreaseActionPoints(increaseAmount);
        
        Assert.Equal(increaseAmount, increaseAmountFromEvent);
    }
}