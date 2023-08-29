using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class PlayerUnitMock : Content, IPlayerOwned, IRemovable, IPlaceBlocking
{
    public Player Owner;
    public int RemoveCost;
    public bool CanBeRemoved;
    
    public Player GetOwner()
    {
        return Owner;
    }

    public int GetRemoveCost(int replacementCost)
    {
        return RemoveCost + replacementCost - 1;
    }

    public int GetMinimalRemoveCost()
    {
        return RemoveCost;
    }

    public bool TryRemove(Player remover, int replacementCost)
    {
        throw new NotImplementedException();
    }
}