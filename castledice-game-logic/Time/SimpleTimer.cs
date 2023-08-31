using System.Timers;
using Timer = System.Timers.Timer;

namespace castledice_game_logic.Time;

public class SimpleTimer : ITimer
{
    private Timer _timer;
    private bool _elapsed;
    
    public SimpleTimer()
    {
        Reset();
    }
    
    public void Reset()
    {
        _timer = new Timer();
        _timer.Close();
        _timer.AutoReset = false;
        _timer.Elapsed += Elapse;
        _elapsed = false;
    }
    
    private void Elapse(object? sender, ElapsedEventArgs e)
    {
        _elapsed = true;
    }
    
    public void Start()
    {
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
    }

    public void SetDuration(int milliseconds)
    {
        _timer.Interval = milliseconds;
    }
    
    public bool IsElapsed()
    {
        return _elapsed;
    }

    public void Close()
    {
        _timer.Close();
    }
}