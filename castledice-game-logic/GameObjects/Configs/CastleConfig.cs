namespace castledice_game_logic.GameObjects.Configs;

public class CastleConfig
{
    public int Durability { get; }
    public int FreeDurability { get; }
    public int CaptureHitCost { get; }
    

    public CastleConfig(int durability, int freeDurability, int captureHitCost)
    {
        Durability = durability;
        FreeDurability = freeDurability;
        CaptureHitCost = captureHitCost;
    }
}