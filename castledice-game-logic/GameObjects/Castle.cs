namespace castledice_game_logic.GameObjects;

public class Castle : Content, ICapturable, IUpgradeable, IPlayerOwned, IPlaceBlocking
{
    private Player _player;
    
    public Castle(Player player)
    {
        _player = player;
    }

    public void Capture(Player capturer)
    {
        throw new NotImplementedException();
    }

    public bool CanBeCaptured(Player capturer)
    {
        return true;
    }

    public bool CanBeUpgraded()
    {
        return true;
    }

    //TODO: Write implementation with configs
    public int GetUpgradeCost()
    {
        return 2;
    }

    public void Upgrade(Player upgrader)
    {
        throw new NotImplementedException();
    }

    public Player GetOwner()
    {
        return _player;
    }
}
