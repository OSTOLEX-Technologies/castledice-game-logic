namespace castledice_game_logic.ActionPointsLogic;

public class GiveActionPointsSaver
{
    private ActionsHistory _history;

    public GiveActionPointsSaver(ActionsHistory history)
    {
        _history = history;
    }

    public void SaveAction(GiveActionPointsAction action)
    {
        _history.History.Add(action.GetSnapshot());
    }
}