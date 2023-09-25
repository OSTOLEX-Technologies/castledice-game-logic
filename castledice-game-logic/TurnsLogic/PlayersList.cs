using System.Collections;

namespace castledice_game_logic.TurnsLogic;

/// <summary>
/// This class represents players that are in the game. Provides KickPlayer method for kicking players which leads to invocation of PlayerKicked event.
/// </summary>
public class PlayersList : IEnumerable<Player>
{
    public int Count => _players.Count;
    public event EventHandler<Player>? PlayerKicked;
    
    private readonly List<Player> _players = new();
    
    public void AddPlayer(Player player)
    {
        if (_players.Contains(player))
        {
            return;
        }

        if (_players.Exists(p => p.Id == player.Id))
        {
            throw new ArgumentException("Player with id: " + player.Id + " already exists in players list!");
        }

        _players.Add(player);
    }

    public PlayersList()
    {
        
    }

    public PlayersList(IEnumerable<Player> players)
    {
        foreach (var player in players)
        {
            AddPlayer(player);
        }
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
                throw new ArgumentException("Invalid index: " + i + "!");
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