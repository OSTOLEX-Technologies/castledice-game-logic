namespace castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation;

public interface ITscListCreator
{
    List<ITsc> GetTscList(List<TscType> tscTypes);
}