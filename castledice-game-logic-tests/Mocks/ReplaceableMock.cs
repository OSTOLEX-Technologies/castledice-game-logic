using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class ReplaceableMock : Content, IReplaceable, IPlayerOwned, IPlaceBlocking
{
    public int ReplaceCost;
    public Player Owner = new NullPlayer();

    public int GetReplaceCost()
    {
        return ReplaceCost;
    }
    
    public Player GetOwner()
    {
        return Owner;
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