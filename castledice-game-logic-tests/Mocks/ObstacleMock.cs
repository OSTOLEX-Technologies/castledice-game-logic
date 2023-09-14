using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class ObstacleMock : Content, IPlaceBlocking
{
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