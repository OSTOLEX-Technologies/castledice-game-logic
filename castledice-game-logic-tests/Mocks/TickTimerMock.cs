using castledice_game_logic.Time;

namespace castledice_game_logic_tests.Mocks;

/// <summary>
/// Timer mock in which you can easily control the amount of time passed by using Tick method
/// </summary>
public class TickTimerMock : ITimer
{
    private int _duration;
    private int _elapsedTicks;
    private bool _enabled;
    
    public void SetDuration(int milliseconds)
    {
        _duration = milliseconds;
    }

    public void Start()
    {
        _enabled = true;
    }

    public void Stop()
    {
        _enabled = false;
    }

    public bool IsElapsed()
    {
        return _elapsedTicks >= _duration && _enabled;
    }

    public void Reset()
    {
        _elapsedTicks = 0;
    }

    
    /// <summary>
    /// This method increases amount of elapsed time.
    /// It doesn't work if timer isn`t started.
    /// </summary>
    /// <param name="ticksAmount"></param>
    public void Tick(int ticksAmount = 1)
    {
        if (!_enabled)
        {
            return;
        }
        _elapsedTicks += ticksAmount;
    }
}