using Newtonsoft.Json.Converters;

namespace castledice_game_logic.ActionPointsLogic;
using Newtonsoft.Json;

[Serializable]
public sealed class GiveActionPointsSnapshot : IActionSnapshot
{
    public int PlayerId { get; }
    public int Amount { get; }

    [JsonConverter(typeof(StringEnumConverter))] 
    public ActionType ActionType { get; } = ActionType.GiveActionPoints;
    
    public GiveActionPointsSnapshot(int playerId, int amount)
    {
        PlayerId = playerId;
        Amount = amount;
    }

    public string GetJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    protected bool Equals(GiveActionPointsSnapshot other)
    {
        return PlayerId == other.PlayerId && Amount == other.Amount && ActionType == other.ActionType;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GiveActionPointsSnapshot)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PlayerId, Amount, (int)ActionType);
    }
}