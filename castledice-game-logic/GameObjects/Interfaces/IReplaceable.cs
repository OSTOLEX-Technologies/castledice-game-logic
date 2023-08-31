namespace castledice_game_logic.GameObjects;

public interface IReplaceable
{
    bool CanBeReplaced()
    {
        return true;
    }

    int GetReplaceCost(int replacementCost);
    
    /// <summary>
    /// Returns remove cost that can be obtained by passing minimal possible replacement cost in GetReplaceCost method.
    /// </summary>
    /// <returns></returns>
    int GetMinimalReplaceCost();
    
    bool Replace(Player remover, int replacementCost);
}