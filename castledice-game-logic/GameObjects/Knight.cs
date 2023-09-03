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

    public int GetReplaceCost(int replacementCost)
    {
        return _health + replacementCost - 1;
    }

    public int GetMinimalReplaceCost()
    {
        return _health;
    }

    public void Replace(Player remover, int replacementCost)
    {
        throw new NotImplementedException();
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