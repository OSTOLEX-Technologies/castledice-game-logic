using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class ReplaceableMock : Content, IReplaceable, IPlayerOwned
{
    public int RemoveCost;
    public bool CanBeRemoved;
    public Player Owner;
    
    public int GetReplaceCost(int replacementCost)
    {
        return RemoveCost + replacementCost - 1;
    }

    public int GetMinimalReplaceCost()
    {
        return RemoveCost;
    }

    public void Replace(Player remover, int replacementCost)
    {
        throw new NotImplementedException();
    }

    public Player GetOwner()
    {
        return Owner;
    }
}