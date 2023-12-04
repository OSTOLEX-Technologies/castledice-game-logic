namespace castledice_game_logic.TurnsLogic.TurnSwitchConditions;

/// <summary>
/// Tsc stands for Turn Switch Condition.
/// This interface provides functionality for checking if turn should go to the next player.
/// </summary>
public interface ITsc
{
    bool ShouldSwitchTurn();
}