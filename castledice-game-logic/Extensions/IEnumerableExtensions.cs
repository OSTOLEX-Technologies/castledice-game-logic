namespace castledice_game_logic.Extensions;

public static class IEnumerableExtensions
{
    public static T GetRandom<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.GetRandom<T>(new Random());
    }

    public static T GetRandom<T>(this IEnumerable<T> enumerable, Random rand)
    {
        int index = rand.Next(0, enumerable.Count());
        return enumerable.ElementAt(index);
    }
}