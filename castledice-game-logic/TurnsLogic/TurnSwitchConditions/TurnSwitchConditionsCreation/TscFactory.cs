using castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation.ActionPointsTscCreation;

namespace castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation;

public class TscFactory : ITscFactory
{
    private readonly IActionPointsTscCreator _actionPointsTscCreator;
    
    public TscFactory(IActionPointsTscCreator actionPointsTscCreator)
    {
        _actionPointsTscCreator = actionPointsTscCreator;
    }
    
    public ITsc GetTsc(TscType tscType)
    {
        return tscType switch
        {
            TscType.SwitchByActionPoints => _actionPointsTscCreator.CreateActionPointsTsc(),
            _ => throw new NotImplementedException("Handling code for TscType " + tscType + " is not implemented.")
        };
    }
}