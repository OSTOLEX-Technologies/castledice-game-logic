﻿namespace castledice_game_logic.GameObjects.Configs;

public class KnightConfig
{
    public int PlacementCost { get; }
    public int Health { get; }

    public KnightConfig(int placementCost, int health)
    {
        PlacementCost = placementCost;
        Health = health;
    }
}