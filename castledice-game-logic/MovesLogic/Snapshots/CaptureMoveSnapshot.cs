using Newtonsoft.Json;

namespace castledice_game_logic.MovesLogic.Snapshots;

[Serializable]
public sealed class CaptureMoveSnapshot : AbstractMoveSnapshot
{
    public override MoveType MoveType => MoveType.Capture;
    
    public CaptureMoveSnapshot(CaptureMove move) : base(move)
    {
    }
    
    public override string GetJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CaptureMoveSnapshot)obj);
    }
    
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}