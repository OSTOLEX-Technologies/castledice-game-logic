using castledice_game_logic.GameObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace castledice_game_logic.MovesLogic.Snapshots;

[Serializable]
public class PlaceMoveSnapshot : AbstractMoveSnapshot
{
    [JsonConverter(typeof(StringEnumConverter))]
    public PlacementType PlacementType { get; }
    
    public override MoveType MoveType => MoveType.Place;
    
    public PlaceMoveSnapshot(PlaceMove move, int moveCost) : base(move, moveCost)
    {
        PlacementType = move.ContentToPlace.PlacementType;
    }
    
    public override string GetJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    protected bool Equals(PlaceMoveSnapshot other)
    {
        return base.Equals(other) && PlacementType == other.PlacementType;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((PlaceMoveSnapshot)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), (int)PlacementType);
    }
}