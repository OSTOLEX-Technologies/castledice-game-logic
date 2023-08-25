using CastleGO = castledice_game_logic.GameObjects.Castle;
using static castledice_game_logic_tests.ObjectCreationUtility;
namespace castledice_game_logic_tests;

public class CastleTests
{
    [Fact]
    public void IsRemovable_ShouldReturnTrue_Always()
    {
        var castle = new CastleGO(GetPlayer());
        
        Assert.True(castle.IsRemovable);
    }
}