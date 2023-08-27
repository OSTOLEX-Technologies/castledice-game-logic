namespace castledice_game_logic.GameObjects;

public class Castle : Content, ICapturable, IUpgradeable, IPlayerOwned
{
    private Player _player;
    
    public Castle(Player player)
    {
        _player = player;
    }

    public bool TryCapture(Player capturer)
    {
        throw new NotImplementedException();
    }

    public bool TryUpgrade(Player upgrader)
    {
        throw new NotImplementedException();
    }

    public Player GetOwner()
    {
        return _player;
    }
}
