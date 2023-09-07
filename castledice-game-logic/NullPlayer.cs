using castledice_game_logic.ActionPointsLogic;

namespace castledice_game_logic;

public sealed class NullPlayer : Player
{
    public NullPlayer() : base(new PlayerActionPoints(), 0)
    {
    }

    public override bool IsNull => true;
}