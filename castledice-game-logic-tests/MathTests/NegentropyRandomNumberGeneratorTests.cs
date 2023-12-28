using System.Diagnostics;
using castledice_game_logic.Math;

namespace castledice_game_logic_tests.MathTests;

public class NegentropyRandomNumberGeneratorTests
{
    [Theory]
    [InlineData(1, 6, 100)]
    [InlineData(3, 18, 1000)]
    [InlineData(1, 12, 10000)]
    public void Properties_ShouldReturnValues_GivenInConstructor(int minInclusive, int maxExclusive, int precision)
    {
        var generator = new NegentropyRandomNumberGenerator(minInclusive, maxExclusive, precision);
        
        Assert.Equal(minInclusive, generator.MinInclusive);
        Assert.Equal(maxExclusive, generator.MaxExclusive);
        Assert.Equal(precision, generator.Precision);
    }
    
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