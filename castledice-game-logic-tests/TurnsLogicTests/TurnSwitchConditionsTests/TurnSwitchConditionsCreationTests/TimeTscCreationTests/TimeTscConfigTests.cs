using castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation.TimeTscCreation;

namespace castledice_game_logic_tests.TurnsLogicTests.TurnSwitchConditionsTests.TurnSwitchConditionsCreationTests.TimeTscCreationTests;

public class TimeTscConfigTests
{
    [Fact]
    public void Properties_ShouldReturnValues_GivenInConstructor()
    {
        var rnd = new Random();
        var turnDuration = rnd.Next(1, 10000);
        var timeTscConfig = new TimeTscConfig(turnDuration);
        
        Assert.Equal(turnDuration, timeTscConfig.TurnDurationMilliseconds);
    }
    
    [Fact]
    public void Equals_ShouldReturnTrue_IfObjectsAreEqual()
    {
        var rnd = new Random();
        var turnDuration = rnd.Next(1, 10000);
        var timeTscConfig1 = new TimeTscConfig(turnDuration);
        var timeTscConfig2 = new TimeTscConfig(turnDuration);
        
        Assert.True(timeTscConfig1.Equals(timeTscConfig2));
    }
    
    [Fact]
    public void Equals_ShouldReturnFalse_IfObjectsAreNotEqual()
    {
        var rnd = new Random();
        var turnDuration1 = rnd.Next(1, 10000);
        var turnDuration2 = rnd.Next(1, 10000);
        var timeTscConfig1 = new TimeTscConfig(turnDuration1);
        var timeTscConfig2 = new TimeTscConfig(turnDuration2);
        
        Assert.False(timeTscConfig1.Equals(timeTscConfig2));
    }
}