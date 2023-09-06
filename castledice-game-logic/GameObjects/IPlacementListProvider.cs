namespace castledice_game_logic.GameObjects;

public interface IPlacementListProvider
{
    List<PlacementType> GetPlacementList(Player player);
}