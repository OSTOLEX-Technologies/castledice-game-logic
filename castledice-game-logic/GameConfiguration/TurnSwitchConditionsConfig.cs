using castledice_game_logic.TurnsLogic.TurnSwitchConditions;

namespace castledice_game_logic.GameConfiguration;

public class TurnSwitchConditionsConfig
{
    public List<TscType> ConditionsToUse { get; set; } = new();
}