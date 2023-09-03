using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class PlaceableMock : Content, IPlaceable
{
    public int Cost;
    public bool CanPlaceOn = true;
    public PlacementType PlacementTypeToReturn;
    
    public int GetPlacementCost()
    {
        return Cost;
    }

    public bool CanBePlacedOn(Cell cell)
    {
        return CanPlaceOn;
    }

    public PlacementType PlacementType
    {
        get
        {
            return PlacementTypeToReturn;
        }
    }
}