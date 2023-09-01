using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Time;
using castledice_game_logic.TurnsLogic;
using Moq;

namespace castledice_game_logic_tests;

public class TimeConditionTests
{
    [Fact]
    public void ShouldSwitchTurn_ShouldReturnFalse_IfTimeIsNotElapsed()
    {
        var timerMock = new Mock<ITimer>();
        timerMock.Setup(t => t.IsElapsed()).Returns(false);
        var timer = timerMock.Object;
        var timeCondition = new TimeCondition(timer);
        timeCondition.Start();
        
        Assert.False(timeCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void ShouldSwitchTurn_ShouldReturnTrue_IfTimeElapsed()
    {
        var timerMock = new Mock<ITimer>();
        timerMock.Setup(t => t.IsElapsed()).Returns(true);
        var timer = timerMock.Object;
        var timeCondition = new TimeCondition(timer);
        timeCondition.Start();

        Assert.True(timeCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void ShouldSwitchTurn_ShouldReturnFalse_IfStartMethodIsNotCalled()
    {
        var timerMock = new Mock<ITimer>();
        timerMock.Setup(t => t.IsElapsed()).Returns(true);
        var timer = timerMock.Object;
        var timeCondition = new TimeCondition(timer);

        Assert.False(timeCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void ShouldSwitchTurn_ShouldResetInnerTimer_IfReturnedTrue()
    {
        var timer = new TickTimerMock();
        var turnTime = 10;
        timer.SetDuration(10);
        var timeCondition = new TimeCondition(timer);
        timeCondition.Start();
        
        timer.Tick(18);
        Assert.True(timeCondition.ShouldSwitchTurn());
        timer.Tick(5);
        Assert.False(timeCondition.ShouldSwitchTurn());
        timer.Tick(6);
        Assert.True(timeCondition.ShouldSwitchTurn());
    }
}