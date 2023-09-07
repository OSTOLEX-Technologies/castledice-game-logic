using System.Collections;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.MovesLogic.Snapshots;

namespace castledice_game_logic_tests.SnapshotsTests;
using static ObjectCreationUtility;


public class RemoveMoveSnapshotTests
{
    private class GetJsonTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new RemoveMoveBuilder()
                {
                    Player = GetPlayer(id: 3257),
                    Position = (3, 2)
                }.Build(),
                "{\"MoveType\":\"Remove\",\"ActionType\":\"Move\",\"PlayerId\":3257,\"Position\":{\"X\":3,\"Y\":2}}"
            };
            yield return new object[]
            {
                new RemoveMoveBuilder()
                {
                    Player = GetPlayer(id: 1505),
                    Position = (2, 8)
                }.Build(),
                "{\"MoveType\":\"Remove\",\"ActionType\":\"Move\",\"PlayerId\":1505,\"Position\":{\"X\":2,\"Y\":8}}"
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    [Fact]
    public void MoveTypeProperty_ShouldAlwaysReturnUpgradeMove()
    {
        var move = new RemoveMoveBuilder().Build();
        var snapshot = new RemoveMoveSnapshot(move);
        var expectedMoveType = MoveType.Remove;

        var actualMoveType = snapshot.MoveType;
        
        Assert.Equal(expectedMoveType, actualMoveType);
    }
    
    [Theory]
    [ClassData(typeof(GetJsonTestCases))]
    public void GetJson_ShouldReturnJson_WithAppropriateData(RemoveMove move, string expectedJson)
    {
        var snapshot = new RemoveMoveSnapshot(move);

        var actualJson = snapshot.GetJson();
        
        Assert.Equal(expectedJson, actualJson);
    }
}