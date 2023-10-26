using castledice_game_logic.GameObjects;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests.GameObjectsTests.DecksTests;

public class CommonPlacementListProviderTests
{
    [Fact]
    public void GetPlacementList_ShouldReturnPlacementList_GivenInConstructor()
    {
        var placementList = new List<PlacementType>() { PlacementType.Knight, PlacementType.Bridge };
        var provider = new CommonPlacementListProvider(placementList);

        var actualList = provider.GetPlacementList(GetPlayer().Id);
        
        Assert.Equal(placementList.Count, actualList.Count);
        foreach (var placementType in placementList)
        {
            Assert.Contains(placementType, actualList);
        }
    }
}