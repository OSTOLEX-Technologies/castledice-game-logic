namespace castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation;

public interface ITscFactory
{
    ITsc GetTsc(TscType tscType);
}