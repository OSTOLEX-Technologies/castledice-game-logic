namespace castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation.ActionPointsTscCreation;

public class ActionPointsTscCreator : IActionPointsTscCreator
{
    private readonly ICurrentPlayerProvider _currentPlayerProvider;

    public ActionPointsTscCreator(ICurrentPlayerProvider currentPlayerProvider)
    {
        _currentPlayerProvider = currentPlayerProvider;
    }

    public ActionPointsTsc CreateActionPointsTsc()
    {
        return new ActionPointsTsc(_currentPlayerProvider);
    }
}