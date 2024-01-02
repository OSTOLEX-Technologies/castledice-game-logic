namespace castledice_game_logic.GameObjects.Decks;

/// <summary>
/// Placement list providers are classes that determine which placement types are available for given player.
/// </summary>
internal interface IDecksList
{
    List<PlacementType> GetDeck(int playerId);
}