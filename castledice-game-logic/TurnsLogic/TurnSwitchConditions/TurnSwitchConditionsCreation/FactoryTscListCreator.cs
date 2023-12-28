namespace castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation;

public class FactoryTscListCreator : ITscListCreator
{
    private readonly ITscFactory _tscFactory;

    public FactoryTscListCreator(ITscFactory tscFactory)
    {
        _tscFactory = tscFactory;
    }

    public List<ITsc> GetTscList(List<TscType> tscTypes)
    {
        var tscList = new List<ITsc>();
        foreach (var tscType in tscTypes)
        {
            var tsc = _tscFactory.GetTsc(tscType);
            tscList.Add(tsc);
        }
        return tscList;
    }
}