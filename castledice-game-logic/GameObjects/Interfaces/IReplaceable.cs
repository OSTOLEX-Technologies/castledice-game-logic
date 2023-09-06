namespace castledice_game_logic.GameObjects;

public interface IReplaceable
{
    bool CanBeReplaced()
    {
        return true;
    }

    /// <summary>
    /// Returns replace cost that doesn't take into account replacement cost.
    /// </summary>
    /// <returns></returns>
    int GetReplaceCost();
}