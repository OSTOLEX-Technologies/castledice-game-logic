using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic.GameObjects.Factories;

public class KnightsFactory : IKnightsFactory
{
    private KnightConfig _config;
    
    public KnightsFactory(KnightConfig config)
    {
        _config = config;
    }
    
    public Knight GetKnight(Player owner)
    {
        return new Knight(owner, _config.PlacementCost, _config.Health);
    }
}