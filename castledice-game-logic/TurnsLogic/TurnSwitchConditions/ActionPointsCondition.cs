namespace castledice_game_logic.TurnsLogic;

public class ActionPointsCondition : ITurnSwitchCondition
{
    public bool ShouldSwitchTurn(Player currentPlayer)
    {
        return currentPlayer.ActionPoints.Amount <= 0;
    }
}