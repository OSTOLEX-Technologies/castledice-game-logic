﻿using castledice_game_logic.Time;

namespace castledice_game_logic.TurnsLogic.TurnSwitchConditions;

public class TimeTsc : ITsc
{
    private readonly ITimer _timer;
    private bool _isStarted = false;
    private readonly PlayerTurnsSwitcher _turnsSwitcher;
    private readonly int _turnDurationMilliseconds;

    public TimeTsc(ITimer timer, int turnDurationMilliseconds, PlayerTurnsSwitcher turnSwitcher)
    {
        _timer = timer;
        timer.SetDuration(turnDurationMilliseconds);
        _turnsSwitcher = turnSwitcher;
        _turnDurationMilliseconds = turnDurationMilliseconds;
        _turnsSwitcher.TurnSwitched += OnTurnSwitched;
    }
    
    ~TimeTsc()
    {
        _turnsSwitcher.TurnSwitched -= OnTurnSwitched;
    }

    private void OnTurnSwitched(object sender, EventArgs e)
    {
        _timer.ResetTimer();
    }


    public void Start()
    {
        _timer.StartTimer();
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
            _timer.ResetTimer();
            return true;
        }

        return false;
    }
    
}