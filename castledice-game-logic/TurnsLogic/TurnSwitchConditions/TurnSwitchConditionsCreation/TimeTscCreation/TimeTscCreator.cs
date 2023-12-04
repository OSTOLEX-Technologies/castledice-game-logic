using castledice_game_logic.Time;

namespace castledice_game_logic.TurnsLogic.TurnSwitchConditions.TurnSwitchConditionsCreation.TimeTscCreation;

public class TimeTscCreator : ITimeTscCreator
{
    private readonly PlayerTurnsSwitcher _turnsSwitcher;
    private readonly TimeTscConfig _timeTscConfig;
    private readonly ITimer _timer;

    public TimeTscCreator(TimeTscConfig timeTscConfig, ITimer timer, PlayerTurnsSwitcher turnsSwitcher)
    {
        _turnsSwitcher = turnsSwitcher;
        _timeTscConfig = timeTscConfig;
        _timer = timer;
    }

    public TimeTsc CreateTimeTsc()
    {
        return new TimeTsc(_timer, _timeTscConfig.TurnDurationMilliseconds, _turnsSwitcher);
    }
}