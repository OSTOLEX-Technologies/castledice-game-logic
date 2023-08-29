namespace castledice_game_logic.GameObjects;

public interface IPlaceable
{
    int GetPlacementCost();
    bool CanBePlacedOn(Cell cell);
}