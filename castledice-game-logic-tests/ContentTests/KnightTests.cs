using castledice_game_logic.GameObjects;
using static castledice_game_logic_tests.ObjectCreationUtility;

namespace castledice_game_logic_tests;

public class KnightTests
{
    [Fact]
    public void IsRemovable_ShouldReturnTrue_Always()
    {
        var knight = new Knight(GetPlayer());
        
        Assert.True(knight.IsRemovable);
    }
}