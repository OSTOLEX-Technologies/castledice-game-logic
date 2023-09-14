using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class PlayerUnitMock : Content, IPlayerOwned, IReplaceable, IPlaceBlocking, IUpgradeable
{
    public Player Owner = new NullPlayer();
    public int RemoveCost;
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

    public bool IsBlocking()
    {
        return true;
    }

    public override void Update()
    {
        
    }

    public override void Accept(IContentVisitor visitor)
    {
        
    }
}