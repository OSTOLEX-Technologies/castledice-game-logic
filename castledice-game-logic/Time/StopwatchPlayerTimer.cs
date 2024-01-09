using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace castledice_game_logic.Time;

/// <summary>
/// This class counts down time using a <see cref="Timer"/> and a <see cref="Stopwatch"/>.
/// It also provides a way to get the left time amount with GetTimeLeft method.
/// After the time is up, the TimeIsUp event is invoked and the timer stops.
/// </summary>
public class StopwatchPlayerTimer : IPlayerTimer
{
    private readonly Timer _timer;
    private readonly Stopwatch _stopwatch;
    private TimeSpan _timeLeft;
    private bool _isRunning;
    
    public StopwatchPlayerTimer(TimeSpan timeSpan)
    {
        _timeLeft = timeSpan;
        _stopwatch = new Stopwatch();
        _timer = new Timer(timeSpan.TotalMilliseconds);
        _timer.Elapsed += OnTimeElapsed;
    }

    public void Start()
    {
        _isRunning = true;
        _timer.Start();
        _stopwatch.Start();
    }

    public void Stop()
    {
        _isRunning = false;
        _timer.Stop();
        _stopwatch.Stop();
        _timeLeft = _timeLeft.Subtract(_stopwatch.Elapsed);
        _stopwatch.Reset();
    }

    public TimeSpan GetTimeLeft()
    {
        return _isRunning ? _timeLeft.Subtract(_stopwatch.Elapsed) : _timeLeft;
    }

    public void SetTimeLeft(TimeSpan timeSpan)
    {
        _timeLeft = timeSpan;
        _timer.Interval = timeSpan.TotalMilliseconds;
    }
    
    public event Action? TimeIsUp;
    
    private void OnTimeElapsed(object sender, ElapsedEventArgs e)
    {
        _timeLeft = TimeSpan.Zero;
        _isRunning = false;
        _stopwatch.Stop();
        _timer.Stop();
        TimeIsUp?.Invoke();
    }
    
    ~StopwatchPlayerTimer()
    {
        _timer.Dispose();
    }
}