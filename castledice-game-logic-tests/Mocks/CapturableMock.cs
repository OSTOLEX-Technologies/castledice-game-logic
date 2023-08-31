using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class CapturableMock : Content, ICapturable, IPlayerOwned
{
    public Player Owner;
        
    public bool Capture(Player capturer)
    {
        return false;
    }

    public Player GetOwner()
    {
        return Owner;
    }
}