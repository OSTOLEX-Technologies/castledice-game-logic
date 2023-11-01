namespace castledice_game_logic.GameConfiguration;

public class RandomConfig
{
    public int MinInclusive { get; }
    public int MaxExclusive { get; }
    public int Precision { get; }

    public RandomConfig(int minInclusive, int maxExclusive, int precision)
    {
        MinInclusive = minInclusive;
        MaxExclusive = maxExclusive;
        Precision = precision;
    }
}