namespace castledice_game_logic.Time;

public interface IPlayerTimer
{
    void Start();
    void Stop();
    TimeSpan GetTimeLeft();
    void SetTimeLeft(TimeSpan timeSpan);
    event Action TimeIsUp;
}