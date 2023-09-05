using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class ReplaceableMock : Content, IReplaceable, IPlayerOwned
{
    public int ReplaceCost;
    public bool CanReplace = true;
    public Player Owner;

    public int GetReplaceCost()
    {
        return ReplaceCost;
    }

    public bool CanBeReplaced()
    {
        return CanReplace;
    }
    
    public Player GetOwner()
    {
        return Owner;
    }
}