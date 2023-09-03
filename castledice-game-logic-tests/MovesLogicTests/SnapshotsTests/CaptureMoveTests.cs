using System.Collections;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.MovesLogic.Snapshots;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests.SnapshotsTests;

public class CaptureMoveTests
{
    private class GetJsonTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new CaptureMoveBuilder()
                {
                    Player = GetPlayer(id: 1234),
                    Position = (1, 2)
                }.Build(),
                "{\"MoveType\":\"Capture\",\"ActionType\":\"Move\",\"PlayerId\":1234,\"Position\":{\"X\":1,\"Y\":2}}"
            };
            yield return new object[]
            {
                new CaptureMoveBuilder()
                {
                    Player = GetPlayer(id: 100),
                    Position = (3, 5)
                }.Build(),
                "{\"MoveType\":\"Capture\",\"ActionType\":\"Move\",\"PlayerId\":100,\"Position\":{\"X\":3,\"Y\":5}}"
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    [Fact]
    public void MoveTypeProperty_ShouldAlwaysBeEqualToCapture()
    {
        var move = new CaptureMoveBuilder().Build();
        var snapshot = new CaptureMoveSnapshot(move);
        var expectedMoveType = MoveType.Capture;
        
        var actualMoveType = snapshot.MoveType;
        
        Assert.Equal(expectedMoveType, actualMoveType);
    }
    
    [Theory]
    [ClassData(typeof(GetJsonTestCases))]
    public void GetJson_ShouldReturnJson_WithAppropriateData(CaptureMove move, string expectedJson)
    {
        var snapshot = new CaptureMoveSnapshot(move);

        var actualJson = snapshot.GetJson();
        
        Assert.Equal(expectedJson, actualJson);
    }
}