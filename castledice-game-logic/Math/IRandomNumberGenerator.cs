namespace castledice_game_logic.Math;

public interface IRandomNumberGenerator
{
    int GetRandom(int minInclusive, int maxInclusive);
}