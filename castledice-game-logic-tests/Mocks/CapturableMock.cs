using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class CapturableMock : Content, ICapturable, IPlayerOwned, IPlaceBlocking
{
    public Player Owner = new NullPlayer();
    public bool CanCapture = true;
    public Func<Player, int> GetCaptureCostHitFunc = (p) => 1;
    public Func<Player, int> CaptureHitsLeftFunc = (p) => 0;
        
    public void CaptureHit(Player capturer)
    {
        Owner = capturer;
    }

    public bool CanBeCaptured(Player capturer)
    {
        return CanCapture;
    }

    public int GetCaptureHitCost(Player capturer)
    {
        return GetCaptureCostHitFunc(capturer);
    }

    public void Free()
    {
        Owner = new NullPlayer();
    }

    public int CaptureHitsLeft(Player capturer)
    {
        return CaptureHitsLeftFunc(capturer);
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

    public override T Accept<T>(IContentVisitor<T> visitor)
    {
        throw new NotImplementedException();
    }
}