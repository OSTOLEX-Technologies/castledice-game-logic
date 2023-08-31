namespace castledice_game_logic.TurnsLogic;

/// <summary>
/// This interface describes functionality that provides player whose turn is now.
/// </summary>
public interface ICurrentPlayerProvider
{
    /// <summary>
    /// Returns player whose turn is now.
    /// </summary>
    /// <returns></returns>
    Player GetCurrentPlayer();
}