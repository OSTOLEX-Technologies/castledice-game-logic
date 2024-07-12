using System.Collections;
using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic_tests.ActionPointsLogicTests;

public class ChangeActionPointsSnapshotTests
{
    public class GetJsonTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ChangeActionPointsSnapshot(1, 1),
                "{\"PlayerId\":1,\"Amount\":1,\"ActionType\":\"ChangeActionPoints\"}"
            };
            yield return new object[]
            {
                new ChangeActionPointsSnapshot(3, 4),
                "{\"PlayerId\":3,\"Amount\":4,\"ActionType\":\"ChangeActionPoints\"}"
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    [Theory]
    [ClassData(typeof(GetJsonTestCases))]
    public void GetJson_ShouldReturnJson_WithAppropriateData(ChangeActionPointsSnapshot snapshot,
        string expectedJson)
    {
        var actualJson = snapshot.GetJson();
        Assert.Equal(expectedJson, actualJson);
    }
}