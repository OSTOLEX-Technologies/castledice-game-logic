namespace castledice_game_logic.TurnsLogic;

public class PlayerTurnsSwitcher : ICurrentPlayerProvider
{
    private List<Player> _players;
    private int _current = 0;

    public PlayerTurnsSwitcher(List<Player> players)
    {
        _players = players;
    }

    public Player GetCurrentPlayer()
    {
        return _players[_current];
    }

    public void SwitchTurn()
    {
        _current++;
        if (_current >= _players.Count)
        {
            _current = 0;
        }
    }
}