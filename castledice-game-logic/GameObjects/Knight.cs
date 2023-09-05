namespace castledice_game_logic.GameObjects;

public class Knight : Content, IPlayerOwned, IUpgradeable, IReplaceable, IPlaceBlocking, IPlaceable
{
    private Player _player;
    private int _health;

    public Knight(Player player)
    {
        _player = player;
    }

    public Player GetOwner()
    {
        return _player;
    }

    public bool CanBeUpgraded()
    {
        return true;
    }

    
    //TODO: Write implementation for upgrade cost considering configs
    public int GetUpgradeCost()
    {
        return 2;
    }

    public void Upgrade()
    {
        throw new NotImplementedException();
    }

    public int GetReplaceCost()
    {
        return 1;
    }

    public int GetPlacementCost()
    {
        throw new NotImplementedException();
    }

    public bool CanBePlacedOn(Cell cell)
    {
        throw new NotImplementedException();
    }

    public PlacementType PlacementType { get; }
}
