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
}