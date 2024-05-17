namespace castledice_game_logic.ActionPointsLogic;

public class ChangeActionPointsSaver
{
    private readonly ActionsHistory _history;

    public ChangeActionPointsSaver(ActionsHistory history)
    {
        _history = history;
    }

    public void SaveAction(ChangeActionPointsAction action)
    {
        _history.AddActionSnapshot(action.GetSnapshot());
    }
}