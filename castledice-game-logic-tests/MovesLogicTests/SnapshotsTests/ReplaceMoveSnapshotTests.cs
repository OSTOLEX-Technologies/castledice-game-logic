using System.Collections;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.MovesLogic.Snapshots;

namespace castledice_game_logic_tests.SnapshotsTests;
using static ObjectCreationUtility;

public class ReplaceMoveSnapshotTests
{
    private class GetJsonTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ReplaceMoveBuilder()
                {
                    Replacement = new PlaceableMock()
                    {
                        PlacementTypeToReturn = PlacementType.Knight
                    },
                    Player = GetPlayer(id: 2283),
                    Position = (5, 15)
                }.Build(),
                "{\"ReplacementType\":\"Knight\",\"MoveType\":\"Replace\",\"ActionType\":\"Move\",\"PlayerId\":2283,\"Position\":{\"X\":5,\"Y\":15}}"
            };
            yield return new object[]
            {
                new ReplaceMoveBuilder()
                {
                    Replacement = new PlaceableMock()
                    {
                        PlacementTypeToReturn = PlacementType.HeavyKnight
                    },
                    Player = GetPlayer(id: 1111),
                    Position = (1, 1)
                }.Build(),
                "{\"ReplacementType\":\"HeavyKnight\",\"MoveType\":\"Replace\",\"ActionType\":\"Move\",\"PlayerId\":1111,\"Position\":{\"X\":1,\"Y\":1}}"
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    [Fact]
    public void MoveTypeProperty_ShouldAlwaysReturnReplaceMove()
    {
        var move = new ReplaceMoveBuilder().Build();
        var snapshot = new ReplaceMoveSnapshot(move);
        var expectedMoveType = MoveType.Replace;

        var actualMoveType = snapshot.MoveType;
        
        Assert.Equal(expectedMoveType, actualMoveType);
    }
    
    [Fact]
    public void ReplacementTypeProperty_ShouldReturnPlacementType_EqualToMoveReplacementType()
    {
        var expectedPlacementType = PlacementType.Knight;
        var placement = new PlaceableMock() { PlacementTypeToReturn = expectedPlacementType };
        var move = new ReplaceMoveBuilder() { Replacement = placement }.Build();
        var snapshot = new ReplaceMoveSnapshot(move);

        var actualPlacementType = snapshot.ReplacementType;
        
        Assert.Equal(expectedPlacementType,actualPlacementType);
    }
    
    [Theory]
    [ClassData(typeof(GetJsonTestCases))]
    public void GetJson_ShouldReturnJson_WithAppropriateData(ReplaceMove move, string expectedJson)
    {
        var snapshot = new ReplaceMoveSnapshot(move);

        var actualJson = snapshot.GetJson();
        
        Assert.Equal(expectedJson,actualJson);
    }
}