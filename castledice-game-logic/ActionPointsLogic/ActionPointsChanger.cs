using castledice_game_logic.Math;

namespace castledice_game_logic.ActionPointsLogic;

public class ActionPointsChanger
{
    private readonly Player _player;

    public ActionPointsChanger(Player player)
    {
        _player = player;
    }

    public ChangeActionPointsAction ChangeActionPoints(int amount)
    {
        return new ChangeActionPointsAction(_player, amount);
    }
}