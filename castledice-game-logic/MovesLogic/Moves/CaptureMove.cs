using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic;

public class CaptureMove : AbstractMove
{
    public CaptureMove(Player player, Vector2Int position) : base(player, position)
    {
    }

    public override bool Accept(IMoveVisitor visitor)
    {
        return visitor.VisitCaptureMove(this);
    }
}