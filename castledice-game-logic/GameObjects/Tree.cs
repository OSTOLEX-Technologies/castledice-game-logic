namespace castledice_game_logic.GameObjects;

public class Tree : Content, IPlaceBlocking, IRemovable
{
    private readonly int _removeCost;
    private readonly bool _canBeRemoved;
    
    public Tree(int removeCost, bool canBeRemoved)
    {
        if (removeCost <= 0)
        {
            throw new ArgumentException("Remove cost must be positive!");
        }
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

    public override void Update()
    {
        
    }

    public override T Accept<T>(IContentVisitor<T> visitor)
    {
        return visitor.VisitTree(this);
    }
}