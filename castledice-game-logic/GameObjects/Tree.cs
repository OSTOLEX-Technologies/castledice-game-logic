namespace castledice_game_logic.GameObjects;

public class Tree : Content, IPlaceBlocking, IRemovable
{
    private int _removeCost;
    private bool _canBeRemoved;
    
    public Tree(int removeCost, bool canBeRemoved)
    {
        _removeCost = removeCost;
        _canBeRemoved = canBeRemoved;
    }
    
    public bool CanBeRemoved()
    {
        return _canBeRemoved;
    }

    public int GetRemoveCost()
    {
        return _removeCost;
    }

    public bool IsBlocking()
    {
        return true;
    }
}