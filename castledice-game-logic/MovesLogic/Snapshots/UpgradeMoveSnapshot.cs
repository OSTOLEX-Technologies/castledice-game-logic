using Newtonsoft.Json;

namespace castledice_game_logic.MovesLogic.Snapshots;

[Serializable]
public class UpgradeMoveSnapshot : AbstractMoveSnapshot
{
    public UpgradeMoveSnapshot(UpgradeMove move) : base(move)
    {
    }
    
    public override MoveType MoveType => MoveType.Upgrade;
    
    public override string GetJson()
    {
        return JsonConvert.SerializeObject(this);
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((UpgradeMoveSnapshot)obj);
    }
    
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}