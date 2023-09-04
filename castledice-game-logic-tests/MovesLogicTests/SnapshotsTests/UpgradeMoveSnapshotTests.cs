using System.Collections;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.MovesLogic.Snapshots;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests.SnapshotsTests;

public class UpgradeMoveSnapshotTests
{
    private class GetJsonTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new UpgradeMoveBuilder()
                {
                    Player = GetPlayer(id: 3257),
                    Position = (3, 2)
                }.Build(),
                "{\"MoveType\":\"Upgrade\",\"ActionType\":\"Move\",\"PlayerId\":3257,\"Position\":{\"X\":3,\"Y\":2}}"
            };
            yield return new object[]
            {
                new UpgradeMoveBuilder()
                {
                    Player = GetPlayer(id: 1505),
                    Position = (2, 8)
                }.Build(),
                "{\"MoveType\":\"Upgrade\",\"ActionType\":\"Move\",\"PlayerId\":1505,\"Position\":{\"X\":2,\"Y\":8}}"
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
        var move = new UpgradeMoveBuilder().Build();
        var snapshot = new UpgradeMoveSnapshot(move);
        var expectedMoveType = MoveType.Upgrade;

        var actualMoveType = snapshot.MoveType;
        
        Assert.Equal(expectedMoveType, actualMoveType);
    }

    [Theory]
    [ClassData(typeof(GetJsonTestCases))]
    public void GetJson_ShouldReturnJson_WithAppropriateData(UpgradeMove move, string expectedJson)
    {
        var snapshot = new UpgradeMoveSnapshot(move);

        var actualJson = snapshot.GetJson();
        
        Assert.Equal(expectedJson, actualJson);
    }
}