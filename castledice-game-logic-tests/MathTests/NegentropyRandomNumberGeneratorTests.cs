using System.Diagnostics;
using castledice_game_logic.Math;

namespace castledice_game_logic_tests.MathTests;

public class NegentropyRandomNumberGeneratorTests
{
    [Fact]
    public void GetNextRandom_MustReturnNumber_FromGivenInterval()
    {
        int min = 1;
        int max = 6;
        var generator = new NegentropyRandomNumberGenerator(min, max, 100);
        
        int result = generator.GetNextRandom();
        
        Assert.True(result >= min && result < max);
    }
}