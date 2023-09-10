using castledice_game_logic.Penalties;

namespace castledice_game_logic_tests.PenaltyTests;
using static ObjectCreationUtility;

public class PassPenaltyTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_IfNonPositiveMaxPassCountGiven(int maxPassCount)
    {
        Assert.Throws<ArgumentException>(() => new PassPenalty(maxPassCount, GetTurnsSwitcher()));
    }
    
    [Theory]
    [InlineData(3)]
    [InlineData(2)]
    public void GetViolators_ShouldReturnEmptyList_IfNoPlayersExceededMaxPassCount(int maxPassCount)
    {
        var turnSwitcher = GetTurnsSwitcher();
        var passPenalty = new PassPenalty(maxPassCount, turnSwitcher);
        
        for (int i = 0; i < maxPassCount-1; i++)
        {
            turnSwitcher.SwitchTurn();
        }
        var violators = passPenalty.GetViolators();
        
        Assert.Empty(violators);
    }
    
    [Theory]
    [InlineData(3)]
    [InlineData(2)]
    [InlineData(1)]
    public void GetViolators_ShouldReturnListWithPlayer_IfPlayerExceededMaxPassCount(int maxPassCount)
    {
        var player = GetPlayer();
        var turnSwitcher = GetTurnsSwitcher(player);
        var passPenalty = new PassPenalty(maxPassCount, turnSwitcher);
        
        for (int i = 0; i < maxPassCount; i++)
        {
            turnSwitcher.SwitchTurn();
        }
        var violators = passPenalty.GetViolators();
        
        Assert.Contains(player, violators);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(3)]
    public void GetPassCount_ShouldReturnAmountOfTurns_ThatPlayerPassed(int passCount)
    {
        var player = GetPlayer();
        var turnSwitcher = GetTurnsSwitcher(player);
        var passPenalty = new PassPenalty(3, turnSwitcher);
        for (int i = 0; i < passCount; i++)
        {
            turnSwitcher.SwitchTurn();
        }
        
        var actualPassCount = passPenalty.GetPassCount(player);
        
        Assert.Equal(passCount, actualPassCount);
    }

    [Fact]
    public void GetPassCount_ShouldReturnZero_IfPlayerSpendActionPointsDuringLastTurn()
    {
        var player = GetPlayer();
        var turnSwitcher = GetTurnsSwitcher(player);
        var passPenalty = new PassPenalty(3, turnSwitcher);
        for (int i = 0; i < 3; i++)
        {
            turnSwitcher.SwitchTurn();
        }
        player.ActionPoints.DecreaseActionPoints(1);
        
        var actualPassCount = passPenalty.GetPassCount(player);
        
        Assert.Equal(0, actualPassCount);
    }

    [Fact]
    public void GetViolators_ShouldNotReturnListWithPlayer_IfPlayerSpendActionPointsDuringLastTurn()
    {
        var player = GetPlayer();
        var turnSwitcher = GetTurnsSwitcher(player);
        var passPenalty = new PassPenalty(3, turnSwitcher);
        for (int i = 0; i < 3; i++)
        {
            turnSwitcher.SwitchTurn();
        }
        player.ActionPoints.DecreaseActionPoints(1);

        var violators = passPenalty.GetViolators();
        
        Assert.Empty(violators);
    }
}