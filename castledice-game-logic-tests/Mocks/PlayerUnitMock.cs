using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class PlayerUnitMock : Content, IPlayerOwned, IReplaceable, IPlaceBlocking, IUpgradeable
{
    public Player Owner;
    public int RemoveCost;
    public bool CanBeRemoved;
    public bool CanUpgrade = true;
    public int UpgradeCost;
    
    public Player GetOwner()
    {
        return Owner;
    }

    public int GetReplaceCost()
    {
        return RemoveCost;
    }

    public void Replace(Player remover, int replacementCost)
    {
        throw new NotImplementedException();
    }

    public bool CanBeUpgraded()
    {
        return CanUpgrade;
    }

    public int GetUpgradeCost()
    {
        return UpgradeCost;
    }

    public void Upgrade()
    {
        
    }
}