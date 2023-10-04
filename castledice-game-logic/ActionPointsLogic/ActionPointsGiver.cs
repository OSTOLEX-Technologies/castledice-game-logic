using castledice_game_logic.Math;

namespace castledice_game_logic.ActionPointsLogic;

public class ActionPointsGiver
{
    private readonly Player _player;

    public ActionPointsGiver(Player player)
    {
        _player = player;
    }

    public GiveActionPointsAction GiveActionPoints(int amount)
    {
        return new GiveActionPointsAction(_player, amount);
    }
}