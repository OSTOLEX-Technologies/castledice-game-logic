using castledice_game_logic_tests.Mocks;
using castledice_game_logic.Time;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions;
using Moq;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests.TurnsLogicTests;

public class TimeTscTests
{
    [Fact]
    public void ShouldSwitchTurn_ShouldReturnFalse_IfTimeIsNotElapsed()
    {
        var timerMock = new Mock<ITimer>();
        timerMock.Setup(t => t.IsElapsed()).Returns(false);
        var timer = timerMock.Object;
        var timeCondition = new TimeTsc(timer, 10, GetTurnsSwitcher(GetPlayer()));
        timeCondition.Start();
        
        Assert.False(timeCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void ShouldSwitchTurn_ShouldReturnTrue_IfTimeElapsed()
    {
        var timerMock = new Mock<ITimer>();
        timerMock.Setup(t => t.IsElapsed()).Returns(true);
        var timer = timerMock.Object;
        var timeCondition = new TimeTsc(timer, 10, GetTurnsSwitcher(GetPlayer()));
        timeCondition.Start();

        Assert.True(timeCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void ShouldSwitchTurn_ShouldReturnFalse_IfStartMethodIsNotCalled()
    {
        var timerMock = new Mock<ITimer>();
        timerMock.Setup(t => t.IsElapsed()).Returns(true);
        var timer = timerMock.Object;
        var timeCondition = new TimeTsc(timer, 30, GetTurnsSwitcher(GetPlayer()));

        Assert.False(timeCondition.ShouldSwitchTurn());
    }

    [Fact]
    public void ShouldSwitchTurn_ShouldResetInnerTimer_IfReturnedTrue()
    {
        int duration = 10;
        var timer = new TickTimerMock();
        var timeCondition = new TimeTsc(timer, duration, GetTurnsSwitcher(GetPlayer()));
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
        var timeCondition = new TimeTsc(timer, duration, turnSwitcher);
        timeCondition.Start();
        timer.Tick(18);
        
        turnSwitcher.SwitchTurn();
        
        Assert.False(timeCondition.ShouldSwitchTurn());
    }
    
    [Fact]
    public void Accept_ShouldCallVisitTimeCondition_OnGivenVisitor()
    {
        var visitorMock = new Mock<ITurnSwitchConditionVisitor<bool>>();
        var timerMock = new Mock<ITimer>();
        var timeCondition = new TimeTsc(timerMock.Object, 10, GetTurnsSwitcher(GetPlayer()));
        
        timeCondition.Accept(visitorMock.Object);
        
        visitorMock.Verify(visitor => visitor.VisitTimeCondition(timeCondition));
    }
}