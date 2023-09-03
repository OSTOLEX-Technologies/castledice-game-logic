namespace castledice_game_logic.Math;

public interface IRangeRandomNumberGenerator
{
    int GetRandom(int minInclusive, int maxExclusive);
}