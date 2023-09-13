using castledice_game_logic.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace castledice_game_logic.MovesLogic.Snapshots;

[Serializable]
public abstract class AbstractMoveSnapshot : IActionSnapshot
{
    private readonly Vector2Int _position;

    [JsonConverter(typeof(StringEnumConverter))] 

    public ActionType ActionType { get; } = ActionType.Move;

    public int PlayerId { get; }

    public Vector2Int Position => _position;
    
    [JsonConverter(typeof(StringEnumConverter))] 

    public abstract MoveType MoveType { get; }
    
    protected AbstractMoveSnapshot(AbstractMove move)
    {
        PlayerId = move.Player.Id;
        _position = move.Position;
    }

    public abstract string GetJson();

    protected bool Equals(AbstractMoveSnapshot other)
    {
        return ActionType == other.ActionType && PlayerId == other.PlayerId && _position.Equals(other._position) && MoveType == other.MoveType;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AbstractMoveSnapshot)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)ActionType, PlayerId, _position, (int)MoveType);
    }
}
