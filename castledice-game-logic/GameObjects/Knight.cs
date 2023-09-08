namespace castledice_game_logic.GameObjects;

public class Knight : Content, IPlayerOwned, IReplaceable, IPlaceBlocking, IPlaceable
{
    private Player _player;
    private int _health;
    private int _placementCost;

    public Knight(Player player, int placementCost, int health)
    {
        if (placementCost <= 0)
        {
            throw new ArgumentException("Placement cost must be positive!");
        }

        if (health <= 0)
        {
            throw new ArgumentException("Health must be positive!");
        }
        
        _player = player;
        _placementCost = placementCost;
        _health = health;
    }

    public Player GetOwner()
    {
        return _player;
    }

    public int GetReplaceCost()
    {
        return _health;
    }

    public int GetPlacementCost()
    {
        return _placementCost;
    }

    public bool CanBePlacedOn(Cell cell)
    {
        return true;
    }

    public PlacementType PlacementType => PlacementType.Knight;
    
    public bool IsBlocking()
    {
        return true;
    }
}
