namespace castledice_game_logic.Math;

public class RangeRandomNumberGenerator : IRangeRandomNumberGenerator
{
    private readonly Random _rnd = new();

    public int GetRandom(int minInclusive, int maxExclusive)
    {
        return _rnd.Next(minInclusive, maxExclusive);
    }
}