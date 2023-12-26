using castledice_game_logic;
using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.Time;
using Moq;

namespace castledice_game_logic_tests;

public class PlayerTests
{
    [Fact]
    public void ActionPointsProperty_ShouldReturnPlayerActionPoints_GivenInConstructor()
    {
        var expectedPoints = new PlayerActionPoints();
        var id = 0;
        var player = new Player(expectedPoints, new Mock<IPlayerTimer>().Object, id);

        var actualPoints = player.ActionPoints;
        
        Assert.Same(expectedPoints, actualPoints);
    }

    [Fact]
    public void IdProperty_ShouldReturnId_GivenInConstructor()
    {
        var points = new PlayerActionPoints();
        var expectedId = 3;
        var player = new Player(points, new Mock<IPlayerTimer>().Object, expectedId);

        var actualId = player.Id;
        
        Assert.Equal(expectedId, actualId);
    }
    
    [Fact]
    public void TimerProperty_ShouldReturnPlayerTimer_GivenInConstructor()
    {
        var points = new PlayerActionPoints();
        var expectedTimer = new Mock<IPlayerTimer>().Object;
        var id = 0;
        var player = new Player(points, expectedTimer, id);

        var actualTimer = player.Timer;
        
        Assert.Same(expectedTimer, actualTimer);
    }
}