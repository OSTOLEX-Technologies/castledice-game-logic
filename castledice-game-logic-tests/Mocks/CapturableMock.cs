using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class CapturableMock : Content, ICapturable, IPlayerOwned
{
    public Player Owner;
        
    public void Capture(Player capturer)
    {
    }

    public Player GetOwner()
    {
        return Owner;
    }
}