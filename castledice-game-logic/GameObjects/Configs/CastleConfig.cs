namespace castledice_game_logic.GameObjects.Configs;

public struct CastleConfig
{
    public int Durability { get; }
    public int FreeDurability { get; }
    public int CaptureCost { get; }
    

    public CastleConfig(int durability, int freeDurability, int captureCost)
    {
        Durability = durability;
        FreeDurability = freeDurability;
        CaptureCost = captureCost;
    }
}