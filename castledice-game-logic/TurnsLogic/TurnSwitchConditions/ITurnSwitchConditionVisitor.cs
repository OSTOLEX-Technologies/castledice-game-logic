namespace castledice_game_logic.TurnsLogic.TurnSwitchConditions;

public interface ITurnSwitchConditionVisitor<out T>
{
    T VisitActionPointsCondition(ActionPointsTsc actionPointsTsc);
    T VisitTimeCondition(TimeTsc timeTsc);
}