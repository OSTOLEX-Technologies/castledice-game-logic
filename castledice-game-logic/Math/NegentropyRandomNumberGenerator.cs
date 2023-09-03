namespace castledice_game_logic.Math;

public class NegentropyRandomNumberGenerator : IRandomNumberGenerator
{
    private Dictionary<int, float> _possibilities = new Dictionary<int, float>();

    private int _precision = 100;

    public NegentropyRandomNumberGenerator(int minInclusive, int maxExclusive, int precision)
    {
        _precision = precision;
        for (int i = minInclusive; i < maxExclusive; i++)
        {
            _possibilities.Add(i, _precision / (float)(maxExclusive - minInclusive));
        }
    }

    public int GetNextRandom()
    {
        var result = GetRandomFromPossibilities();
        _possibilities = AdjustPossibilities(result);
        return result;
    }

    private int GetRandomFromPossibilities()
    {
        var rnd = new Random();
        float randomValue = rnd.Next() / (float)int.MaxValue * _precision;
        float probabilitiesSum = 0f;
        foreach (var probability in _possibilities)
        {
            probabilitiesSum += probability.Value;
            if (randomValue < probabilitiesSum)
            {
                return probability.Key;
            }
        }
        return _possibilities.Last().Key;
    }

    private Dictionary<int, float> AdjustPossibilities(int lastResult)
    {
        var others = GetPossibilitiesCopy();
        others.Remove(lastResult);
        float raiseAmount = (float)_precision - ValuesSum(others);
        raiseAmount /= 2;
        int othersCount = others.Count;
        foreach (var key in others.Keys)
        {
            others[key] += raiseAmount / othersCount;
        }
        float leftAmount = _precision - ValuesSum(others);
        others.Add(lastResult, leftAmount);
        return others;

    }

    private Dictionary<int, float> GetPossibilitiesCopy()
    {
        Dictionary<int, float> copy = new Dictionary<int, float>();
        foreach (var item in _possibilities)
        {
            copy.Add(item.Key, item.Value);
        }
        return copy;
    }

    private float ValuesSum(Dictionary<int, float> possibilites)
    {
        float sum = 0f;
        foreach (var possibility in possibilites)
        {
            sum += possibility.Value;
        }
        return sum;
    }
}