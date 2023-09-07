namespace castledice_game_logic.GameObjects;

public interface IRemovable
{
    bool CanBeRemoved();
    
    int GetRemoveCost();
}