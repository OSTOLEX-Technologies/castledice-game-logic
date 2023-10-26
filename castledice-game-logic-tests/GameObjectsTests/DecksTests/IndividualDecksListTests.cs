using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Decks;

namespace castledice_game_logic_tests.GameObjectsTests.DecksTests;

public class IndividualDecksListTests
{
    [Fact]
    public void GetDeck_ShouldReturnEmptyList_IfPlacementListForGivenIdIsAbsent()
    {
        var placementListProvider = new IndividualDecksList(new Dictionary<int, List<PlacementType>>());

        var list = placementListProvider.GetDeck(1);
        
        Assert.Empty(list);
    }

    [Theory]
    [MemberData(nameof(IdToPlacementListCases))]
    public void GetDeck_ShouldReturnAppropriatePlacementList_IfListForGivenIdExists(Dictionary<int, List<PlacementType>> idToPlacementList)
    {
        var placementListProvider = new IndividualDecksList(idToPlacementList);

        foreach (var id in idToPlacementList.Keys)
        {
            var actualList = placementListProvider.GetDeck(id);
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