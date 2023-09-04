using castledice_game_logic.GameObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace castledice_game_logic.MovesLogic.Snapshots;

public class ReplaceMoveSnapshot : AbstractMoveSnapshot
{
    [JsonConverter(typeof(StringEnumConverter))]
    public PlacementType ReplacementType { get; }
    
    public override MoveType MoveType => MoveType.Replace;
    
    public ReplaceMoveSnapshot(ReplaceMove move, int moveCost) : base(move, moveCost)
    {
        ReplacementType = move.Replacement.PlacementType;
    }
    
    public override string GetJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    protected bool Equals(ReplaceMoveSnapshot other)
    {
        return base.Equals(other) && ReplacementType == other.ReplacementType;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ReplaceMoveSnapshot)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), (int)ReplacementType);
    }
}