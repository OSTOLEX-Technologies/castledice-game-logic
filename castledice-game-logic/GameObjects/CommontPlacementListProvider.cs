namespace castledice_game_logic.GameObjects;

/// <summary>
/// This class determines common list of available placeables for every player.
/// </summary>
public class CommontPlacementListProvider : IPlacementListProvider
{
    private List<PlacementType> _availablePlacementTypes;

    public CommontPlacementListProvider(List<PlacementType> availablePlacementTypes)
    {
        _availablePlacementTypes = availablePlacementTypes;
    }

    public List<PlacementType> GetPlacementList(Player player)
    {
        return _availablePlacementTypes;
    }
}