using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic.GameObjects.Factories.Castles;

public class CastlesFactory : ICastlesFactory
{
    public CastleConfig Config => _config;
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