namespace castledice_game_logic.ActionPointsLogic;

public class GiveActionPointsApplier
{
    public void ApplyAction(GiveActionPointsAction action)
    {
        var player = action.Player;
        var amount = action.Amount;
        player.ActionPoints.IncreaseActionPoints(amount);
    }
}