namespace castledice_game_logic.GameConfiguration;

public class RandomConfig
{
    public int MinActionPointsRoll { get; }
    public int MaxActionPointsRoll { get; }
    public int ProbabilityPrecision { get; }

    public RandomConfig(int minActionPointsRoll, int maxActionPointsRoll, int probabilityPrecision)
    {
        MinActionPointsRoll = minActionPointsRoll;
        MaxActionPointsRoll = maxActionPointsRoll;
        ProbabilityPrecision = probabilityPrecision;
    }
}