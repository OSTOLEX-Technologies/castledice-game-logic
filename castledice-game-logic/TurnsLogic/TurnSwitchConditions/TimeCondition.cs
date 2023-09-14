using castledice_game_logic.Time;

namespace castledice_game_logic.TurnsLogic;

public class TimeCondition : ITurnSwitchCondition
{
    private readonly ITimer _timer;
    private bool _isStarted = false;

    public TimeCondition(ITimer timer)
    {
        _timer = timer;
    }

    public void Start()
    {
        _timer.Start();
        _isStarted = true;
    }

    public bool ShouldSwitchTurn(Player currentPlayer)
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