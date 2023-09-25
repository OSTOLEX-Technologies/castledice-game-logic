using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class PlacementListProviderMock : IPlacementListProvider
{
    public List<PlacementType> ListToReturn = new();
    
    public List<PlacementType> GetPlacementList(Player player)
    {
        return ListToReturn;
    }
}