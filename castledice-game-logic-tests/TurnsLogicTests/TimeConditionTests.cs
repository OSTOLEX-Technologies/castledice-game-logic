using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Time;
using castledice_game_logic.TurnsLogic;
using Moq;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class TimeConditionTests
{
    [Fact]
    public void ShouldSwitchTurn_ShouldReturnFalse_IfTimeIsNotElapsed()
    {
        var timerMock = new Mock<ITimer>();
        timerMock.Setup(t => t.IsElapsed()).Returns(false);
        var timer = timerMock.Object;
        var timeCondition = new TimeCondition(timer, 10, GetTurnsSwitcher(GetPlayer()));
        timeCondition.Start();
        
        Assert.False(timeCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void ShouldSwitchTurn_ShouldReturnTrue_IfTimeElapsed()
    {
        var timerMock = new Mock<ITimer>();
        timerMock.Setup(t => t.IsElapsed()).Returns(true);
        var timer = timerMock.Object;
        var timeCondition = new TimeCondition(timer, 10, GetTurnsSwitcher(GetPlayer()));
        timeCondition.Start();

        Assert.True(timeCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void ShouldSwitchTurn_ShouldReturnFalse_IfStartMethodIsNotCalled()
    {
        var timerMock = new Mock<ITimer>();
        timerMock.Setup(t => t.IsElapsed()).Returns(true);
        var timer = timerMock.Object;
        var timeCondition = new TimeCondition(timer, 30, GetTurnsSwitcher(GetPlayer()));

        Assert.False(timeCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void ShouldSwitchTurn_ShouldResetInnerTimer_IfReturnedTrue()
    {
        int duration = 10;
        var timer = new TickTimerMock();
        var timeCondition = new TimeCondition(timer, duration, GetTurnsSwitcher(GetPlayer()));
        timeCondition.Start();
        
        timer.Tick(18);
        Assert.True(timeCondition.ShouldSwitchTurn());
        timer.Tick(5);
        Assert.False(timeCondition.ShouldSwitchTurn());
        timer.Tick(6);
        Assert.True(timeCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void InnerTimer_ShouldBeReset_IfTurnSwitchedInTurnSwitcher()
    {
        int duration = 10;
        var timer = new TickTimerMock();
        var turnSwitcher = GetTurnsSwitcher(GetPlayer());
        var timeCondition = new TimeCondition(timer, duration, turnSwitcher);
        timeCondition.Start();
        timer.Tick(18);
        
        turnSwitcher.SwitchTurn();
        
        Assert.False(timeCondition.ShouldSwitchTurn());
    }
}