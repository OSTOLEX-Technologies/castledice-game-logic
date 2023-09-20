using castledice_game_logic.Math;

namespace castledice_game_logic.MovesLogic;

public abstract class AbstractMove 
{
    private readonly Player _player;
    private readonly Vector2Int _position;

    public Player Player => _player;
    public Vector2Int Position => _position;

    protected AbstractMove(Player player, Vector2Int position)
    {
        _player = player;
        _position = position;
    }

    public abstract T Accept<T>(IMoveVisitor<T> visitor);

    protected bool Equals(AbstractMove other)
    {
        return _player.Equals(other._player) && _position.Equals(other._position);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AbstractMove)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_player, _position);
    }
}