using castledice_game_logic;
using castledice_game_logic.Math;

namespace castledice_game_logic_tests.SnapshotsTests;
using static ObjectCreationUtility;

public class AbstractMoveSnapshotTests
{
    [Fact]
    public void ActionTypeProperty_ShouldAlwaysBeEqualToMove()
    {
        var move = new TestMoveBuilder().Build();
        var snapshot = new TestMoveSnapshot(move);
        var expectedActionType = ActionType.Move;
        
        var actualActionType = snapshot.ActionType;
        
        Assert.Equal(expectedActionType, actualActionType);
    }

    [Fact]
    public void PlayerIdProperty_ShouldReturnIdOfPlayer_FromMovePlayerProperty()
    {
        var player = GetPlayer(id: 1234);
        var move = new TestMoveBuilder(){Player = player}.Build();
        var snapshot = new TestMoveSnapshot(move);
        var expectedId = move.Player.Id;

        var actualId = snapshot.PlayerId;
        
        Assert.Equal(expectedId, actualId);
    }

    [Fact]
    public void PositionProperty_ShouldReturnPosition_EqualToMovePositionProperty()
    {
        Vector2Int expectedPosition = (1, 3);
        var move = new TestMoveBuilder() { Position = expectedPosition }.Build();
        var snapshot = new TestMoveSnapshot(move);

        var actualPosition = snapshot.Position;
        
        Assert.Equal(expectedPosition, actualPosition);
    }
}