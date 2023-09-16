using castledice_game_logic.Time;

namespace castledice_game_logic.TurnsLogic;

public class TimeCondition : ITurnSwitchCondition
{
    private readonly ITimer _timer;
    private bool _isStarted = false;

    public TimeCondition(ITimer timer, int turnDuration)
    {
        _timer = timer;
        timer.SetDuration(turnDuration);
    }

    public void Start()
    {
        _timer.Start();
        _isStarted = true;
    }

    public bool ShouldSwitchTurn()
    {
        if (!_isStarted)
        {
            return false;
        }

        if (_timer.IsElapsed())
        {
            _timer.Reset();
            return true;
        }

        return false;
    }
}