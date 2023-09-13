using castledice_game_logic;
using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests.Mocks;

public class UpgradeableMock : Content, IUpgradeable, IPlayerOwned
{
    public bool Upgradeable = true;
    public int UpgradeCost = 1;
    public Player Owner = new NullPlayer();
    public int Level;
        
    public bool CanBeUpgraded()
    {
        return Upgradeable;
    }

    public int GetUpgradeCost()
    {
        return UpgradeCost;
    }

    public void Upgrade()
    {
        Level++;
    }

    public Player GetOwner()
    {
        return Owner;
    }
}