using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class RemovableMock : Content, IRemovable, IPlaceBlocking
{
    public bool CanRemove = true;
    public int RemoveCost = 1;
    
    public bool CanBeRemoved()
    {
        return CanRemove;
    }

    public int GetRemoveCost()
    {
        return RemoveCost;
    }

    public bool IsBlocking()
    {
        return true;
    }

    public override void Update()
    {
        
    }

    public override void Accept(IContentVisitor visitor)
    {
        
    }
}