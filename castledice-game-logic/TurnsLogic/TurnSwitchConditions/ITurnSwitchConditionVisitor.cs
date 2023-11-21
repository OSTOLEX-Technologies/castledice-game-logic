namespace castledice_game_logic.TurnsLogic;

public interface ITurnSwitchConditionVisitor<out T>
{
    T VisitActionPointsCondition(ActionPointsCondition actionPointsTsc);
    T VisitTimeCondition(TimeCondition timeCondition);
}