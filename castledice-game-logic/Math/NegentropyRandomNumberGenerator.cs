namespace castledice_game_logic.Math;

public class NegentropyRandomNumberGenerator : IRandomNumberGenerator
{
    private Dictionary<int, float> _probabilities = new Dictionary<int, float>();

    private readonly int _precision;

    public NegentropyRandomNumberGenerator(int minInclusive, int maxExclusive, int precision)
    {
        _precision = precision;
        for (int i = minInclusive; i < maxExclusive; i++)
        {
            _probabilities.Add(i, _precision / (float)(maxExclusive - minInclusive));
        }
    }

    public int GetNextRandom()
    {
        var result = GetRandomFromProbabilities();
        _probabilities = AdjustProbabilities(result);
        return result;
    }

    private int GetRandomFromProbabilities()
    {
        var rnd = new Random();
        float randomValue = rnd.Next() / (float)int.MaxValue * _precision;
        float probabilitiesSum = 0f;
        foreach (var probability in _probabilities)
        {
            probabilitiesSum += probability.Value;
            if (randomValue < probabilitiesSum)
            {
                return probability.Key;
            }
        }
        return _probabilities.Last().Key;
    }

    private Dictionary<int, float> AdjustProbabilities(int lastResult)
    {
        var others = GetProbabilitiesCopy();
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

    private Dictionary<int, float> GetProbabilitiesCopy()
    {
        return _probabilities.ToDictionary(item => item.Key, item => item.Value);
    }

    private static float ValuesSum(Dictionary<int, float> probabilities)
    {
        return probabilities.Sum(possibility => possibility.Value);
    }
}