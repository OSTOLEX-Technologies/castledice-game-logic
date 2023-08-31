namespace castledice_game_logic.GameObjects;

public interface IRemovable
{
    bool CanBeRemoved()
    {
        return true;
    }

    int GetRemoveCost(int replacementCost);
    
    /// <summary>
    /// Returns remove cost that can be obtained by passing minimal possible replacement cost in GetRemoveCost method.
    /// </summary>
    /// <returns></returns>
    int GetMinimalRemoveCost();
    
    bool TryRemove(Player remover, int replacementCost);
}