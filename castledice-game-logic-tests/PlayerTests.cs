using castledice_game_logic;
using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic_tests;

public class PlayerTests
{
    [Fact]
    public void ActionPointsProperty_ShouldReturnPlayerActionPoints_GivenInConstructor()
    {
        var expectedPoints = new PlayerActionPoints();
        var id = 0;
        var player = new Player(expectedPoints, id);

        var actualPoints = player.ActionPoints;
        
        Assert.Same(expectedPoints, actualPoints);
    }

    [Fact]
    public void IdProperty_ShouldReturnId_GivenInConstructor()
    {
        var points = new PlayerActionPoints();
        var expectedId = 3;
        var player = new Player(points, expectedId);

        var actualId = player.Id;
        
        Assert.Equal(expectedId, actualId);
    }
}