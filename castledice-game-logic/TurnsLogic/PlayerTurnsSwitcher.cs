namespace castledice_game_logic.TurnsLogic;

public class PlayerTurnsSwitcher : ICurrentPlayerProvider, IPreviousPlayerProvider
{
    public event EventHandler? TurnSwitched; 
    
    private readonly PlayersList _players;
    private int _current = 0;

    public PlayerTurnsSwitcher(PlayersList players)
    {
        if (players.Count == 0)
        {
            throw new ArgumentException("Players list can't be empty!");
        }
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

    public Player GetPreviousPlayer()
    {
        throw new NotImplementedException();
    }
}