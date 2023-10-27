using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic.GameObjects.Factories;

public class CastlesFactory : ICastlesFactory
{
    private readonly CastleConfig _config;
    
    public CastlesFactory(CastleConfig config)
    {
        _config = config;
    }
    
    public Castle GetCastle(Player owner)
    {
        return new Castle(owner, _config.MaxDurability, _config.MaxDurability, _config.MaxFreeDurability, _config.CaptureHitCost);
    }
}