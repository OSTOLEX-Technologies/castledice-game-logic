using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class CapturableMock : Content, ICapturable, IPlayerOwned
{
    private Player _owner;

    public CapturableMock(Player owner)
    {
        _owner = owner;
    }
        
    public bool TryCapture(Player capturer)
    {
        return false;
    }

    public Player GetOwner()
    {
        return _owner;
    }
}