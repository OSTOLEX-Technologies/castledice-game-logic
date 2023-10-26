namespace castledice_game_logic.GameObjects.Factories;

public class PlaceablesFactory : IPlaceablesFactory
{
    private readonly IKnightsFactory _knightsFactory;

    public PlaceablesFactory(IKnightsFactory knightsFactory)
    {
        _knightsFactory = knightsFactory;
    }

    public IPlaceable CreatePlaceable(PlacementType placementType, Player creator)
    {
        switch (placementType)
        {
            case PlacementType.Knight:
                return _knightsFactory.GetKnight(creator);
            default:
                throw new InvalidOperationException("Unknown placement type: " + placementType);
        }
    }
}