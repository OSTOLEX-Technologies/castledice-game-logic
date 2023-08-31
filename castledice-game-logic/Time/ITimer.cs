namespace castledice_game_logic.Time;

public interface ITimer
{
    void SetDuration(int milliseconds);
    void Start();
    void Stop();
    bool IsElapsed();
    void Reset();
}