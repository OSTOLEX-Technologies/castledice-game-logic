using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Decks;

namespace castledice_game_logic_tests.GameObjectsTests.DecksTests;

public class IndividualPlacementListProviderTests
{
    [Fact]
    public void GetPlacementList_ShouldReturnEmptyList_IfPlacementListForGivenIdIsAbsent()
    {
        var placementListProvider = new IndividualPlacementListProvider(new Dictionary<int, List<PlacementType>>());

        var list = placementListProvider.GetPlacementList(1);
        
        Assert.Empty(list);
    }

    [Theory]
    [MemberData(nameof(IdToPlacementListCases))]
    public void GetPlacementList_ShouldReturnAppropriatePlacementList_IfListForGivenIdExists(Dictionary<int, List<PlacementType>> idToPlacementList)
    {
        var placementListProvider = new IndividualPlacementListProvider(idToPlacementList);

        foreach (var id in idToPlacementList.Keys)
        {
            var actualList = placementListProvider.GetPlacementList(id);
            var expectedList = idToPlacementList[id];
            Assert.Same(expectedList, actualList);
        }
    }

    public static IEnumerable<object[]> IdToPlacementListCases()
    {
        yield return new object[]
        {
            new Dictionary<int, List<PlacementType>>
            {
                { 1, new List<PlacementType>() },
                { 2, new List<PlacementType>() },
                { 3, new List<PlacementType>() }
            }
        };
        yield return new object[]
        {
            new Dictionary<int, List<PlacementType>>
            {
                { 5, new List<PlacementType>() },
                { 9, new List<PlacementType>() },
                { 8, new List<PlacementType>() }
            }
        };
    }
}