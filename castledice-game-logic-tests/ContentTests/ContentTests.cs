using castledice_game_logic.GameObjects;

namespace castledice_game_logic_tests;

public class ContentTests
{
    [Fact]
    public void IsRemovable_ShouldReturnFalse_ByDefault()
    {
        var content = new Content();
        
        Assert.False(content.IsRemovable);
    }
}