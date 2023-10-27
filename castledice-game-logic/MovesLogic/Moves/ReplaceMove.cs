using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic;

public sealed class ReplaceMove : AbstractMove
{
    private readonly IPlaceable _replacement;

    public IPlaceable Replacement => _replacement;
    
    public ReplaceMove(Player player, Vector2Int position, IPlaceable replacement) : base(player, position)
    {
        _replacement = replacement;
    }

    public override T Accept<T>(IMoveVisitor<T> visitor)
    {
        return visitor.VisitReplaceMove(this);
    }

    private bool Equals(ReplaceMove other)
    {
        return base.Equals(other) && _replacement.Equals(other._replacement);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ReplaceMove)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _replacement);
    }
}