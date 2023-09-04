using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class CapturableMock : Content, ICapturable, IPlayerOwned
{
    public Player Owner;
    public bool CanCapture = true;
    public Func<Player, int> GetCaptureCostFunc;
        
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

    public Player GetOwner()
    {
        return Owner;
    }
}