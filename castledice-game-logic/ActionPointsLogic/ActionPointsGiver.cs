using castledice_game_logic.Math;

namespace castledice_game_logic.ActionPointsLogic;

public class ActionPointsGiver
{
    private IRandomNumberGenerator _numberGenerator;
    private Player _player;
    private int _minAmount;
    private int _maxAmount;

    public ActionPointsGiver(IRandomNumberGenerator numberGenerator, Player player, int minAmount, int maxAmount)
    {
        _numberGenerator = numberGenerator;
        _player = player;
        _minAmount = minAmount;
        _maxAmount = maxAmount;
    }

    public GiveActionPointsAction GiveActionPoints()
    {
        int number = _numberGenerator.GetRandom(_minAmount, _maxAmount + 1);
        return new GiveActionPointsAction(_player, number);
    }
}