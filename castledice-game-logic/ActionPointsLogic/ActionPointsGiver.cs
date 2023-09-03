using castledice_game_logic.Math;

namespace castledice_game_logic.ActionPointsLogic;

public class ActionPointsGiver
{
    private IRandomNumberGenerator _numberGenerator;
    private Player _player;

    public ActionPointsGiver(IRandomNumberGenerator numberGenerator, Player player)
    {
        _numberGenerator = numberGenerator;
        _player = player;
    }

    public GiveActionPointsAction GiveActionPoints()
    {
        int number = _numberGenerator.GetNextRandom();
        return new GiveActionPointsAction(_player, number);
    }
}