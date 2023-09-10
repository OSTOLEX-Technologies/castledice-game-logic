using castledice_game_logic.TurnsLogic;

namespace castledice_game_logic.Penalties;

/// <summary>
/// This penalty is applied if player passes his turn several times in a row.
/// Turn is considered to be passed if player didn't spend any action points.
/// </summary>
public class PassPenalty : IPenalty
{
    private class PlayerSpending
    {
        public Player Player;
        public int ActionPointsSpent;
    }
    
    private PlayerTurnsSwitcher _turnsSwitcher;
    private Dictionary<Player, int> _playersPassCounts = new();
    private int _maxPassCount;
    private PlayerSpending _lastPlayerSpending;
    

    /// <summary>
    /// Parameter maxPassCount must be positive.
    /// </summary>
    /// <param name="maxPassCount"></param>
    /// <param name="turnsSwitcher"></param>
    public PassPenalty(int maxPassCount, PlayerTurnsSwitcher turnsSwitcher)
    {
        if (maxPassCount <= 0)
        {
            throw new ArgumentException("Max pass count must be positive");
        }
        _maxPassCount = maxPassCount;
        _turnsSwitcher = turnsSwitcher;
        _turnsSwitcher.TurnSwitched += OnTurnSwitched;
        SwitchToCurrentPlayer();
    }
    
    public List<Player> GetViolators()
    {
        List<Player> violators = new List<Player>();
        foreach (var passCount in _playersPassCounts)
        {
            if (passCount.Value >= _maxPassCount)
            {
                violators.Add(passCount.Key);
            }
        }
        return violators;
    }

    public int GetPassCount(Player player)
    {
        if (_playersPassCounts.ContainsKey(player))
        {
            return _playersPassCounts[player];
        }
        return 0;
    }

    private void OnTurnSwitched(object? sender, EventArgs e)
    {
        CheckLastPlayerSpending();
        UnsubscribeFromLastPlayer();
        SwitchToCurrentPlayer();
    }

    private void SwitchToCurrentPlayer()
    {
        var currentPlayer = _turnsSwitcher.GetCurrentPlayer();
        _lastPlayerSpending = new PlayerSpending()
        {
            Player = currentPlayer,
            ActionPointsSpent = 0
        };
        currentPlayer.ActionPoints.ActionPointsDecreased += OnActionPointsDecreased;
    }
    
    private void OnActionPointsDecreased(object? sender, int decreaseAmount)
    {
        _lastPlayerSpending.ActionPointsSpent += decreaseAmount;
        _playersPassCounts[_lastPlayerSpending.Player] = 0;
    }

    private void CheckLastPlayerSpending()
    {
        var actionPointsSpent = _lastPlayerSpending.ActionPointsSpent;
        var player = _lastPlayerSpending.Player;
        if (actionPointsSpent == 0)
        {
            if (!_playersPassCounts.ContainsKey(player))
            {
                _playersPassCounts.Add(player, 1);
            }
            else
            {
                _playersPassCounts[player]++;
            }
        }
    }

    private void UnsubscribeFromLastPlayer()
    {
        var lastPlayer = _lastPlayerSpending.Player;
        lastPlayer.ActionPoints.ActionPointsDecreased -= OnActionPointsDecreased;
    }
    
    ~PassPenalty()
    {
        _turnsSwitcher.TurnSwitched -= OnTurnSwitched;
    }
}