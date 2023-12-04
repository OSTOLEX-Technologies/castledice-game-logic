using System.Reflection;
using castledice_game_logic.Time;
using castledice_game_logic.TurnsLogic;
using static castledice_game_logic_tests.ObjectCreationUtility;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation.TimeTscCreation;
using Moq;

namespace castledice_game_logic_tests.TurnsLogicTests.TurnSwitchConditionsTests.TurnSwitchConditionsCreationTests.TimeTscCreationTests;

public class TimeTscCreatorTests
{
    [Fact]
    public void CreateTimeTsc_ShouldReturnTimeTsc_WithTurnDurationFromConfig()
    {
        var rnd = new Random();
        var turnDuration = rnd.Next(1, 10000);
        var timeTscConfig = new TimeTscConfig(turnDuration);
        var timer = new Mock<ITimer>().Object;
        var turnsSwitcher = GetTurnsSwitcher(GetPlayer());
        var timeTscCreator = new TimeTscCreator(timeTscConfig, timer, turnsSwitcher);
        
        var timeTsc = timeTscCreator.CreateTimeTsc();
        var fieldInfo = timeTsc.GetType().GetField("_turnDurationMilliseconds", BindingFlags.NonPublic | BindingFlags.Instance);
        var actualTurnDuration = (int) fieldInfo.GetValue(timeTsc);
        
        Assert.Equal(turnDuration, actualTurnDuration);
    }

    [Fact]
    public void CreateTimeTsc_ShouldReturnTimeTsc_WithGivenTimer()
    {
        var config = new TimeTscConfig(1);
        var timer = new Mock<ITimer>().Object;
        var turnsSwitcher = GetTurnsSwitcher(GetPlayer());
        var timeTscCreator = new TimeTscCreator(config, timer, turnsSwitcher);
        
        var timeTsc = timeTscCreator.CreateTimeTsc();
        var fieldInfo = timeTsc.GetType().GetField("_timer", BindingFlags.NonPublic | BindingFlags.Instance);
        var actualTimer = (ITimer) fieldInfo.GetValue(timeTsc);
        
        Assert.Same(timer, actualTimer);
    }
    
    [Fact]
    public void CreateTimeTsc_ShouldReturnTimeTsc_WithGivenTurnsSwitcher()
    {
        var config = new TimeTscConfig(1);
        var timer = new Mock<ITimer>().Object;
        var turnsSwitcher = GetTurnsSwitcher(GetPlayer());
        var timeTscCreator = new TimeTscCreator(config, timer, turnsSwitcher);
        
        var timeTsc = timeTscCreator.CreateTimeTsc();
        var fieldInfo = timeTsc.GetType().GetField("_turnsSwitcher", BindingFlags.NonPublic | BindingFlags.Instance);
        var actualTurnsSwitcher = (PlayerTurnsSwitcher) fieldInfo.GetValue(timeTsc);
        
        Assert.Same(turnsSwitcher, actualTurnsSwitcher);
    }
}