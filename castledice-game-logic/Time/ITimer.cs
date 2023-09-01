namespace castledice_game_logic.Time;

public interface ITimer
{
    /// <summary>
    /// Sets how many milliseconds should pass before IsElapsed will start to return true.
    /// </summary>
    /// <param name="milliseconds"></param>
    void SetDuration(int milliseconds);
    
    void Start();
    
    void Stop();
    
    /// <summary>
    /// Returns true if the amount of passed time is bigger or equal to the duration that was set in SetDuration method
    /// </summary>
    /// <returns></returns>
    bool IsElapsed();
    
    /// <summary>
    /// Sets the amount of elapsed time to zero. Doesn't stop timer and doesn't resets duration that was set in SetDuration.
    /// </summary>
    void Reset();
}