using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic.GameConfiguration;

public class UnitsConfig
{
    public KnightConfig KnightConfig { get; }

    public UnitsConfig(KnightConfig knightConfig)
    {
        KnightConfig = knightConfig;
    }
}