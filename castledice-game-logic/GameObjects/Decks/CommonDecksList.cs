using castledice_game_logic.GameObjects.Decks;

namespace castledice_game_logic.GameObjects;

/// <summary>
/// This class determines common list of available placeables for every player.
/// </summary>
internal class CommonDecksList : IDecksList
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