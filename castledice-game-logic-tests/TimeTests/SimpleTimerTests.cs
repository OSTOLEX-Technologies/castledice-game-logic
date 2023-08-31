using castledice_game_logic.Time;

namespace castledice_game_logic_tests.TimeTests;

public class SimpleTimerTests
{
    [Fact]
    public void IsElapsed_ShouldReturnTrue_IfTimeElapsed()
    {
        int milliseconds = 3;
        var timer = new SimpleTimer();
        timer.SetDuration(milliseconds);
        timer.Start();
        Thread.Sleep(100);
        
        Assert.True(timer.IsElapsed());
        timer.Close();
    }
    
    [Fact]
    public void IsElapsed_ShouldReturnFalse_IfStartNotCalled()
    {
        int milliseconds = 3;
        var timer = new SimpleTimer();
        timer.SetDuration(milliseconds);
        Thread.Sleep(100);
        
        Assert.False(timer.IsElapsed());
        timer.Close();
    }

    [Fact]
    public void IsElapsed_ShouldReturnFalse_IfTimerStoppedBeforeElapsing()
    {
        int milliseconds = 50;
        var timer = new SimpleTimer();
        timer.SetDuration(milliseconds);
        timer.Start();
        Thread.Sleep(5);
        timer.Stop();
        Thread.Sleep(10);
        
        Assert.False(timer.IsElapsed());
        timer.Close();
    }

    [Fact]
    public void IsElapsed_ShouldReturnFalse_AfterResetCalled()
    {
        int milliseconds = 3;
        var timer = new SimpleTimer();
        timer.SetDuration(milliseconds);
        timer.Start();
        Thread.Sleep(100);
        timer.Reset();
        
        Assert.False(timer.IsElapsed());
        timer.Close();
    }

    [Fact]
    public void IsElapsed_ShouldReturnTrue_AfterResetEndTimeElapsing()
    {
        int milliseconds = 50;
        var timer = new SimpleTimer();
        timer.SetDuration(milliseconds);
        timer.Start();
        Thread.Sleep(100);
        timer.Reset();
        timer.SetDuration(milliseconds);
        timer.Start();
        Thread.Sleep(100);
        
        Assert.True(timer.IsElapsed());
        timer.Close();
    }
}