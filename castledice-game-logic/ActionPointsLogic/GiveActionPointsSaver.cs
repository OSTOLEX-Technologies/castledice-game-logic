namespace castledice_game_logic.ActionPointsLogic;

public class GiveActionPointsSaver
{
    private readonly ActionsHistory _history;

    public GiveActionPointsSaver(ActionsHistory history)
    {
        _history = history;
    }

    public void SaveAction(GiveActionPointsAction action)
    {
        _history.AddActionSnapshot(action.GetSnapshot());
    }
}