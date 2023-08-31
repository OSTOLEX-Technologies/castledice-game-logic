using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using static castledice_game_logic_tests.ObjectCreationUtility;


namespace castledice_game_logic_tests;


public class AbstractMoveTests
{
    [Fact]
    public void PlayerProperty_ShouldReturnPlayer_GivenInConstructor()
    {
        var player = GetPlayer();
        var position = new Vector2Int(0, 0);
        var move = new TestMove(player, position);

        var actualPlayer = move.Player;
        
        Assert.Same(player, actualPlayer);
    }

    [Fact]
    public void PositionProperty_ShouldReturnPosition_GivenInConstructor()
    {
        var player = GetPlayer();
        var position = new Vector2Int(0, 0);
        var move = new TestMove(player, position);

        var actualPosition = move.Position;
        
        Assert.Equal(position, actualPosition);
    }
}