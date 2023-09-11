namespace castledice_game_logic.Penalties;

/// <summary>
/// Penalty classes represent rules which are checked after each turn.
/// If some players violate the rule, they are returned in the GetViolators method.
/// </summary>
public interface IPenalty
{
    List<Player> GetViolators();
}