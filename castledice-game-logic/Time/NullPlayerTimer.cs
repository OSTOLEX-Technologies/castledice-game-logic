namespace castledice_game_logic.Time;

public class NullPlayerTimer : IPlayerTimer
{
    public void Start()
    {
    }

    public void Stop()
    {
    }

    public TimeSpan GetTimeLeft()
    {
        return TimeSpan.Zero;
    }

    public void SetTimeLeft(TimeSpan timeSpan)
    {
    }

    public event Action? TimeIsUp;
}