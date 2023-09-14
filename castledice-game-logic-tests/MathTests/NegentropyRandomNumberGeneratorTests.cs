using System.Diagnostics;
using castledice_game_logic.Math;

namespace castledice_game_logic_tests.MathTests;

public class NegentropyRandomNumberGeneratorTests
{
    [Fact]
    public void GetNextRandom_MustReturnSomeNumber()
    {
        var generator = new NegentropyRandomNumberGenerator(1, 6, 100);
        generator.GetNextRandom();
        Assert.True(generator.GetNextRandom() > 0);
    }
}