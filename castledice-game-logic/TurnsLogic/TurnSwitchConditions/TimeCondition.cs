using castledice_game_logic.Time;

namespace castledice_game_logic.TurnsLogic;

public class TimeCondition : ITurnSwitchCondition
{
    private ITimer _timer;
    private bool _isStarted = false;

    public TimeCondition(ITimer timer)
    {
        _timer = timer;
    }

    public void Start()
    {
        _isStarted = true;
        _timer.Start();
    }

    public bool ShouldSwitchTurn()
    {
        if (!_isStarted)
        {
            return false;
        }
        return _timer.IsElapsed();
    }
}