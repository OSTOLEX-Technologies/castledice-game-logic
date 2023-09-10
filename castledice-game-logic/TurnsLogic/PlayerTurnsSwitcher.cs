namespace castledice_game_logic.TurnsLogic;

public class PlayerTurnsSwitcher : ICurrentPlayerProvider
{
    public event EventHandler TurnSwitched; 
    
    private PlayersList _players;
    private int _current = 0;

    public PlayerTurnsSwitcher(PlayersList players)
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
        TurnSwitched?.Invoke(this, EventArgs.Empty);
    }
}