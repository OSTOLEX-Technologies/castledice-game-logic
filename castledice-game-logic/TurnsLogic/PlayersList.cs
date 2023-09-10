using System.Collections;

namespace castledice_game_logic.TurnsLogic;

/// <summary>
/// This class represents players that are in the game. Provides KickPlayer method for kicking players which leads to invocation of PlayerKicked event.
/// </summary>
public class PlayersList : IEnumerable<Player>
{
    public event EventHandler<Player> PlayerKicked; 
    private List<Player> _players = new();
    
    public void AddPlayer(Player player)
    {
        if (_players.Contains(player))
        {
            return;
        }
        _players.Add(player);
    }
    
    public void KickPlayer(Player player)
    {
        _players.Remove(player);
        PlayerKicked?.Invoke(this, player);
    }

    public Player this[int i]
    {
        get
        {
            if (i < 0 || i >= _players.Count)
            {
                throw new IndexOutOfRangeException();
            }
            return _players[i];
        }
    }

    public IEnumerator<Player> GetEnumerator()
    {
        return _players.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}