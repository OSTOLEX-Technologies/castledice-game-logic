namespace castledice_game_logic.Math;

public class RandomNumberGenerator : IRandomNumberGenerator
{
    private Random _rnd = new Random();
    
    public int Range(int minInclusive, int maxExclusive)
    {
        return _rnd.Next(minInclusive, maxExclusive);
    }
}