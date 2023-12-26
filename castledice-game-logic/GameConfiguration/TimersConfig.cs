namespace castledice_game_logic.GameConfiguration;

public class TimersConfig
{
    private readonly Dictionary<int, TimeSpan> _playerIdToTimeSpan;

    public TimersConfig(Dictionary<int, TimeSpan> playerIdToTimeSpan)
    {
        _playerIdToTimeSpan = playerIdToTimeSpan;
    }
    
    public TimeSpan GetTimeSpanForPlayer(int playerId)
    {
        return _playerIdToTimeSpan[playerId];
    }
}