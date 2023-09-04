using System.Collections;
using castledice_game_logic_tests.Mocks;
using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.MovesLogic.Snapshots;

namespace castledice_game_logic_tests.SnapshotsTests;
using static ObjectCreationUtility;

public class PlaceMoveSnapshotTests
{
    private class GetJsonTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                3,
                new PlaceMoveBuilder()
                {
                    Content = new PlaceableMock()
                    {
                        PlacementTypeToReturn = PlacementType.Knight
                    },
                    Player = GetPlayer(id: 1234),
                    Position = (1, 2)
                }.Build(),
                
                "{\"PlacementType\":\"Knight\",\"MoveType\":\"Place\",\"ActionType\":\"Move\",\"PlayerId\":1234,\"Position\":{\"X\":1,\"Y\":2},\"MoveCost\":3}"
            };
            yield return new object[]
            {
                4,
                new PlaceMoveBuilder()
                {
                    Content = new PlaceableMock()
                    {
                        PlacementTypeToReturn = PlacementType.HeavyKnight
                    },
                    Player = GetPlayer(id: 100),
                    Position = (3, 5)
                }.Build(),
                "{\"PlacementType\":\"HeavyKnight\",\"MoveType\":\"Place\",\"ActionType\":\"Move\",\"PlayerId\":100,\"Position\":{\"X\":3,\"Y\":5},\"MoveCost\":4}"
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    [Fact]
    public void MoveTypeProperty_ShouldAlwaysReturnPlaceMove()
    {
        var move = new PlaceMoveBuilder().Build();
        var snapshot = new PlaceMoveSnapshot(move, 0);
        var expectedMoveType = MoveType.Place;

        var actualMoveType = snapshot.MoveType;
        
        Assert.Equal(expectedMoveType, actualMoveType);
    }
    
    [Fact]
    public void PlacementTypeProperty_ShouldReturnPlacementType_EqualToMovePlacementType()
    {
        var expectedPlacementType = PlacementType.Knight;
        var placement = new PlaceableMock() { PlacementTypeToReturn = expectedPlacementType };
        var move = new PlaceMoveBuilder() { Content = placement }.Build();
        var snapshot = new PlaceMoveSnapshot(move, 0);

        var actualPlacementType = snapshot.PlacementType;
        
        Assert.Equal(expectedPlacementType,actualPlacementType);
    }

    [Theory]
    [ClassData(typeof(GetJsonTestCases))]
    public void GetJson_ShouldReturnJson_WithAppropriateData(int moveCost, PlaceMove move, string expectedJson)
    {
        var snapshot = new PlaceMoveSnapshot(move, moveCost);

        var actualJson = snapshot.GetJson();
        
        Assert.Equal(expectedJson,actualJson);
    }
}