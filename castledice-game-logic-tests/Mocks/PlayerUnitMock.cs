using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class PlayerUnitMock : Content, IPlayerOwned, IReplaceable, IPlaceBlocking
{
    public Player Owner;
    public int RemoveCost;
    public bool CanBeRemoved;
    
    public Player GetOwner()
    {
        return Owner;
    }

    public int GetReplaceCost(int replacementCost)
    {
        return RemoveCost + replacementCost - 1;
    }

    public int GetMinimalReplaceCost()
    {
        return RemoveCost;
    }

    public bool Replace(Player remover, int replacementCost)
    {
        throw new NotImplementedException();
    }
}