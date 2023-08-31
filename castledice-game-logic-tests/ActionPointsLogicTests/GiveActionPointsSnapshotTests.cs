using System.Collections;
using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic_tests;

public class GiveActionPointsSnapshotTests
{
    public class GetJsonTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new GiveActionPointsSnapshot(1, 1),
                "{\"PlayerId\":1,\"Amount\":1,\"ActionType\":\"GiveActionPoints\"}"
            };
            yield return new object[]
            {
                new GiveActionPointsSnapshot(3, 4),
                "{\"PlayerId\":3,\"Amount\":4,\"ActionType\":\"GiveActionPoints\"}"
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    [Theory]
    [ClassData(typeof(GetJsonTestCases))]
    public void GetJson_ShouldReturnJson_WithAppropriateData(GiveActionPointsSnapshot snapshot,
        string expectedJson)
    {
        var actualJson = snapshot.GetJson();
        Assert.Equal(expectedJson, actualJson);
    }
}