using castledice_game_logic.GameObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace castledice_game_logic.MovesLogic.Snapshots;

public sealed class ReplaceMoveSnapshot : AbstractMoveSnapshot
{
    [JsonConverter(typeof(StringEnumConverter))]
    public PlacementType ReplacementType { get; }
    
    public ReplaceMoveSnapshot(ReplaceMove move) : base(move)
    {
        ReplacementType = move.Replacement.PlacementType;
    }

    public override MoveType MoveType => MoveType.Replace;
    
    public override string GetJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    private bool Equals(ReplaceMoveSnapshot other)
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