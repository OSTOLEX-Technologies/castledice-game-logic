namespace castledice_game_logic.GameObjects;

public interface IRemovable
{
    bool CanBeRemoved()
    {
        return true;
    }
    
    bool TryRemove(Player remover, int replacementCost);
}