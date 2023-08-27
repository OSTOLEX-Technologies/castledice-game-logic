namespace castledice_game_logic.GameObjects;

public class Knight : Content, IPlayerOwned, IUpgradeable, IRemovable
{
    private Player _player;

    public Knight(Player player)
    {
        _player = player;
    }

    public Player GetOwner()
    {
        return _player;
    }

    public bool CanBeUpgraded()
    {
        return true;
    }

    public int GetUpgradeCost()
    {
        throw new NotImplementedException();
    }

    public bool TryUpgrade(Player upgrader)
    {
        throw new NotImplementedException();
    }

    public bool TryRemove(Player remover, int replacementCost)
    {
        throw new NotImplementedException();
    }
}