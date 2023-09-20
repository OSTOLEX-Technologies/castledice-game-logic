using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic;

public sealed class PlaceMove : AbstractMove
{
    private readonly IPlaceable _contentToPlace;

    public IPlaceable ContentToPlace => _contentToPlace;
    
    public PlaceMove(Player player, Vector2Int position, IPlaceable contentToPlace) : base(player, position)
    {
        _contentToPlace = contentToPlace;
    }

    public override T Accept<T>(IMoveVisitor<T> visitor)
    {
        return visitor.VisitPlaceMove(this);
    }

    private bool Equals(PlaceMove other)
    {
        return base.Equals(other) && _contentToPlace.Equals(other._contentToPlace);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((PlaceMove)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _contentToPlace);
    }
}