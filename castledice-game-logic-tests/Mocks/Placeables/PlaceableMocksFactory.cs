using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Factories;

namespace castledice_game_logic_tests.Mocks;

public class PlaceableMocksFactory : IPlaceablesFactory
{
    public Dictionary<PlacementType, IPlaceable> TypeAndPlaceableToReturn = new Dictionary<PlacementType, IPlaceable>()
    {
        { PlacementType.Knight, new PlaceableMock(){PlacementTypeToReturn = PlacementType.Knight}},
        { PlacementType.HeavyKnight, new PlaceableMock(){PlacementTypeToReturn = PlacementType.HeavyKnight}},
        { PlacementType.Bridge, new PlaceableMock(){PlacementTypeToReturn = PlacementType.Bridge}}
    };
    
    public IPlaceable CreatePlaceable(PlacementType placementType, Player creator)
    {
        return TypeAndPlaceableToReturn[placementType];
    }
}