namespace castledice_game_logic.GameObjects;

public interface IUpgradeable
{
    bool TryUpgrade(Player upgrader);
}