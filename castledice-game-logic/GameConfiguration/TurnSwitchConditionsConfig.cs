using castledice_game_logic.TurnsLogic.TurnSwitchConditions;

namespace castledice_game_logic.GameConfiguration;

public sealed class TurnSwitchConditionsConfig
{
    public List<TscType> ConditionsToUse { get; }

    public TurnSwitchConditionsConfig(List<TscType> conditionsToUse)
    {
        ConditionsToUse = conditionsToUse;
    }
}