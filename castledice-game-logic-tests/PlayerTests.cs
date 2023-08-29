using castledice_game_logic;
using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic_tests;

public class PlayerTests
{
    [Fact]
    public void ActionPointsProperty_ShouldReturnPlayerActionPoints_GivenInConstructor()
    {
        var actionPoints = new PlayerActionPoints();
        var player = new Player(actionPoints);

        var actualActionPoints = player.ActionPoints;
        
        Assert.Same(actionPoints, actualActionPoints);
    }
}