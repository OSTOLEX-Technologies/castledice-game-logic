using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class CapturableMock : Content, ICapturable, IPlayerOwned
{
    public Player Owner;
    public bool CanCapture = true;
        
    public void Capture(Player capturer)
    {
        Owner = capturer;
    }

    public bool CanBeCaptured(Player capturer)
    {
        return CanCapture;
    }

    public Player GetOwner()
    {
        return Owner;
    }
}