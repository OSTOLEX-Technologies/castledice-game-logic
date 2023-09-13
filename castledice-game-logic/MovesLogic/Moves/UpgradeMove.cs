using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic;

public sealed class UpgradeMove : AbstractMove
{
    public UpgradeMove(Player player, Vector2Int position) : base(player, position)
    {
    }

    public override bool Accept(IMoveVisitor visitor)
    {
        return visitor.VisitUpgradeMove(this);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((UpgradeMove)obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}