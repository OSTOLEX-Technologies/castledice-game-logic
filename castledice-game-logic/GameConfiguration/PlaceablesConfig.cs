using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic.GameConfiguration;

public class PlaceablesConfig
{
    public KnightConfig KnightConfig { get; }

    public PlaceablesConfig(KnightConfig knightConfig)
    {
        KnightConfig = knightConfig;
    }
}