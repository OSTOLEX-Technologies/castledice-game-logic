using castledice_game_logic.GameObjects.Configs;

namespace castledice_game_logic_tests.ConfigsTests;

public class TreeConfigTests
{
    [Theory]
    [InlineData(1, false)]
    [InlineData(3, true)]
    public void Properties_ShouldReturnValues_GivenInConstructor(int removeCost, bool canBeRemoved)
    {
        var treeConfig = new TreeConfig(removeCost, canBeRemoved);
        
        Assert.Equal(canBeRemoved, treeConfig.CanBeRemoved);
        Assert.Equal(removeCost, treeConfig.RemoveCost);
    }
}