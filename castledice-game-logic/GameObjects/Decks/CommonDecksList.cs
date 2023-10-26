namespace castledice_game_logic.GameObjects;

/// <summary>
/// This class determines common list of available placeables for every player.
/// </summary>
public class CommonDecksList : IDecksList
{
    private readonly List<PlacementType> _availablePlacementTypes;

    public CommonDecksList(List<PlacementType> availablePlacementTypes)
    {
        _availablePlacementTypes = availablePlacementTypes;
    }

    public List<PlacementType> GetDeck(int playerId)
    {
        return _availablePlacementTypes;
    }
}