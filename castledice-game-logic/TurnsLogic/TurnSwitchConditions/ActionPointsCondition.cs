namespace castledice_game_logic.TurnsLogic;

public class ActionPointsCondition : ITurnSwitchCondition
{
    private readonly ICurrentPlayerProvider _currentPlayerProvider;

    public ActionPointsCondition(ICurrentPlayerProvider currentPlayerProvider)
    {
        _currentPlayerProvider = currentPlayerProvider;
    }

    public bool ShouldSwitchTurn()
    {
        var currentPlayer = _currentPlayerProvider.GetCurrentPlayer();
        return currentPlayer.ActionPoints.Amount <= 0;
    }

    public T Accept<T>(ITurnSwitchConditionVisitor<T> visitor)
    {
        return visitor.VisitActionPointsCondition(this);
    }
}