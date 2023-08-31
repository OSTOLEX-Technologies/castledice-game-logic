﻿namespace castledice_game_logic.GameObjects;

public interface IUpgradeable
{
    bool CanBeUpgraded();

    int GetUpgradeCost();
    
    bool Upgrade(Player upgrader);
}