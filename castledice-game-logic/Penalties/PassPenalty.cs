using castledice_game_logic.TurnsLogic;

namespace castledice_game_logic.Penalties;

/// <summary>
/// This penalty is applied if player passes his turn several times in a row.
/// Turn is considered to be passed if player didn't spend any action points.
/// </summary>
public class PassPenalty : IPenalty
{
    public List<Player> GetViolators()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Parameter maxPassCount must be positive.
    /// </summary>
    /// <param name="maxPassCount"></param>
    /// <param name="turnsSwitcher"></param>
    public PassPenalty(int maxPassCount, PlayerTurnsSwitcher turnsSwitcher)
    {
        
    }

    public int GetPassCount(Player player)
    {
        return 0;
    }
}