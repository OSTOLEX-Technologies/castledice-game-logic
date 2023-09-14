using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class CapturableMock : Content, ICapturable, IPlayerOwned, IPlaceBlocking
{
    public Player Owner = new NullPlayer();
    public bool CanCapture = true;
    public Func<Player, int> GetCaptureCostFunc = (p) => 1;
        
    public void Capture(Player capturer)
    {
        Owner = capturer;
    }

    public bool CanBeCaptured(Player capturer)
    {
        return CanCapture;
    }

    public int GetCaptureCost(Player capturer)
    {
        return GetCaptureCostFunc(capturer);
    }

    public void Free()
    {
        Owner = new NullPlayer();
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