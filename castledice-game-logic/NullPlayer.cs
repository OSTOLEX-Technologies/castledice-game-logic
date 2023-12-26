using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.Time;

namespace castledice_game_logic;

public sealed class NullPlayer : Player
{
    public NullPlayer() : base(new PlayerActionPoints(), new NullPlayerTimer(), 0)
    {
    }

    public override bool IsNull => true;
}