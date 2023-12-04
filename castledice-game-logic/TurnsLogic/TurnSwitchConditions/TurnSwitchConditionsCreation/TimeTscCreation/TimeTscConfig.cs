namespace castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation.TimeTscCreation;

public sealed class TimeTscConfig
{
    public int TurnDurationMilliseconds { get; }
    
    public TimeTscConfig(int turnDurationMilliseconds)
    {
        TurnDurationMilliseconds = turnDurationMilliseconds;
    }

    private bool Equals(TimeTscConfig other)
    {
        return TurnDurationMilliseconds == other.TurnDurationMilliseconds;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is TimeTscConfig other && Equals(other);
    }

    public override int GetHashCode()
    {
        return TurnDurationMilliseconds;
    }
}