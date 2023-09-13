namespace castledice_game_logic.GameObjects.Configs;

public struct CastleConfig
{
    public int Durability { get; }
    public int FreeDurability { get; }

    public CastleConfig(int durability, int freeDurability)
    {
        Durability = durability;
        FreeDurability = freeDurability;
    }
}