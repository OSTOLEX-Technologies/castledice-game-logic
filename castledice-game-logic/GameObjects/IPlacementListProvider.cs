namespace castledice_game_logic.GameObjects;

/// <summary>
/// Placement list providers are classes that determine which placement types are available for given player.
/// </summary>
public interface IPlacementListProvider
{
    List<PlacementType> GetPlacementList(Player player);
}