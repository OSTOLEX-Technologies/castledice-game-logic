namespace castledice_game_logic.GameObjects.Factories;

public interface IPlaceablesFactory
{
    IPlaceable CreatePlaceable(PlacementType placementType, Player creator);
}