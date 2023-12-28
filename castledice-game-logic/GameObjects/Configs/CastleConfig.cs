namespace castledice_game_logic.GameObjects.Configs;

public class CastleConfig
{
    public int MaxDurability { get; }
    public int MaxFreeDurability { get; }
    public int CaptureHitCost { get; }
    

    public CastleConfig(int maxDurability, int maxFreeDurability, int captureHitCost)
    {
        MaxDurability = maxDurability;
        MaxFreeDurability = maxFreeDurability;
        CaptureHitCost = captureHitCost;
    }
}