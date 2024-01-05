using castledice_game_logic.Time;

namespace castledice_game_logic_tests.TimeTests;

public class StopwatchPlayerTimerTests
{
    [Fact]
    public void GetTimeLeft_ShouldReturnSmallerTimeSpan_AfterTimerWasStarted()
    {
        var rnd = new Random();
        var timeSpan = TimeSpan.FromMilliseconds(rnd.Next(100, 100000));
        var stopwatchPlayerTimer = new StopwatchPlayerTimer(timeSpan);
        
        stopwatchPlayerTimer.Start();
        Thread.Sleep(100);
        var actualTimeSpan = stopwatchPlayerTimer.GetTimeLeft();
        
        Assert.True(actualTimeSpan < timeSpan);
    }
    
    [Fact]
    //In this test we check that the time left is always less than the initial time span minus delta, where delta is a timer period between timer start and stop moments.
    public void GetTimeLeft_ShouldReturnTimeSpan_LessThenInitialTimeSpanMinusDelta()
    {
        var rnd = new Random();
        var timeSpan = TimeSpan.FromMilliseconds(rnd.Next(100, 100000));
        var delta = TimeSpan.FromMilliseconds(rnd.Next(100, 1000));
        var stopwatchPlayerTimer = new StopwatchPlayerTimer(timeSpan);
        
        stopwatchPlayerTimer.Start();
        Thread.Sleep(delta);
        stopwatchPlayerTimer.Stop();
        var actualTimeSpan = stopwatchPlayerTimer.GetTimeLeft();
        
        Assert.True(actualTimeSpan < timeSpan - delta);
    }
    
    [Fact]
    // In this test "SetOne" means the time span set by SetTimeLeft method.
    public void GetTimeLeft_ShouldReturnValueSmallerThanSetOne_IfTimerIsRunning()
    {
        var rnd = new Random();
        var timeSpan = TimeSpan.FromMilliseconds(rnd.Next(1000, 3000));
        var stopwatchPlayerTimer = new StopwatchPlayerTimer(timeSpan);
        
        stopwatchPlayerTimer.Start();
        var newTimeSpan = TimeSpan.FromMilliseconds(rnd.Next(400, 600));
        stopwatchPlayerTimer.SetTimeLeft(newTimeSpan);
        Thread.Sleep(100);
        var actualTimeSpan = stopwatchPlayerTimer.GetTimeLeft();
        
        Assert.True(actualTimeSpan < newTimeSpan);
    }
    
    [Fact]
    public void GetTimeLeft_ShouldReturnValue_LessThanSetOneMinusDelta_IfTimerIsStopped()
    {
        var rnd = new Random();
        var timeSpan = TimeSpan.FromMilliseconds(rnd.Next(1000, 3000));
        var delta = TimeSpan.FromMilliseconds(rnd.Next(100, 1000));
        var stopwatchPlayerTimer = new StopwatchPlayerTimer(timeSpan);
        
        stopwatchPlayerTimer.Start();
        var newTimeSpan = TimeSpan.FromMilliseconds(rnd.Next(4000, 6000));
        stopwatchPlayerTimer.SetTimeLeft(newTimeSpan);
        Thread.Sleep(delta);
        stopwatchPlayerTimer.Stop();
        var actualTimeSpan = stopwatchPlayerTimer.GetTimeLeft();
        
        Assert.True(actualTimeSpan < newTimeSpan - delta);
    }
    
    [Theory]
    [InlineData(100)]
    // In this test we check that the time left is always greater than the given time span minus delta with limit error.
    public void GetTimeLeft_ShouldReturnValue_BiggerThanSetOnMinusDeltaWithLimitError_IfTimerIsStopped(int limitErrorMilliseconds)
    {
        var rnd = new Random();
        var timeSpan = TimeSpan.FromMilliseconds(rnd.Next(1000, 3000));
        var delta = TimeSpan.FromMilliseconds(rnd.Next(100, 1000));
        var limitError = TimeSpan.FromMilliseconds(limitErrorMilliseconds);
        var stopwatchPlayerTimer = new StopwatchPlayerTimer(timeSpan);
        
        stopwatchPlayerTimer.Start();
        var newTimeSpan = TimeSpan.FromMilliseconds(rnd.Next(4000, 6000));
        stopwatchPlayerTimer.SetTimeLeft(newTimeSpan);
        Thread.Sleep(delta);
        stopwatchPlayerTimer.Stop();
        var actualTimeSpan = stopwatchPlayerTimer.GetTimeLeft();
        
        Assert.True(actualTimeSpan > newTimeSpan - delta - limitError);
    }
    
    [Fact]
    public void TimeIsUp_ShouldBeInvoked_WhenTimeIsUp()
    {
        var rnd = new Random();
        var timeSpan = TimeSpan.FromMilliseconds(rnd.Next(100, 300));
        var stopwatchPlayerTimer = new StopwatchPlayerTimer(timeSpan);
        var isInvoked = false;
        stopwatchPlayerTimer.TimeIsUp += () => isInvoked = true;
        
        stopwatchPlayerTimer.Start();
        Thread.Sleep(timeSpan.Add(TimeSpan.FromMilliseconds(100)));
        
        Assert.True(isInvoked);
    }
    
    [Fact]
    public void GetTimeLeft_ShouldReturnZero_AfterTimeIsUp()
    {
        var rnd = new Random();
        var timeSpan = TimeSpan.FromMilliseconds(rnd.Next(100, 300));
        var stopwatchPlayerTimer = new StopwatchPlayerTimer(timeSpan);
        
        stopwatchPlayerTimer.Start();
        Thread.Sleep(timeSpan.Add(TimeSpan.FromMilliseconds(100)));
        var actualTimeSpan = stopwatchPlayerTimer.GetTimeLeft();
        
        Assert.Equal(TimeSpan.Zero, actualTimeSpan);
    }
}