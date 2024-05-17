namespace castledice_game_logic.ActionPointsLogic;

public class ChangeActionPointsApplier
{
    public void ApplyAction(ChangeActionPointsAction action)
    {
        var player = action.Player;
        var amount = action.Amount;
        if (amount >= 0)
        {
            player.ActionPoints.IncreaseActionPoints(amount);
        }
        else
        {
            player.ActionPoints.DecreaseActionPoints(-amount);
        }
    }
}