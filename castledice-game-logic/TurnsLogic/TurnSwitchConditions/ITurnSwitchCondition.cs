namespace castledice_game_logic.TurnsLogic;

/// <summary>
/// This interface provides functionality for checking if turn should go to the next player.
/// </summary>
public interface ITurnSwitchCondition
{
    bool ShouldSwitchTurn();
}